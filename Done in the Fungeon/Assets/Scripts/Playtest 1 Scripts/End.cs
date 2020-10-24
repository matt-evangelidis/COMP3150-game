using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			c.gameObject.GetComponent<EventTracking>().playtestPanel.SetActive(true);
			c.gameObject.GetComponent<EventTracking>().end = true;
		}
	}
}
