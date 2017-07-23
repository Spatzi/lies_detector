using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Kinect;

namespace LieDetectorApplication.Models
{
    [DataContract]
    internal class Record
    {
        private static readonly DataContractSerializer Serializer = new DataContractSerializer(typeof (Record));
        [DataMember] private readonly Dictionary<long, byte[]> _images; // Milliseconds => Image from frame
        [DataMember] private readonly Dictionary<long, Skeleton> _skeletons; // Milliseconds => Skeleton from frame

        public Record()
        {
            _skeletons = new Dictionary<long, Skeleton>();
            _images = new Dictionary<long, byte[]>();
        }

        public Dictionary<long, Skeleton> Skeletons
        {
            get { return _skeletons; }
        }

        public Dictionary<long, byte[]> Images
        {
            get { return _images; }
        }

        public static Record Open(string path)
        {
            using (Stream stream = new FileStream(path, FileMode.Open))
            {
                return (Record) Serializer.ReadObject(stream);
            }
        }

        public void Save(string path)
        {
            using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Serializer.WriteObject(stream, this);
            }
        }
    }
}