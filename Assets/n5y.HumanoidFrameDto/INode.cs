using System.Collections.Generic;

namespace n5y.HumanoidFrameDto
{
    public interface INode
    {
        NodeId Id { get; }
        ICollection<INode> Forward { get; }
        ICollection<INode> Backward{ get; }
    }
}
