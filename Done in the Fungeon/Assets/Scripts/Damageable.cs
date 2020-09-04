using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
	private SpriteRenderer sprite;
	public Color damageColour;
	public Color defaultColour;
	public float damageTime;
	private float damageTimer;
	
	private Vector3 knockbackVector;
	public float knockbackSpeed;
	public Damager damager;
	
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damageTimer > 0)
		{
			sprite.color = damageColour;
			damageTimer -= Time.deltaTime;
			transform.Translate(knockbackVector * knockbackSpeed * Time.deltaTime);
		} else {
			sprite.color = defaultColour;
		}
		
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		damager = c.gameObject.GetComponent<Damager>();
		if(damager != null)
		{
			damageTimer = damageTime;
			
			knockbackVector = transform.position - damager.source.position;
			knockbackVector = knockbackVector.normalized;
		}
	}
}
