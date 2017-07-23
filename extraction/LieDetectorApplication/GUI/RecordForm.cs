using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using LieDetectorApplication.Models;
using Microsoft.Kinect;
using Image = System.Windows.Controls.Image;

namespace LieDetectorApplication.GUI
{
    public partial class RecordForm : Form
    {
        private readonly Image _image;
        private SkeletonStreamPainter _painter;
        private Record _record;
        private KinectSensor _sensor;

        public RecordForm()
        {
            InitializeComponent();

            var elementHost = new ElementHost
            {
                Size = new Size(640, 480),
                Location = new Point(20, 50)
            };

            var imageUserControl = new ImageUserControl();
            elementHost.Child = imageUserControl;

            Controls.Add(elementHost);
            _image = imageUserControl.Image;

            saveRecordDialog.DefaultExt = "record";
            saveRecordDialog.AddExtension = true;
        }

        private void init_btn_Click(object sender, EventArgs e)
        {
            _sensor = KinectSensor.KinectSensors.ToList().FirstOrDefault(s => s.Status == KinectStatus.Connected);
            if (_sensor == null)
            {
                MessageBox.Show("Can't start recording, sensor not found", "Sensor not found", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            _sensor.SkeletonStream.Enable(new TransformSmoothParameters
            {
                Smoothing = 0.5f,
                Correction = 0.5f,
                Prediction = 0.5f,
                JitterRadius = 0.02f,
                MaxDeviationRadius = 0.04f
            });
            try
            {
                _sensor.Start();
            }
            catch (IOException)
            {
                _sensor = null;
                MessageBox.Show("Can't start recording, sensor error", "Sensor error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            _painter = new SkeletonStreamPainter(_sensor, _image);
            _painter.Start();

            init_btn.Enabled = false;
            start_btn.Enabled = true;
        }

        private void record_btn_Click(object sender, EventArgs e)
        {
            _record = new Record();
            _sensor.SkeletonFrameReady += SkeletonFrameReady;
            _sensor.ColorFrameReady += ColorFrameReady;
            start_btn.Enabled = false;
            stop_btn.Enabled = true;
        }

        private void SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            var skeletonFrame = e.OpenSkeletonFrame();
            if (skeletonFrame == null)
            {
                return;
            }
            var time = skeletonFrame.Timestamp;
            var skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
            skeletonFrame.CopySkeletonDataTo(skeletons);
            skeletonFrame.Dispose();
            skeletons.Where(skeleton => skeleton.TrackingState == SkeletonTrackingState.Tracked)
                .ToList()
                .ForEach(s => _record.Skeletons[time] = s);
        }

        private void ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            var colorFrame = e.OpenColorImageFrame();
            if (colorFrame == null)
            {
                return;
            }
            var time = colorFrame.Timestamp;
            var image = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(image);
            colorFrame.Dispose();
            _record.Images[time] = image;
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            _painter.Stop();
            _sensor.SkeletonFrameReady -= SkeletonFrameReady;
            if (saveRecordDialog.ShowDialog() == DialogResult.OK)
            {
                _record.Save(saveRecordDialog.FileName);
            }
            Close();
        }
    }
}