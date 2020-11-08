using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButton : MonoBehaviour
{
	public bool pressed = false;
	public GameObject unpressedSprite;
	public GameObject pressedSprite;
	
	void Update()
	{
		if(pressed)
		{
			pressedSprite.SetActive(true);
			unpressedSprite.SetActive(false);
		}
		else
		{
			pressedSprite.SetActive(false);
			unpressedSprite.SetActive(true);
		}
	}
	
	void OnTriggerStay2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			pressed = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			pressed = false;
		}
	}
}
