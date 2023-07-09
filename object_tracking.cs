using System;
using OpenCvSharp;

class Program
{
    static void Main()
    {
        // Create a tracker object
        using (var tracker = TrackerKCF.Create())
        {
            // Open the video capture
            using (var capture = new VideoCapture(0))
            {
                if (!capture.IsOpened())
                {
                    Console.WriteLine("Failed to open video capture.");
                    return;
                }

                // Read the first frame from the video
                using (var frame = new Mat())
                {
                    if (!capture.Read(frame))
                    {
                        Console.WriteLine("Failed to read frame.");
                        return;
                    }

                    // Select the region of interest (ROI) for tracking
                    using (var bbox = Cv2.SelectROI("Tracking", frame, false))
                    {
                        // Initialize the tracker with the selected ROI
                        tracker.Init(frame, bbox);

                        // Main loop for video processing
                        while (true)
                        {
                            // Read a frame from the video
                            if (!capture.Read(frame))
                            {
                                Console.WriteLine("Failed to read frame.");
                                break;
                            }

                            // Update the tracker with the current frame
                            var success = tracker.Update(frame, out var boundingBox);

                            // If tracking is successful, draw the bounding box
                            if (success)
                            {
                                Cv2.Rectangle(frame, boundingBox, Scalar.Magenta, 3);
                                Cv2.PutText(frame, "Tracking", new Point((int)boundingBox.Left, (int)boundingBox.Top - 10),
                                    HersheyFonts.HersheyPlain, 0.7, Scalar.Green, 2);
                            }
                            else
                            {
                                Cv2.PutText(frame, "Lost", new Point(100, 75), HersheyFonts.HersheyPlain, 0.7, Scalar.Blue, 2);
                            }

                            // Display the frame
                            Cv2.ImShow("Tracking", frame);

                            // Exit the loop if 'q' is pressed
                            if (Cv2.WaitKey(1) == 'q')
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
