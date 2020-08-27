using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
	
	public Transform enemy1;
	public Transform enemy2;
	public Transform enemy3;
	public Transform enemy4;
	public Transform enemy5;
	public Transform enemy6;
	
	private Vector3 e1;
	private Vector3 e2;
	private Vector3 e3;
	private Vector3 e4;
	private Vector3 e5;
	private Vector3 e6;
	
    // Start is called before the first frame update
    void Start()
    {
        e1 = enemy1.gameObject.transform.position;
		e2 = enemy2.gameObject.transform.position;
		e3 = enemy3.gameObject.transform.position;
		e4 = enemy4.gameObject.transform.position;
		e5 = enemy5.gameObject.transform.position;
		e6 = enemy6.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Reset")) {
			enemy1.gameObject.transform.position = e1;
			enemy2.gameObject.transform.position = e2;
			enemy3.gameObject.transform.position = e3;
			enemy4.gameObject.transform.position = e4;
			enemy5.gameObject.transform.position = e5;
			enemy6.gameObject.transform.position = e6;
		}
    }
}
