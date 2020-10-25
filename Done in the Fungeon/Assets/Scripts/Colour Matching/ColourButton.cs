using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourButton : MonoBehaviour
{
	public int colour;
	public SpriteRenderer sr;
	
    // Start is called before the first frame update
    void Start()
    {
		sr = gameObject.GetComponent<SpriteRenderer>();
		
        switch(colour)
		{
			case 0:
				sr.color = Color.red;
				break;
			case 1:
				sr.color = Color.yellow;
				break;
			case 2:
				sr.color = Color.green;
				break;
			case 3:
				sr.color = Color.blue;
				break;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		switch(colour)
		{
			case 0:
				colour = 1;
				sr.color = Color.yellow;
				break;
			case 1:
				colour = 2;
				sr.color = Color.green;
				break;
			case 2:
				colour = 3;
				sr.color = Color.blue;
				break;
			case 3:
				colour = 0;
				sr.color = Color.red;
				break;
		}
	}
}
