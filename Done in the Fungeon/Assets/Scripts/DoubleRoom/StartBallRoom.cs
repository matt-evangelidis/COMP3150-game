using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBallRoom : MonoBehaviour
{
	public DoubleRoomSpawner doubleRoomSpawner;
	public GameObject otherPlayer;
	
    void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			doubleRoomSpawner.started = true;
			otherPlayer.SetActive(true);
		}
	}
}
