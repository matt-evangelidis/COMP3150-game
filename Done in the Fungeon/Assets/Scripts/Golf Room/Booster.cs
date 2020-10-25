using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
	public int direction; // up = 0, right = 1, down = 2, left = 3
	public float power;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		switch(direction)
		{
			case 0:
				c.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				c.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * power, ForceMode2D.Impulse);
				break;
			case 1:
				c.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				c.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * power, ForceMode2D.Impulse);
				break;
			case 2:
				c.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				c.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.down * power, ForceMode2D.Impulse);
				break;
			case 3:
				c.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				c.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.left * power, ForceMode2D.Impulse);
				break;
		}
	}
}
