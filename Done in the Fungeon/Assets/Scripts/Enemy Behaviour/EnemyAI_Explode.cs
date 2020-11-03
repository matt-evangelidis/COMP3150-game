using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Explode : MonoBehaviour
{
	public float fuse;
	private float fuseTimer;
	
	public GameObject flashingSprite;
	
	public float damageZoneTime;
	private float damageZoneTimer;
	
	private CameraShake camShake;
	
	private SpriteRenderer sprite;
	
	public Explosion explosion;
	
	void Start()
	{
		camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
		sprite = gameObject.GetComponent<SpriteRenderer>();
	}
	
    void OnEnable()
    {
        fuseTimer = fuse;
		damageZoneTimer = damageZoneTime;
    }
	
	void OnDisable()
	{
		flashingSprite.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        if(fuseTimer > 0)
		{
			fuseTimer -= Time.deltaTime;
			
			float flashRate = (fuseTimer/fuse)*6 + 3;
			
			if((int)(fuseTimer*16)%(int)flashRate == 0)
			{
				flashingSprite.SetActive(true);
			}
			else
			{
				flashingSprite.SetActive(false);
			}
		}
		else
		{
			camShake.StrongShake();
			Explosion e = Instantiate(explosion);
			e.transform.position = transform.position;
			Destroy(gameObject);
		}
    }
}
