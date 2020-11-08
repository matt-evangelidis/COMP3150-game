using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	private float explosionTime = 0.1f;
	
	private AudioSource sounds;
	public AudioClip clip;
	
	void Awake()
	{
		sounds = GameObject.Find("/Sound Effects").GetComponent<AudioSource>();
	}
	
    void Update()
    {
        if(explosionTime > 0)
		{
			explosionTime -= Time.deltaTime;
		}
		else
		{
			sounds.clip = clip;
			sounds.Play();
			Destroy(gameObject);
		}
    }
}
