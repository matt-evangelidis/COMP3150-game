using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnCollision : MonoBehaviour
{
    public GameObject thingToDeactivate;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			thingToDeactivate.SetActive(false);
		}
	}
}
