using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBooster : MonoBehaviour
{
	public Booster booster;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		switch(booster.direction)
		{
			case 0:
				booster.direction = 1;
				booster.transform.rotation = Quaternion.Euler(0, 0, -90);
				break;
			case 1:
				booster.direction = 2;
				booster.transform.rotation = Quaternion.Euler(0, 0, 180);
				break;
			case 2:
				booster.direction = 3;
				booster.transform.rotation = Quaternion.Euler(0, 0, 90);
				break;
			case 3:
				booster.direction = 0;
				booster.transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
		}
	}
}
