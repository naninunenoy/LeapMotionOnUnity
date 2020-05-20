using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Leap;
using UniRx;
using Leap.Unity;
using n5y.HumanoidFrame.Dto;
using n5y.HumanoidFrame.Abstruct;

namespace n5y.HumanoidFrame.LeapMotionImplement
{
    public class LeapMotionArmsFrameUpdate : IArmsFrameUpdate
    {
        readonly LeapServiceProvider leapMotion;
        
        public LeapMotionArmsFrameUpdate(LeapServiceProvider leapMotion)
        {
            this.leapMotion = leapMotion;
        }
        
        public IUniTaskAsyncEnumerable<Arms> ArmsUpdateAsync()
        {
            var controller = leapMotion.GetLeapController();
            return Observable
                .FromEventPattern<EventHandler<FrameEventArgs>, FrameEventArgs>(
                    h => h, 
                    h => controller.FrameReady += h,
                    h => controller.FrameReady -= h)
                .Select(x => x.EventArgs.frame)
                .Select(Frame2Arms.Convert)
                .ToUniTaskAsyncEnumerable();
        }
    }
}
