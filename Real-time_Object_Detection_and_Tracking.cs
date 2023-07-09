using OpenCvSharp;
using System;
using System.Collections.Generic;

class Program
{
    static void EmptyFunction(int value, IntPtr userdata)
    {
        int threshold1 = Cv2.GetTrackbarPos("Threshold1", "Parameters");
        int threshold2 = Cv2.GetTrackbarPos("Threshold2", "Parameters");
        int area = Cv2.GetTrackbarPos("Area", "Parameters");
        Console.WriteLine("Threshold1: " + threshold1);
        Console.WriteLine("Threshold2: " + threshold2);
        Console.WriteLine("Area: " + area);
    }

    static Mat StackImages(double scale, IEnumerable<IEnumerable<Mat>> imgArray)
    {
        var images = new List<Mat[]>();
        int maxWidth = 0;
        int totalHeight = 0;

        foreach (var row in imgArray)
        {
            var rowImages = row as List<Mat>;
            images.Add(rowImages.ToArray());

            foreach (var img in rowImages)
            {
                if (img.Width > maxWidth)
                    maxWidth = img.Width;

                totalHeight += img.Height;
            }
        }

        var result = new Mat(totalHeight, maxWidth, MatType.CV_8UC3, Scalar.All(0));

        int y = 0;
        foreach (var rowImages in images)
        {
            int x = 0;
            foreach (var img in rowImages)
            {
                var resized = new Mat();
                Cv2.Resize(img, resized, new Size(), scale, scale);

                var roi = new Mat(result, new Rect(x, y, resized.Width, resized.Height));
                resized.CopyTo(roi);

                x += resized.Width;
            }

            y += rowImages[0].Height;
        }

        return result;
    }

    static void GetContours(Mat img, Mat imgContour)
    {
        var contours = new Point[][] { };
        var hierarchy = new HierarchyIndex[] { };

        Cv2.FindContours(img, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

        for (int i = 0; i < contours.Length; i++)
        {
            double area = Cv2.ContourArea(contours[i]);
            int areaMin = Cv2.GetTrackbarPos("Area", "Parameters");

            if (area > areaMin)
            {
                Cv2.DrawContours(imgContour, contours, i, Scalar.Magenta, 7);

                double perimeter = Cv2.ArcLength(contours[i], true);
                var approx = Cv2.ApproxPolyDP(contours[i], 0.02 * perimeter, true);

                Rect rect = Cv2.BoundingRect(approx);
                Cv2.Rectangle(imgContour, rect, Scalar.Green, 5);

                Cv2.PutText(imgContour, "Points: " + approx.Length, new Point(rect.Right + 20, rect.Top + 20),
                    HersheyFonts.HersheyComplex, 0.7, Scalar.Green, 2);
                Cv2.PutText(imgContour, "Area: " + (int)area, new Point(rect.Right + 20, rect.Top + 45),
                    HersheyFonts.HersheyComplex, 0.7, Scalar.Green, 2);
            }
        }
    }

    static void Main(string[] args)
    {
        int frameWidth = 640;
        int frameHeight = 480;

        var cap = new VideoCapture(0);
        cap.Set(VideoCaptureProperties.FrameWidth, frameWidth);
        cap.Set(VideoCaptureProperties.FrameHeight, frameHeight);

        Cv2.NamedWindow("Parameters");
        Cv2.ResizeWindow("Parameters", 640, 240);

        Cv2.CreateTrackbar("Threshold1", "Parameters", 23, 255, EmptyFunction);
        Cv2.CreateTrackbar("Threshold2", "Parameters", 20, 255, EmptyFunction);
        Cv2.CreateTrackbar("Area", "Parameters", 5000, 30000, EmptyFunction);

        using (var img = new Mat())
        using (var imgContour = new Mat())
        using (var imgBlur = new Mat())
        using (var imgGray = new Mat())
        using (var imgCanny = new Mat())
        using (var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(5, 5)))
        using (var imgDil = new Mat())
        {
            while (true)
            {
                cap.Read(img);

                if (img.Empty())
                {
                    Console.WriteLine("Failed to capture frame from the camera.");
                    break;
                }

                imgContour = img.Clone();
                Cv2.GaussianBlur(img, imgBlur, new Size(7, 7), 1);
                Cv2.CvtColor(imgBlur, imgGray, ColorConversionCodes.BGR2GRAY);

                int threshold1 = Cv2.GetTrackbarPos("Threshold1", "Parameters");
                int threshold2 = Cv2.GetTrackbarPos("Threshold2", "Parameters");
                Cv2.Canny(imgGray, imgCanny, threshold1, threshold2);
                Cv2.Dilate(imgCanny, imgDil, kernel, iterations: 1);

                GetContours(imgDil, imgContour);

                var imgStack = StackImages(0.8, new[]
                {
                    new[] { img, imgCanny },
                    new[] { imgDil, imgContour }
                });

                Cv2.ImShow("Result", imgStack);

                if (Cv2.WaitKey(1) == 'q')
                    break;
            }
        }

        Cv2.DestroyAllWindows();
    }
}
