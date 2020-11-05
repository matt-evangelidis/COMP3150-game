using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
	private SpriteRenderer sr;
	private bool collected = false;
	public bool Collected
	{
		get
		{
			return collected;
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
       sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		collected = true;
		sr.enabled = false;
	}
}
