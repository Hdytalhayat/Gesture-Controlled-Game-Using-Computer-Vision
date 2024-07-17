using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Base.Interact
{
    public class AudioPlayer : MonoBehaviour
    {
        public AudioSource audioSource; // This is for the first sound
        [SerializeField] private List<AudioClip> _audioLists;
        public UnityEvent onOpenEvent;
        public UnityEvent onCloseEvent;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            // _audioLists = GetComponentInParent<DoorAdventure>().audioList;
        }

        public void PlayOpenSfx()
        {
            if (_audioLists[0] != null)
            {
                audioSource.PlayOneShot(_audioLists[0]);
            }
            onOpenEvent?.Invoke();
        }

        public void PlayCloseSfx()
        {
            if (_audioLists[1] != null)
            {
                audioSource.PlayOneShot(_audioLists[1]);
            }

            onCloseEvent?.Invoke();
        }

        public void PlayLockedSfx()
        {
            if (_audioLists[2] != null)
            {
                audioSource.PlayOneShot(_audioLists[2]);
            }
         
        }

        public void PlayCreakSfx()
        {
            if (_audioLists[3] != null)
            {
                audioSource.PlayOneShot(_audioLists[3]);
            }
        }

        public void PlaySlamDoor()
        {
            if (_audioLists[4] != null)
            {
                audioSource.PlayOneShot(_audioLists[4]);
            }
          
        }

    }


}