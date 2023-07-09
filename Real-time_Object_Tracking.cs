using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ObjectTracking
{
    public partial class MainForm : Form
    {
        private VideoCapture _capture;
        private Tracker _tracker;
        private bool _isTracking = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _capture = new VideoCapture(0); // Open the video capture
            if (!_capture.IsOpened)
            {
                MessageBox.Show("Failed to open video capture");
                return;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _capture?.Dispose(); // Release the video capture
        }

        private void StartTracking()
        {
            // Read the first frame from the video
            Mat frame = _capture.QueryFrame();
            if (frame == null)
            {
                MessageBox.Show("Failed to read video");
                return;
            }

            // Select the region of interest (ROI) for tracking
            Rectangle roi = CvInvoke.SelectROI("Tracking", frame, false);

            // Create a tracker object
            _tracker = new Emgu.CV.Tracking.KCFTracker();
            _tracker.Init(roi, frame);

            // Set tracking flag to true
            _isTracking = true;

            // Main loop for video processing
            while (_isTracking)
            {
                // Read a frame from the video
                frame = _capture.QueryFrame();
                if (frame == null)
                {
                    MessageBox.Show("Failed to read video");
                    break;
                }

                // Update the tracker with the current frame
                bool success = _tracker.Update(frame, out roi);

                // If tracking is successful, draw the bounding box
                if (success)
                    CvInvoke.Rectangle(frame, roi, new Bgr(Color.Yellow).MCvScalar, 3);
                else
                    CvInvoke.PutText(frame, "Lost", new Point(20, 40), Emgu.CV.CvEnum.FontFace.HersheyScriptSimplex, 0.9, new Bgr(Color.Red).MCvScalar, 2, Emgu.CV.CvEnum.LineType.AntiAlias);

                // Display the image with tracking results
                CvInvoke.Imshow("Tracking", frame);
                CvInvoke.WaitKey(1);
            }
        }

        private void btnStartTracking_Click(object sender, EventArgs e)
        {
            StartTracking();
        }

        private void btnStopTracking_Click(object sender, EventArgs e)
        {
            _isTracking = false;
        }
    }
}
