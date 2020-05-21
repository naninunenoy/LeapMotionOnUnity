using n5y.HumanoidFrame.Dto;
using n5y.HumanoidFrame.Humanoid.Abstract;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace n5y.HumanoidFrame.Humanoid.RiggingImplement
{
    public class RiggingHumanoidBody : IHumanoidBody
    {
        readonly Animator animator;
        readonly RigBuilder rigBuilder;
        
        public RiggingHumanoidBody(Animator animator, RigBuilder rigBuilder)
        {
            this.animator = animator;
            this.rigBuilder = rigBuilder;
        }
        
        
        public void ApplyArms(Arms arms)
        {
            
        }
    }
}
