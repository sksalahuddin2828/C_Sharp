using System;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Forms;

namespace HSVColorDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            int frameWidth = 640;
            int frameHeight = 480;

            VideoCapture cap = new VideoCapture(0);
            cap.Set(VideoCaptureProperties.FrameWidth, frameWidth);
            cap.Set(VideoCaptureProperties.FrameHeight, frameHeight);

            Cv2.NamedWindow("HSV");
            Cv2.ResizeWindow("HSV", 640, 240);

            Mat frame = new Mat();
            Mat imgHsv = new Mat();
            Mat mask = new Mat();
            Mat result = new Mat();
            Mat hStack = new Mat();

            Cv2.CreateTrackbar("HUE Min", "HSV", 0, 179, EmptyFunction);
            Cv2.CreateTrackbar("HUE Max", "HSV", 179, 179, EmptyFunction);
            Cv2.CreateTrackbar("SAT Min", "HSV", 0, 255, EmptyFunction);
            Cv2.CreateTrackbar("SAT Max", "HSV", 255, 255, EmptyFunction);
            Cv2.CreateTrackbar("VALUE Min", "HSV", 0, 255, EmptyFunction);
            Cv2.CreateTrackbar("VALUE Max", "HSV", 255, 255, EmptyFunction);

            while (true)
            {
                cap.Read(frame);

                if (frame.Empty())
                {
                    Console.WriteLine("Failed to capture frame from the camera.");
                    break;
                }

                Cv2.CvtColor(frame, imgHsv, ColorConversionCodes.BGR2HSV);

                int hMin = Cv2.GetTrackbarPos("HUE Min", "HSV");
                int hMax = Cv2.GetTrackbarPos("HUE Max", "HSV");
                int sMin = Cv2.GetTrackbarPos("SAT Min", "HSV");
                int sMax = Cv2.GetTrackbarPos("SAT Max", "HSV");
                int vMin = Cv2.GetTrackbarPos("VALUE Min", "HSV");
                int vMax = Cv2.GetTrackbarPos("VALUE Max", "HSV");

                Scalar lower = new Scalar(hMin, sMin, vMin);
                Scalar upper = new Scalar(hMax, sMax, vMax);

                Cv2.InRange(imgHsv, lower, upper, mask);
                Cv2.BitwiseAnd(frame, frame, result, mask);

                Cv2.CvtColor(mask, mask, ColorConversionCodes.GRAY2BGR);

                Cv2.HConcat(new[] { frame, mask, result }, hStack);

                Cv2.ImShow("Horizontal Stacking", hStack);

                char key = (char)Cv2.WaitKey(1);
                if (key == 'q')
                    break;
            }

            cap.Release();
            Cv2.DestroyAllWindows();
        }

        static void EmptyFunction(int pos)
        {
            int threshold1 = Cv2.GetTrackbarPos("Threshold1", "Parameters");
            int threshold2 = Cv2.GetTrackbarPos("Threshold2", "Parameters");
            int area = Cv2.GetTrackbarPos("Area", "Parameters");
            Console.WriteLine($"Threshold1: {threshold1}");
            Console.WriteLine($"Threshold2: {threshold2}");
            Console.WriteLine($"Area: {area}");
        }
    }
}
