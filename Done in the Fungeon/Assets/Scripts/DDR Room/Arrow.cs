using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	public float speed;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "damager")
		{
			
		}
	}
}
