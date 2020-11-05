using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
	public GameObject[] exitDoors;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		c.gameObject.SetActive(false);
		foreach(GameObject i in exitDoors)
		{
			i.SetActive(false);
		}
	}
}
