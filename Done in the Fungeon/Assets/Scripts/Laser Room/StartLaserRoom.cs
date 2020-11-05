using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLaserRoom : MonoBehaviour
{
    public GameObject laserSpawner;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			laserSpawner.SetActive(true);
		}
	}
}
