namespace n5y.LeapMotionSample.Limb
{
    public class NodeIdFactory : INodeIdFactory
    {
        int currentId;

        public NodeIdFactory(NodeId firstNodeId)
        {
            currentId = firstNodeId.Value;
        }

        public NodeId Create()
        {
            var ret = new NodeId(currentId);
            currentId++;
            return ret;
        }
    }
}
