using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {
        private AudioSource audioSource;

        public string Name;

        void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
            SoundManager.Instance.PlayClip(audioSource);
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

        }
    }
}
