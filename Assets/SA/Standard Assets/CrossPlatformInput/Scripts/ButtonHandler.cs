using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {
        private AudioSource ButtonPressedaudioSource;

        public string Name;

        void OnEnable()
        {
          
        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
            SoundManager.Instance.PlayClip(ButtonPressedaudioSource);
        }


        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {
              ButtonPressedaudioSource = GetComponent<AudioSource>();
        }
    }
}
