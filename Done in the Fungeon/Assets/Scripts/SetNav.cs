using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNav : MonoBehaviour
{
	private float disableTime = 1;
	public GenerateNav nav;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(disableTime > 0)
		{
			disableTime -= Time.deltaTime;
		}
		else
		{
			nav.generate();
			enabled = false;
		}
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "nav_enable")
		{
			c.gameObject.tag = "nav_disable";
		}
	}
}
