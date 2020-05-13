using System;
using Leap.Unity.Encoding;
using UnityEngine;
using UniRx;

namespace n5y.LeapMotionSample
{
    public class LeapMotionMain : MonoBehaviour
    {
        [SerializeField] LeapMotionRxController leapMotionController = default;
        [SerializeField] RenderTexture leftCameraRenderTexture = default;
        [SerializeField] RenderTexture rightCameraRenderTexture;
        
        Texture2D leftTexture = default;
        Texture2D rightTexture = default;
        
        void Start()
        {
            leapMotionController.SetCameraPrivacyPolicy();
            leapMotionController.Listen();

            leapMotionController
                .LeapFrame
                .SkipLatestValueOnSubscribe()
                .Select(x => x.Item2)
                .Subscribe(x =>
                {
                    Debug.Log(x.Hands.Count);
                })
                .AddTo(this);

            leapMotionController
                .LeapImage
                .SkipLatestValueOnSubscribe()
                .First()
                .Subscribe(x =>
                {
                    leftTexture = new Texture2D(x.Width, x.Height, TextureFormat.R8, false);
                    rightTexture = new Texture2D(x.Width, x.Height, TextureFormat.R8, false);
                })
                .AddTo(this);
            
            leapMotionController
                .LeapImage
                .SkipLatestValueOnSubscribe()
                .Skip(1)
                .Subscribe(x =>
                {
                    leftTexture.LoadRawTextureData(x.Data(Leap.Image.CameraType.LEFT));
                    leftTexture.Apply();
                    Graphics.Blit(leftTexture, leftCameraRenderTexture);
                    rightTexture.LoadRawTextureData(x.Data(Leap.Image.CameraType.RIGHT));
                    rightTexture.Apply();
                    Graphics.Blit(rightTexture, rightCameraRenderTexture);
                })
                .AddTo(this);
        }

        void OnDestroy()
        {
            leapMotionController.Unlisten();
        }
    }
}