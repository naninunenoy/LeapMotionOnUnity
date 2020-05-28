using n5y.HumanoidFrame.Dto;
using n5y.HumanoidFrame.Humanoid.Abstract;
using UnityEngine;

namespace n5y.HumanoidFrame.Humanoid.RiggingImplement
{
    public class RiggingHumanoidBody : IHumanoidBody
    {
        readonly Vector3 leapMotionBase;
        readonly Transform leftHandIkTarget;
        readonly Transform rightHandIkTarget;

        public RiggingHumanoidBody(Vector3 leapMotionBase, Transform leftHandIkTarget, Transform rightHandIkTarget)
        {
            this.leapMotionBase = leapMotionBase;
            this.leftHandIkTarget = leftHandIkTarget;
            this.rightHandIkTarget = rightHandIkTarget;
        }


        public void ApplyArms(Arms arms)
        {
            var hand = arms?.Left?.Hand;
            if (hand != null)
            {
                leftHandIkTarget.position = hand.Palm.Position.ToUnityLeftHand() + leapMotionBase;
                leftHandIkTarget.rotation = hand.Rotation.ToUnityLeftHand();
            }

            hand = arms?.Right?.Hand;
            if (hand != null)
            {
                rightHandIkTarget.position = hand.Palm.Position.ToUnityLeftHand() + leapMotionBase;
                rightHandIkTarget.rotation = hand.Rotation.ToUnityLeftHand();
            }
        }
    }

    static class VectorLeftHand
    {
        public static Vector3 ToUnityLeftHand(this Vector3 v)
        {
            return new Vector3(v.x, v.y, -v.z) / 100.0F; // [mm]->[m]
        }
        public static Quaternion ToUnityLeftHand(this Quaternion q)
        {
            return new Quaternion(-q.x, q.y, -q.z, q.w);
        }
    }
}
