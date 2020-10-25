using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourMatchingManager : MonoBehaviour
{
	public int[] coloursNeeded = new int[4];
	public ColourButton button0;
	public ColourButton button1;
	public ColourButton button2;
	public ColourButton button3;
	
	private SpriteRenderer[] displayBorders = new SpriteRenderer[4];
	public SpriteRenderer display0Border;
	public SpriteRenderer display0;
	public SpriteRenderer display1Border;
	public SpriteRenderer display1;
	public SpriteRenderer display2Border;
	public SpriteRenderer display2;
	public SpriteRenderer display3Border;
	public SpriteRenderer display3;
	
	public GameObject exitDoor;
	
    // Start is called before the first frame update
    void Start()
    {
		displayBorders[0] = display0Border;
		displayBorders[1] = display1Border;
		displayBorders[2] = display2Border;
		displayBorders[3] = display3Border;
		
		for(int i = 0;i<coloursNeeded.Length;i++)
		{
			switch(coloursNeeded[i])
			{
				case 0:
					displayBorders[i].color = Color.red;
					break;
				case 1:
					displayBorders[i].color = Color.yellow;
					break;
				case 2:
					displayBorders[i].color = Color.green;
					break;
				case 3:
					displayBorders[i].color = Color.blue;
					break;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(button0.colour == coloursNeeded[0] && button1.colour == coloursNeeded[1] && button2.colour == coloursNeeded[2] && button3.colour == coloursNeeded[3])
		{
			exitDoor.SetActive(false);
			display0Border.gameObject.SetActive(false);
			display1Border.gameObject.SetActive(false);
			display2Border.gameObject.SetActive(false);
			display3Border.gameObject.SetActive(false);
			display0.gameObject.SetActive(false);
			display1.gameObject.SetActive(false);
			display2.gameObject.SetActive(false);
			display3.gameObject.SetActive(false);
		}
		
		display0.color = button0.sr.color;
		display1.color = button1.sr.color;
		display2.color = button2.sr.color;
		display3.color = button3.sr.color;
    }
}
