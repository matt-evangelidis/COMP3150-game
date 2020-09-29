using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
	public float health = 5;
	public bool invincible;
	
	private SpriteRenderer sprite;
	public Color damageColour;
	public Color defaultColour;
	public float damageTime;
	private float damageTimer;
	private Rigidbody2D rb2d;
	
	private Vector3 knockbackVector;
	private float knockbackSpeed;
	private EnemyDamager damager;
	
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damageTimer > 0)
		{
			sprite.color = damageColour;
			damageTimer -= Time.deltaTime;
			rb2d.AddForce(knockbackVector * Time.deltaTime * knockbackSpeed);
		} else {
			sprite.color = defaultColour;
		}
		
		if(health < 0)
		{
			Destroy(gameObject);
		}
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		damager = c.gameObject.GetComponent<EnemyDamager>();
		if(damager != null)
		{
			if(!invincible)
			{
				health -= damager.damage;
			}
			
			damageTimer = damageTime;
			
			knockbackVector = transform.position - damager.source.position;
			knockbackVector = knockbackVector.normalized;
			
			knockbackSpeed = damager.knockbackPower;
		}
	}
}
