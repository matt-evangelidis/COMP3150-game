using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnHit : MonoBehaviour
{
    private AudioSource sounds;
	public AudioClip clip;
	
	void Awake()
	{
		sounds = GameObject.Find("/Sound Effects").GetComponent<AudioSource>();
	}

    void OnCollisionEnter2D(Collision2D c)
	{
		sounds.clip = clip;
		sounds.Play();
	}
}
