using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSlower : MonoBehaviour
{
	private float magnitude;
	private Vector3 currentVector;
	public float deceleration;
	private bool on;
	private float timer;
	public float disableTime = 0.1f;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		on = true;
	}
	
	void OnTriggerStay2D(Collider2D c)
	{
		magnitude = c.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
		if(magnitude > 0 && on)
		{
			currentVector = c.gameObject.GetComponent<Rigidbody2D>().velocity;
			currentVector = currentVector.normalized;
			if(magnitude > 0.01f)
			{
				magnitude -= magnitude * deceleration * Time.deltaTime;
			}
			else
			{
				magnitude = 0;
			}
			c.gameObject.GetComponent<Rigidbody2D>().velocity = currentVector * magnitude;
		}
		
		if(c.gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
		{
			on = false;
			timer = disableTime;
		}
		else
		{
			timer -= Time.deltaTime;
			if(timer < 0)
			{
				on = true;
			}
		}
	}
}
