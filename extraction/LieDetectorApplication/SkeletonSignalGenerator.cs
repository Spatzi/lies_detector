using System.Collections.Generic;
using System.Linq;
using Liadmat.Signal;
using Microsoft.Kinect;

namespace LieDetectorApplication
{
    internal class SkeletonSignalGenerator
    {
        private readonly Dictionary<long, Skeleton> _skeletons;

        public SkeletonSignalGenerator(Dictionary<long, Skeleton> skeletons)
        {
            _skeletons = skeletons;
        }

        public Signal3D JointPosition(JointType jointType)
        {
            return new Signal3D(_skeletons
                .Where(kv => kv.Value.Joints[jointType].TrackingState == JointTrackingState.Tracked)
                .ToDictionary(
                    kv => kv.Key,
                    kv => new Value3D(
                        kv.Value.Joints[jointType].Position.X,
                        kv.Value.Joints[jointType].Position.Y,
                        kv.Value.Joints[jointType].Position.Z
                    )
                )
            );
        }
    }
}