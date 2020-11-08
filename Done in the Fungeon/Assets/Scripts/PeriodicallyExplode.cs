using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicallyExplode : MonoBehaviour
{
	public float explosionPeriod;
	private float explosionPeriodTimer;
	
	private float explosion = 0.1f;
	private float explosionTimer;
	
	public SpriteRenderer sprite;
	public Color defaultColour;
	public Color flashingColor;
	
	public GameObject damageZone1;
	public GameObject damageZone2;
	
	private float flashRate;
	
	private CameraShake camShake;
	
	private AudioSource sounds;
	public AudioClip clip;
	
	void Awake()
	{
		sounds = GameObject.Find("/Sound Effects").GetComponent<AudioSource>();
	}
	
    // Start is called before the first frame update
    void Start()
    {
        explosionPeriodTimer = explosionPeriod;
		camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if(explosionPeriodTimer > 0)
		{
			explosionPeriodTimer -= Time.deltaTime;
			
			flashRate = (explosionPeriodTimer/explosionPeriod)*6 + 3;
			
			if((int)(explosionPeriodTimer*16)%(int)flashRate == 0)
			{
				sprite.color = flashingColor;
			}
			else
			{
				sprite.color = defaultColour;
			}
		}
		else
		{
			camShake.StrongShake();
			explosionTimer = explosion;
			explosionPeriodTimer = explosionPeriod;
			sounds.clip = clip;
			sounds.Play();
		}
		
		if(explosionTimer > 0)
		{
			explosionTimer -= Time.deltaTime;
			damageZone1.SetActive(true);
			damageZone2.SetActive(true);
		}
		else
		{
			damageZone1.SetActive(false);
			damageZone2.SetActive(false);
		}
    }
}
