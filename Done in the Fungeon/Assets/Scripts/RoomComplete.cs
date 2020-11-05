using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComplete : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			GameObject player = GameObject.Find("/Player");
			player.GetComponent<LevelsComplete>().RoomComplete();
		}
	}
}
