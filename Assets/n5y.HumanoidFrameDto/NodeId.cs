using System;

namespace n5y.HumanoidFrameDto
{
    public readonly struct NodeId : IEquatable<NodeId>
    {
        public readonly int Value;
        public NodeId(int id) => Value = id;

        public bool Equals(NodeId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is NodeId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static readonly NodeId OriginNodeId = new NodeId(0);
    }
}
