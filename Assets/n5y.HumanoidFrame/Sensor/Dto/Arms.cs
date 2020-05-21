using System.Collections.Generic;

namespace n5y.HumanoidFrame.Sensor.Dto
{
    public class Arms : INode
    {
        public Arm Right { set; get; }
        public Arm Left { set; get; }

        public Arms(NodeId id)
        {
            Id = id;
        }

        public NodeId Id { get; }
        public ICollection<INode> Forward => new List<INode> {Right, Left};
        public ICollection<INode> Backward => new List<INode>(); //Empty
    }
}
