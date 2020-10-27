using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleRoom2Manager : MonoBehaviour
{
	public DoubleButton button0;
	public DoubleButton button1;
	
	public GameObject walls;
	public GameObject lava;
	public GameObject otherPlayer;
	public GameObject red;
	public GameObject exitDoor;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(button0.pressed && button1.pressed)
		{
			walls.SetActive(false);
			lava.SetActive(false);
			otherPlayer.SetActive(false);
			red.SetActive(false);
			exitDoor.SetActive(false);
		}
    }
}
