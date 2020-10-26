using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadButtons : MonoBehaviour
{
	public DoubleButton button0;
	public DoubleButton button1;
	public DoubleButton button2;
	public DoubleButton button3;
	
	public GameObject walls;
	public GameObject clone1;
	public GameObject clone2;
	public GameObject clone3;
	public GameObject exitDoor;
	public GameObject ballSlower1;
	public GameObject ballSlower2;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(button0.pressed && button1.pressed && button2.pressed && button3.pressed)
		{
			walls.SetActive(false);
			clone1.SetActive(false);
			clone2.SetActive(false);
			clone3.SetActive(false);
			exitDoor.SetActive(false);
			ballSlower1.SetActive(false);
			ballSlower2.SetActive(false);
		}
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		button0.gameObject.SetActive(true);
		button1.gameObject.SetActive(true);
		button3.gameObject.SetActive(true);
		walls.SetActive(true);
		clone1.SetActive(true);
		clone2.SetActive(true);
		clone3.SetActive(true);
		ballSlower1.SetActive(true);
		ballSlower2.SetActive(true);
	}
}
