using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
	public Transform source; // the point where the damager's knockback will originate from
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnCollisionEnter2D(Collision2D c) { 
		if(c.gameObject.GetComponent<Damageable>() != null) {
			Debug.Log(c.gameObject.name);
		}
		Debug.Log("Collision");
	}
}
