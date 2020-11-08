using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    private AudioSource sounds;
	public AudioClip clip;
	
	
	void Awake()
	{
		sounds = GameObject.Find("/Sound Effects").GetComponent<AudioSource>();
	}
	
    void OnEnable()
	{
		sounds.clip = clip;
		sounds.Play();
	}
}
