using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
	public GameObject button0;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public GameObject slower0;
	public GameObject slower1;
	public GameObject clone0;
	public GameObject clone1;
	public GameObject clone2;
	public GameObject exitDoor;
	public GameObject walls;
	

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("r") && Input.GetKey("right shift"))
		{
			button0.SetActive(false);
			button1.SetActive(false);
			button2.SetActive(false);
			button3.SetActive(false);
			slower0.SetActive(false);
			slower1.SetActive(false);
			clone0.SetActive(false);
			clone1.SetActive(false);
			clone2.SetActive(false);
			exitDoor.SetActive(false);
			walls.SetActive(false);
		}
    }
}
