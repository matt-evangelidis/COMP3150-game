using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfLevelComplete : MonoBehaviour
{
	public KnockableEntity entity;
	
    void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			c.gameObject.GetComponent<EventTracking>().addLevelCompletionEvent(entity.numHits.ToString());
		}
	}
}
