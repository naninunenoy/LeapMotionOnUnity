using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using n5y.HumanoidFrame.Sensor.LeapMotionImplement;
using UnityEngine;
using Cysharp.Threading.Tasks;
using n5y.HumanoidFrame.Humanoid.RiggingImplement;

namespace n5y.LeapMotionAdapter
{
    public class SampleScene : MonoBehaviour
    {
        [SerializeField] LeapServiceProvider leapMotion = default;
        [SerializeField] Animator animator = default;
        [SerializeField] Transform leftHand = default;
        [SerializeField] Transform rightHand = default;
        async void Start()
        {
            var leap = new LeapMotionArmsFrameUpdate(leapMotion);
            var humanoid = new RiggingHumanoidBody(animator, leftHand, rightHand);
        }
    }
}
