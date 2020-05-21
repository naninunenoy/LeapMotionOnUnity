using System.Collections.Generic;

namespace n5y.HumanoidFrame.Dto
{
    public interface INode
    {
        NodeId Id { get; }
        ICollection<INode> Forward { get; }
        ICollection<INode> Backward{ get; }
    }
}
