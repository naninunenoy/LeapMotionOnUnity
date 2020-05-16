using Leap;
using Leap.Unity;
using UnityEngine;
using UniRx;

namespace n5y.LeapMotionAdaper
{
    public class LeapMotionRxController : MonoBehaviour
    {
        [SerializeField] LeapServiceProvider leapMotion = default;

        ReactiveProperty<Image> leapImage;
        public IReadOnlyReactiveProperty<Image> LeapImage => leapImage;
        ReactiveProperty<(long, Frame)> leapFrame;
        public IReadOnlyReactiveProperty<(long, Frame)> LeapFrame => leapFrame;

        public void SetCameraPrivacyPolicy()
        {
            leapMotion.GetLeapController().SetPolicy(Controller.PolicyFlag.POLICY_IMAGES);
        }
        
        public void Listen()
        {
            leapImage?.Dispose();
            leapFrame?.Dispose();
            leapImage = new ReactiveProperty<Image>();
            leapFrame = new ReactiveProperty<(long, Frame)>();
            leapMotion.OnUpdateFrame += LeapMotionOnOnUpdateFrame;
            leapMotion.GetLeapController().ImageReady += LeapControllerOnImageReady;
        }

        public void Unlisten()
        {
            leapImage?.Dispose();
            leapFrame?.Dispose();
            leapImage = null;
            leapFrame = null;
            if (leapMotion != null)
            {
                leapMotion.OnUpdateFrame -= LeapMotionOnOnUpdateFrame;
                if (leapMotion.GetLeapController() != null)
                {
                    leapMotion.GetLeapController().ImageReady -= LeapControllerOnImageReady;
                }
            }
        }

        void LeapControllerOnImageReady(object sender, ImageEventArgs e)
        {
            if (e.image == null) return;
            leapImage.Value = e.image;
        }

        void LeapMotionOnOnUpdateFrame(Frame e)
        {
            leapFrame.Value = (e.Timestamp, e);
        }
    }
}
