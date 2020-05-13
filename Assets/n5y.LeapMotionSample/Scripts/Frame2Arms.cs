using System.Collections.Generic;
using leap = Leap;
using n5y.LeapMotionSample.Limb;
using UnityEngine;

namespace n5y.LeapMotionSample
{
    public static class Frame2Arms
    {
        public static Arms Convert(leap.Frame frame)
        {
            if (frame.Hands.Count < 1)
            {
                return default;
            }

            var idFactory = new NodeIdFactory(NodeId.OriginNodeId);

            var ret = new Arms(idFactory.Create());

            CreateAndSetArm(frame.Hands[0], idFactory, ret);

            if (frame.Hands.Count == 1)
            {
                return ret;
            }

            CreateAndSetArm(frame.Hands[1], idFactory, ret);
            return ret;
        }

        public static void CreateAndSetArm(leap.Hand hand, INodeIdFactory idFactory, Arms arms)
        {
            var arm = LeapHand2Arm(hand, idFactory);
            if (hand.IsRight)
            {
                arms.Right = arm;
            }
            else
            {
                arms.Left = arm;
            }
        }

        public static Arm LeapHand2Arm(leap.Hand hand, INodeIdFactory idFactory)
        {
            var arm = new Arm();
            NodeId Id() => idFactory.Create();
            var elbowNode = new Node(Id()) {Position = hand.Arm.ElbowPosition.V3()};
            var wristNode = new Node(Id()) {Position = hand.Arm.WristPosition.V3()};
            var palmNode = new Node(Id()) {Position = hand.PalmPosition.V3()};
            var thumbs = new[]
            {
                new Node(Id()) {Position = hand.Fingers[0].Bone(leap.Bone.BoneType.TYPE_PROXIMAL).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[0].Bone(leap.Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[0].Bone(leap.Bone.BoneType.TYPE_DISTAL).NextJoint.V3()}
            };
            var indexes = new[]
            {
                new Node(Id()) {Position = hand.Fingers[1].Bone(leap.Bone.BoneType.TYPE_PROXIMAL).NextJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[1].Bone(leap.Bone.BoneType.TYPE_PROXIMAL).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[1].Bone(leap.Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[1].Bone(leap.Bone.BoneType.TYPE_DISTAL).NextJoint.V3()}
            };
            var middles = new[]
            {
                new Node(Id()) {Position = hand.Fingers[2].Bone(leap.Bone.BoneType.TYPE_PROXIMAL).NextJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[2].Bone(leap.Bone.BoneType.TYPE_PROXIMAL).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[2].Bone(leap.Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[2].Bone(leap.Bone.BoneType.TYPE_DISTAL).NextJoint.V3()}
            };
            var rings = new[]
            {
                new Node(Id()) {Position = hand.Fingers[3].Bone(leap.Bone.BoneType.TYPE_PROXIMAL).NextJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[3].Bone(leap.Bone.BoneType.TYPE_PROXIMAL).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[3].Bone(leap.Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[3].Bone(leap.Bone.BoneType.TYPE_DISTAL).NextJoint.V3()}
            };
            var pinkies = new[]
            {
                new Node(Id()) {Position = hand.Fingers[4].Bone(leap.Bone.BoneType.TYPE_PROXIMAL).NextJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[4].Bone(leap.Bone.BoneType.TYPE_PROXIMAL).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[4].Bone(leap.Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint.V3()},
                new Node(Id()) {Position = hand.Fingers[4].Bone(leap.Bone.BoneType.TYPE_DISTAL).NextJoint.V3()}
            };

            elbowNode.Backward = new[] {arm};
            elbowNode.Forward = new[] {wristNode};
            wristNode.Backward = new[] {elbowNode};
            wristNode.Forward = new[] {palmNode};
            palmNode.Backward = new[] {wristNode};
            palmNode.Forward = new[] {thumbs[0], indexes[0], middles[0], rings[0], pinkies[0]};
            SetFingerNodes(thumbs, palmNode);
            SetFingerNodes(indexes, palmNode);
            SetFingerNodes(middles, palmNode);
            SetFingerNodes(rings, palmNode);
            SetFingerNodes(pinkies, palmNode);

            arm.Elbow = elbowNode;
            arm.Wrist = wristNode;
            arm.Rotation = hand.Arm.Rotation.Quat();
            arm.Hand = new Hand
            {
                Rotation = hand.Rotation.Quat(),
                Wrist = wristNode, Palm = palmNode,
                Thumbs = thumbs, Indexes = indexes, Middles = middles, Rings = rings, Pinkies = pinkies,
            };

            return arm;
        }

        public static void SetFingerNodes(IReadOnlyList<Node> fingerNodes, Node handNode)
        {
            if (fingerNodes == null || fingerNodes.Count == 0) return;
            fingerNodes[0].Backward = new[] {handNode};
            for (var i = 0; i < fingerNodes.Count - 1; i++)
            {
                var finger = fingerNodes[i];
                var nextFinger = fingerNodes[i + 1];
                finger.Forward = new[] {nextFinger};
                nextFinger.Backward = new[] {finger};
            }
        }
    }

    public static class LeapExtension
    {
        public static Vector3 V3(this leap.Vector v)
        {
            return new Vector3(v.x, v.y, v.z);
        }


        public static Quaternion Quat(this leap.LeapQuaternion q)
        {
            return new Quaternion(q.x, q.y, q.z, q.w);
        }
    }
}
