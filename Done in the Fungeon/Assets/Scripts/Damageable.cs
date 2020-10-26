using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
	public float health = 5;
	public bool invincible;
	private bool damageIframes;
	
	private SpriteRenderer sprite;
	public Color damageColour;
	public Color defaultColour;
	public float damageTime;
	private float damageTimer;
	private Rigidbody2D rb2d;
	public float knockbackResistance = 0;
	
	public bool immuneToPlayerAttacks;
	public bool iframesAfterDamage;
	
	public float iframeTime;
	private float iframeTimer;
	
	private Vector3 knockbackVector;
	private float knockbackSpeed;
	
	private CameraShake camShake;
	
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		
		camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damageTimer >= 0)
		{
			sprite.color = damageColour;
			damageTimer -= Time.deltaTime;
		} else {
			sprite.color = defaultColour;
		}
		
		if(health <= 0)
		{
			Destroy(gameObject);
		}
		
		if(iframeTimer > 0)
		{
			iframeTimer -= Time.deltaTime;
			damageIframes = true;
			if((int)(iframeTimer*8)%2 == 0)
			{
				sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
			}
			else
			{
				sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
			}
		}
		else
		{
			damageIframes = false;
		}
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		EnemyDamager damager = c.gameObject.GetComponent<EnemyDamager>();
		if(damager != null)
		{
			camShake.Shake();
			if(c.gameObject.tag == "PlayerAttack" && !immuneToPlayerAttacks && !invincible && !damageIframes)
			{
				health -= damager.damage;
				iframeTimer = iframeTime;
			}
			else if(c.gameObject.tag != "PlayerAttack" && !invincible && !damageIframes)
			{
				health -= damager.damage;
				iframeTimer = iframeTime;
			}
			
			if(damageTimer < 0  && !damageIframes)
			{
				damageTimer = damageTime;
				
				knockbackVector = transform.position - damager.source.position;
				knockbackVector = knockbackVector.normalized;
			
				knockbackSpeed = damager.knockbackPower;
				
				float resistance = 1 - knockbackResistance;
				
				rb2d.AddForce(knockbackVector * Time.deltaTime * knockbackSpeed * resistance, ForceMode2D.Impulse);
			}
		}
	}
}
