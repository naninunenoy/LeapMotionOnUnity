using System.Collections.Generic;

namespace n5y.LeapMotionSample.Limb
{
    public class Chest : INode
    {
        public Arm RightArm { set; get; }
        public Arm LeftArm { set; get; }

        public Chest(NodeId id)
        {
            Id = id;
        }

        public NodeId Id { get; }
        public ICollection<INode> Forward => new List<INode> {LeftArm, RightArm};
        public ICollection<INode> Backward => new List<INode>(); //Empty
    }
}
