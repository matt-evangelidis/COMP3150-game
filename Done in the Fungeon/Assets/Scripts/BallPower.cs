using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPower : MonoBehaviour
{
	public Rigidbody2D rb2d;
	public EnemyDamager enemyDamager;
	public float minimumDamageSpeed;
	public float damageModifier;
	public float topSpeed;
	public float knockbackPower;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyDamager.damage = rb2d.velocity.magnitude/topSpeed * damageModifier;
		enemyDamager.knockbackPower = rb2d.velocity.magnitude/topSpeed * knockbackPower;
		
		//if the ball is moving too slowly, disable its damager so enemies won't be damaged by a still ball
		if(rb2d.velocity.magnitude < minimumDamageSpeed)
		{
			enemyDamager.gameObject.SetActive(false);
		}
		else
		{
			enemyDamager.gameObject.SetActive(true);
		}
    }
}
