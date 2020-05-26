using n5y.HumanoidFrame.Dto;
using n5y.HumanoidFrame.Humanoid.Abstract;
using UnityEngine;

namespace n5y.HumanoidFrame.Humanoid.RiggingImplement
{
    public class RiggingHumanoidBody : IHumanoidBody
    {
        readonly Animator animator;
        readonly Transform leftHandIkTarget;
        readonly Transform rightHandIkTarget;
        
        public RiggingHumanoidBody(Animator animator, Transform leftHandIkTarget, Transform rightHandIkTarget)
        {
            this.animator = animator;
            this.leftHandIkTarget = leftHandIkTarget;
            this.rightHandIkTarget = rightHandIkTarget;
        }
        
        
        public void ApplyArms(Arms arms)
        {
            leftHandIkTarget.position = arms.Left.Hand.Palm.Position;
            rightHandIkTarget.position = arms.Right.Hand.Palm.Position;
        }
    }
}
