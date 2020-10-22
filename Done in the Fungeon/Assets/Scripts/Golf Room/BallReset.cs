using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{
	public Rigidbody2D rb2d;
	public Transform startPosition;
	private float knockback;
	public Damageable damageable;

	void Start()
	{
		knockback = damageable.knockbackResistance;
	}
	
    // Update is called once per frame
    void Update()
    {
        if(rb2d.velocity == Vector2.zero)
		{
			transform.position = startPosition.position;
			damageable.knockbackResistance = knockback;
		}
		else
		{
			damageable.knockbackResistance = 1;
		}
    }
}
