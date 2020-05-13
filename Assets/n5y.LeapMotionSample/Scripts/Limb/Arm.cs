using System.Collections.Generic;
using UnityEngine;

namespace n5y.LeapMotionSample.Limb
{
    public class Arm : INode
    {
        public Node Elbow { set; get; }
        public Node Wrist { set; get; }
        public Hand Hand { set; get; }
        public Quaternion Rotation { set; get; }
        public NodeId Id => Elbow.Id;
        public ICollection<INode> Forward => new List<INode> {Hand};
        public ICollection<INode> Backward => new List<INode> {Elbow};
    }
}
