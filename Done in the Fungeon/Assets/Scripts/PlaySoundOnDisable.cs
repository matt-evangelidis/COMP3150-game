using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnDisable : MonoBehaviour
{
	private AudioSource sounds;
	public AudioClip clip;
	
	
	void Awake()
	{
		sounds = GameObject.Find("/Sound Effects").GetComponent<AudioSource>();
	}
	
    void OnDisable()
	{
		sounds.clip = clip;
		sounds.Play();
	}
}
