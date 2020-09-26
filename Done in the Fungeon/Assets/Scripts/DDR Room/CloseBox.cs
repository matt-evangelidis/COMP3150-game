using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBox : MonoBehaviour
{
    public GameObject door;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		door.SetActive(true);
		gameObject.SetActive(false);
	}
}
