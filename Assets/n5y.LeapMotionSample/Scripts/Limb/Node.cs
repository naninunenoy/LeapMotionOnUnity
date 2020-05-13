using System;
using System.Collections.Generic;
using UnityEngine;

namespace n5y.LeapMotionSample.Limb
{
    public class Node: INode, IEquatable<Node>
    {
        public NodeId Id { get; }
        public ICollection<INode> Forward { set; get; }
        public ICollection<INode> Backward { set; get; }
        public Vector3 Position { set; get; }

        public Node(NodeId id)
        {
            Id = id;
            Forward = new List<INode>();
            Backward = new List<INode>();
            Position = Vector3.zero;
        }

        public bool Equals(Node other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
