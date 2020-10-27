using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableEntity : MonoBehaviour
{
	public float damageTime;
	private float damageTimer;
	private float knockbackSpeed;
	public float knockbackResistance = 0;
	
	private Vector3 knockbackVector;
	
	private Rigidbody2D rb2d;
	
	private CameraShake camShake;
	
	public int numHits;

	void Start()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
	}
	
    // Update is called once per frame
    void Update()
    {
        if(damageTimer >= 0)
		{
			damageTimer -= Time.deltaTime;
		}
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		EnemyDamager damager = c.gameObject.GetComponent<EnemyDamager>();
		if(damager != null)
		{
			camShake.Shake();
			if(damageTimer < 0)
			{
				damageTimer = damageTime;
				
				knockbackVector = transform.position - damager.source.position;
				knockbackVector = knockbackVector.normalized;
			
				knockbackSpeed = damager.knockbackPower;
				
				float resistance = 1 - knockbackResistance;
				
				if(resistance != 0)
				{
					numHits++;
				}
				
				rb2d.AddForce(knockbackVector * Time.deltaTime * knockbackSpeed * resistance, ForceMode2D.Impulse);
			}
		}
	}
}
