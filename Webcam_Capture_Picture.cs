// Make sure to install the Emgu.CV NuGet package and its dependencies to run this code.

using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace WebcamCapture
{
    public class Program
    {
        private static VideoCapture capture;

        public static void Main()
        {
            capture = new VideoCapture(0);

            while (true)
            {
                using (var frame = capture.QueryFrame().ToImage<Bgr, byte>())
                {
                    if (frame == null)
                        break;

                    var rescaledFrame = RescaleFrame(frame, 30);
                    CvInvoke.Imshow("frame", rescaledFrame);

                    var rescaledFrame2 = RescaleFrame(frame, 500);
                    CvInvoke.Imshow("frame2", rescaledFrame2);
                }

                if (CvInvoke.WaitKey(20) == 'q')
                    break;
            }

            capture.Dispose();
            CvInvoke.DestroyAllWindows();
        }

        private static Mat RescaleFrame(Mat frame, double percent)
        {
            double scalePercent = percent / 100;
            int width = (int)(frame.Width * scalePercent);
            int height = (int)(frame.Height * scalePercent);
            var rescaledFrame = new Mat();
            CvInvoke.Resize(frame, rescaledFrame, new System.Drawing.Size(width, height), 0, 0, Inter.Linear);
            return rescaledFrame;
        }
    }
}
