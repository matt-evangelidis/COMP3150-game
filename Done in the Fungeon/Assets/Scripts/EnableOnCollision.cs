using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnCollision : MonoBehaviour
{
	public GameObject thingToActivate;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		thingToActivate.SetActive(true);
	}
}
