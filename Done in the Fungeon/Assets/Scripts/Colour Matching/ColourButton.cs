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
				sr.color = new Color(0.9725490196078431f, 0.2196078431372549f, 0, 1); // red
				break;
			case 1:
				sr.color = new Color(0.9725490196078431f, 0.7215686274509804f, 0, 1); // yellow 
				break;
			case 2:
				sr.color = new Color(0, 0.7215686274509804f, 0, 1); // green
				break;
			case 3:
				sr.color = new Color(0, 0.4705882352941176f, 0.9725490196078431f, 1); // blue
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
				sr.color = new Color(0.9725490196078431f, 0.7215686274509804f, 0, 1);
				break;
			case 1:
				colour = 2;
				sr.color = new Color(0, 0.7215686274509804f, 0, 1);
				break;
			case 2:
				colour = 3;
				sr.color = new Color(0, 0.4705882352941176f, 0.9725490196078431f, 1);
				break;
			case 3:
				colour = 0;
				sr.color = new Color(0.9725490196078431f, 0.2196078431372549f, 0, 1);
				break;
		}
	}
}
