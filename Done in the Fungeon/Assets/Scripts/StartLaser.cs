using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLaser : MonoBehaviour
{
	public Sprite onSprite;
	public Sprite offSprite;
	public Damager damager;
	public SpriteRenderer sr;
	
    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "LaserStarter")
		{
			sr.sprite = onSprite;
			damager.enabled = true;
			transform.parent.gameObject.tag = "damager";
			gameObject.tag = "Untagged";
		}
	}
}
