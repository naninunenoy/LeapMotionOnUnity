using System.Collections.Generic;
using UnityEngine;

namespace n5y.LeapMotionSample.Limb
{
    public class Hand : INode
    {
        public Node Wrist { set; get; }
        public Node Palm { set; get; }
        public Quaternion Rotation { set; get; }
        public IList<Node> Thumbs { set; get; }
        public IList<Node> Indexes { set; get; }
        public IList<Node> Middles { set; get; }
        public IList<Node> Rings { set; get; }
        public IList<Node> Pinkies { set; get; }

        public NodeId Id => Palm.Id;
        public ICollection<INode> Forward => new List<INode> {Wrist};
        public ICollection<INode> Backward => new List<INode> {Thumbs[0], Indexes[0], Middles[0], Rings[0], Pinkies[0]};
    }
}
