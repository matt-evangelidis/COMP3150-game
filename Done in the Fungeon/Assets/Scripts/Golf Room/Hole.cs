using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
	public GameObject exitDoor;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		c.gameObject.SetActive(false);
		exitDoor.SetActive(false);
	}
}
