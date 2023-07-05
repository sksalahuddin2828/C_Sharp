// Please note that you need to install the Emgu.CV NuGet package and its dependencies to run this code.

using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.VideoWriter;

namespace WebcamCapture
{
    public class Program
    {
        private static string filename = "video.avi"; // change here which format you want to save here .mp4 .avi etc
        private static double framesPerSecond = 24.0;
        private static string res = "720p";

        private static readonly Size[] StandardDimensions =
        {
            new Size(640, 480),   // 480p
            new Size(1280, 720),  // 720p
            new Size(1920, 1080), // 1080p
            new Size(3840, 2160)  // 4k
        };

        private static VideoWriter writer;

        public static void Main()
        {
            using (var capture = new VideoCapture())
            {
                capture.Start();
                var frameSize = GetFrameSize(capture, res);
                writer = new VideoWriter(filename, FourCC.MJPG, framesPerSecond, frameSize, true);

                while (true)
                {
                    using (var frame = capture.QueryFrame().ToImage<Bgr, byte>())
                    {
                        if (frame == null)
                            break;

                        CvInvoke.Imshow("frame", frame);
                        writer.Write(frame);
                    }

                    if (CvInvoke.WaitKey(1) == 'q')
                        break;
                }
            }

            writer.Dispose();
            CvInvoke.DestroyAllWindows();
        }

        private static Size GetFrameSize(VideoCapture capture, string res)
        {
            Size frameSize = StandardDimensions[0];

            switch (res)
            {
                case "480p":
                    frameSize = StandardDimensions[0];
                    break;
                case "720p":
                    frameSize = StandardDimensions[1];
                    break;
                case "1080p":
                    frameSize = StandardDimensions[2];
                    break;
                case "4k":
                    frameSize = StandardDimensions[3];
                    break;
            }

            capture.SetCaptureProperty(CapProp.FrameWidth, frameSize.Width);
            capture.SetCaptureProperty(CapProp.FrameHeight, frameSize.Height);

            return frameSize;
        }
    }
}
