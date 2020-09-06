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
	private Rigidbody2D rb2d;
	
	private Vector3 knockbackVector;
	public float knockbackSpeed;
	private Damager damager;
	
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
			//transform.Translate(knockbackVector * knockbackSpeed * Time.deltaTime);
			rb2d.AddForce(knockbackVector * Time.deltaTime * knockbackSpeed);
			//Debug.DrawLine(transform.position, damager.source.position, Color.white, 10.0f, false);
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
