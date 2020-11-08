using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    private AudioSource sounds;
	public AudioClip clip;
	
	void Awake()
	{
		sounds = GameObject.Find("/Sound Effects").GetComponent<AudioSource>();
	}

    void OnTriggerEnter2D(Collider2D c)
	{
		sounds.clip = clip;
		sounds.Play();
	}
}
