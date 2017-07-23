using System;
using System.Collections.Generic;
using System.Linq;
using LieDetectorApplication.Models;
using Microsoft.Kinect;

namespace LieDetectorApplication
{
    internal static class RecordPropertyExtractor
    {
        private const double NEAR = 0.4;
        private const double VERY_NEAR = 0.2;
        private const double FAST = 0.001;
        private static readonly List<Tuple<string, Func<Record, double>>> Properties = new List
            <Tuple<string, Func<Record, double>>>
        {
            /************************************************** Properties definitions **************************************************/

            // A
            new Tuple<string, Func<Record, double>>("Percentage of frames where the left hand is near and behind the head", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);

                var direction = leftHandPosition - headPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z > 0).Count() / (double)direction.Count();
            }),
			
            // B
			new Tuple<string, Func<Record, double>>("Percentage of frames where the right hand is near and behind the head", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);

                var direction = rightHandPosition - headPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z > 0).Count() / (double)direction.Count();
            }),
						
            // C
			new Tuple<string, Func<Record, double>>("Percentage of frames where the left hand is near and behind the neck", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
				var chestPosition = ssg.JointPosition(JointType.ShoulderCenter);
                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);
				
				var neckPosition = (headPosition - chestPosition) * 0.5;
                var direction = leftHandPosition - neckPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z > 0).Count() / (double)direction.Count();
            }),
						
            // D
			new Tuple<string, Func<Record, double>>("Percentage of frames where the right hand is near and behind the neck", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
				var chestPosition = ssg.JointPosition(JointType.ShoulderCenter);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);
				
				var neckPosition = (headPosition - chestPosition) * 0.5;
                var direction = rightHandPosition - neckPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z > 0).Count() / (double)direction.Count();
            }),
						
            // E
			new Tuple<string, Func<Record, double>>("Percentage of frames where the left hand is near and above the head", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);

                var direction = leftHandPosition - headPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Y > 0).Count() / (double)direction.Count();
            }),
					
            // F
			new Tuple<string, Func<Record, double>>("Percentage of frames where the right hand is near and above the head", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);

                var direction = rightHandPosition - headPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Y > 0).Count() / (double)direction.Count();
            }),
            			
            // G
            new Tuple<string, Func<Record, double>>("Percentage of frames where the left hand is near and in front of the head", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);

                var direction = leftHandPosition - headPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z < 0).Count() / (double)direction.Count();
            }),
						
            // H
			new Tuple<string, Func<Record, double>>("Percentage of frames where the right hand is near and in front of the head", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);

                var direction = rightHandPosition - headPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z < 0).Count() / (double)direction.Count();
            }),
            			
            // I
            new Tuple<string, Func<Record, double>>("Percentage of frames with short distance between left hand and right shoulder", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var rightShoulderPosition = ssg.JointPosition(JointType.ShoulderRight);
                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);

                var distance = (rightShoulderPosition - leftHandPosition).Length();

                return (double)distance.Filter(value => value < VERY_NEAR).Count() / (double)distance.Count();
            }),
						
            // J
			new Tuple<string, Func<Record, double>>("Percentage of frames with short distance between right hand and left shoulder", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var leftShoulderPosition = ssg.JointPosition(JointType.ShoulderLeft);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);

                var distance = (leftShoulderPosition - rightHandPosition).Length();

                return (double)distance.Filter(value => value < NEAR).Count() / (double)distance.Count();
            }),
						
            // K
			new Tuple<string, Func<Record, double>>("Percentage of frames with short distance between left hand and left shoulder", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var leftShoulderPosition = ssg.JointPosition(JointType.ShoulderLeft);
                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);

                var distance = (leftShoulderPosition - leftHandPosition).Length();

                return (double)distance.Filter(value => value < NEAR).Count() / (double)distance.Count();
            }),
						
            // L
			new Tuple<string, Func<Record, double>>("Percentage of frames with short distance between right hand and right shoulder", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var rightShoulderPosition = ssg.JointPosition(JointType.ShoulderRight);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);

                var distance = (rightShoulderPosition - rightHandPosition).Length();

                return (double)distance.Filter(value => value < NEAR).Count() / (double)distance.Count();
            }),
						
            // M
			new Tuple<string, Func<Record, double>>("Percentage of frames with short distance between left hand and right elbow", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var rightElbowPosition = ssg.JointPosition(JointType.ElbowRight);
                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);

                var distance = (rightElbowPosition - leftHandPosition).Length();

                return (double)distance.Filter(value => value < VERY_NEAR).Count() / (double)distance.Count();
            }),
						
            // N
			new Tuple<string, Func<Record, double>>("Percentage of frames with short distance between right hand and left elbow", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var leftElbowPosition = ssg.JointPosition(JointType.ElbowLeft);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);

                var distance = (leftElbowPosition - rightHandPosition).Length();

                return (double)distance.Filter(value => value < VERY_NEAR).Count() / (double)distance.Count();
            }),
            			
            // O
            new Tuple<string, Func<Record, double>>("Percentage of frames with short distance between knees", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var rightKneePosition = ssg.JointPosition(JointType.KneeRight);
                var leftKneePosition = ssg.JointPosition(JointType.KneeLeft);

                var distance = (rightKneePosition - leftKneePosition).Length();

                return (double)distance.Filter(value => value < VERY_NEAR).Count() / (double)distance.Count();
            }),

            // P
            new Tuple<string, Func<Record, double>>("Percentage of frames where left hand is moving fast", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);
                var leftHandSpeed = leftHandPosition.Derivative().Length();

                return (double)leftHandSpeed.Filter(value => value > FAST).Count() / (double)leftHandSpeed.Count();
            }),
						
            // Q
            new Tuple<string, Func<Record, double>>("Percentage of frames where right hand is moving fast", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var rightHandPosition = ssg.JointPosition(JointType.HandRight);
                var rightHandSpeed = rightHandPosition.Derivative().Length();

                return (double)rightHandSpeed.Filter(value => value > FAST).Count() / (double)rightHandSpeed.Count();
            }),

            // R
            new Tuple<string, Func<Record, double>>("Percentage of frames where the left hand is near and behind the back", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var lowerBackPosition = ssg.JointPosition(JointType.HipCenter);
                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);

                var direction = leftHandPosition - lowerBackPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z > 0).Count() / (double)direction.Count();
            }),
						
            // S
			new Tuple<string, Func<Record, double>>("Percentage of frames where the right hand is near and behind the back", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var lowerBackPosition = ssg.JointPosition(JointType.HipCenter);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);

                var direction = rightHandPosition - lowerBackPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z > 0).Count() / (double)direction.Count();
            }), 
						
            // T
			new Tuple<string, Func<Record, double>>("Percentage of frames where the left hand is near and in front of the chest", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var chestPosition = ssg.JointPosition(JointType.ShoulderCenter);
                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);

                var direction = leftHandPosition - chestPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z < 0).Count() / (double)direction.Count();
            }),
						
            // U
			new Tuple<string, Func<Record, double>>("Percentage of frames where the right hand is near and in front of the chest", record =>
            {
                var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var chestPosition = ssg.JointPosition(JointType.ShoulderCenter);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);

                var direction = rightHandPosition - chestPosition;

                return (double)direction.Filter(value3D => value3D.Length() < NEAR && value3D.Z < 0).Count() / (double)direction.Count();
            }),
						
            // V
			new Tuple<string, Func<Record, double>>("Percentage of frames with small angle between the neck and the chest (looking downwards)", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
                var chestPosition = ssg.JointPosition(JointType.ShoulderCenter);
                var backPosition = ssg.JointPosition(JointType.Spine);
                

                var angle = (headPosition - chestPosition).Angle(backPosition - chestPosition);

                return (double)angle.Filter(value => value < 150).Count() / (double)angle.Count();
            }),
						
            // W
			new Tuple<string, Func<Record, double>>("Percentage of frames with large angle between the neck and the chest (looking upwards)", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var headPosition = ssg.JointPosition(JointType.Head);
                var chestPosition = ssg.JointPosition(JointType.ShoulderCenter);
                var backPosition = ssg.JointPosition(JointType.Spine);
                

                var angle = (headPosition - chestPosition).Angle(backPosition - chestPosition);

                return (double)angle.Filter(value => value > 160).Count() / (double)angle.Count();
            }),
						
            // X
			new Tuple<string, Func<Record, double>>("Percentage of frames with small angle between the Z axis of the Kinect and the line between the shoulders", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var leftShoulderPosition = ssg.JointPosition(JointType.ShoulderLeft);
                var rightShoulderPosition = ssg.JointPosition(JointType.ShoulderRight);

                var angle = (leftShoulderPosition - rightShoulderPosition).ZAngle();

                return (double)angle.Filter(value => value < 120 && value > 60).Count() / (double)angle.Count();
            }),
						
            // Y
			new Tuple<string, Func<Record, double>>("Percentage of frames with large angle between the Z axis of the Kinect and the line between the shoulders", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var leftShoulderPosition = ssg.JointPosition(JointType.ShoulderLeft);
                var rightShoulderPosition = ssg.JointPosition(JointType.ShoulderRight);

                var angle = (leftShoulderPosition - rightShoulderPosition).ZAngle();

                return (double)angle.Filter(value => value > 130 || value < 50).Count() / (double)angle.Count();
            }),
						
            // Z
			new Tuple<string, Func<Record, double>>("Percentage of frames with a short distance between the hands", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);
                var rightHandPosition = ssg.JointPosition(JointType.HandRight);

                var distance = rightHandPosition - leftHandPosition;

                return (double)distance.Filter(value3D => value3D.Length() < VERY_NEAR).Count() / (double)distance.Count();
            }),
             			
            // AA
            new Tuple<string, Func<Record, double>>("Percentage of frames where the left hand is near the left hip", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var leftHandPosition = ssg.JointPosition(JointType.HandLeft);
                var leftHipPosition = ssg.JointPosition(JointType.HipLeft);

                var distance = leftHandPosition - leftHipPosition;

                return (double)distance.Filter(value3D => value3D.Length() < VERY_NEAR).Count() / (double)distance.Count();
            }),
             			
            // AB
            new Tuple<string, Func<Record, double>>("Percentage of frames where the right hand is near the right hip", record =>
            {
               var ssg = new SkeletonSignalGenerator(record.Skeletons);

                var rightHandPosition = ssg.JointPosition(JointType.HandRight);
                var rightHipPosition = ssg.JointPosition(JointType.HipRight);

                var distance = rightHandPosition - rightHipPosition;

                return (double)distance.Filter(value3D => value3D.Length() < VERY_NEAR).Count() / (double)distance.Count();
            })
			
            /************************************************** Properties definitions **************************************************/
        };

        public static string[] PropertyNames = Properties.Select(t => t.Item1).ToArray();

        public static double[] PropertyValues(Record record)
        {
            return Properties.Select(t => t.Item2.Invoke(record)).ToArray();
        }
    }
}