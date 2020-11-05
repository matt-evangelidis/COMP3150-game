using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    public GameObject[] doors;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			foreach(GameObject i in doors)
			{
				i.SetActive(true);
			}
			gameObject.SetActive(false);
		}
	}
}
