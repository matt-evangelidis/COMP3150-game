using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridPos : MonoBehaviour
{
	public Transform currentlyOn;
	
	void start()
	{
		currentlyOn = transform;
	}
	
	void OnTriggerStay2D(Collider2D c)
	{
		if(c.gameObject.tag == "nav_enable")
		{
			currentlyOn = c.transform;
		}
	}
}
