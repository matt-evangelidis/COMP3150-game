using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallResetter : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D c)
	{
		c.gameObject.GetComponent<BallReset>().Reset();
	}
}
