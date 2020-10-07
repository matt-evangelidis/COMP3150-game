using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButton : MonoBehaviour
{
	public bool pressed = false;
	public SpriteRenderer sprite;
	public Color currentColour;
	
	void Update()
	{
		if(pressed)
		{
			sprite.color = Color.green;
		}
		else
		{
			sprite.color = currentColour;
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
