using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenAttackedByPlayer : MonoBehaviour
{
    private AudioSource sounds;
	public AudioClip clip;
	
	void Awake()
	{
		sounds = GameObject.Find("/Sound Effects").GetComponent<AudioSource>();
	}

    void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "PlayerAttack")
		{
			sounds.clip = clip;
			sounds.Play();
		}
	}
}
