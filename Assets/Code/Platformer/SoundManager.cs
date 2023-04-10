using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer{
	public class SoundManager : MonoBehaviour
	{
		public static SoundManager instance;

		AudioSource audioSource;
		public AudioClip missSound;
		public AudioClip hitSound;
    	// Start is called before the first frame update
    	void Awake()
    	{
        	instance = this;
    	}

    	// Update is called once per frame
    	void Start()
    	{
        	audioSource = GetComponent<AudioSource>();
    	}

		public void PlaySoundHit()
		{
			audioSource.PlayOneShot(hitSound);
		}	

		public void PlaySoundMiss()
		{
			audioSource.PlayOneShot(missSound);
		}			

	}
}
