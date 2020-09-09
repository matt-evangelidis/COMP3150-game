using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
	public Transform sourceTransform;
	public Vector3 source; // the point where the damager's knockback will originate from
	public float knockbackPower;
	public bool isProjectile;
	public bool disableSource; // disable this if the damage soruce is managed elsewhere (like for lasers)
	
    // Start is called before the first frame update
    void Start()
    {
        source = sourceTransform.position;
    }
	
	void Awake()
	{
		source = sourceTransform.position;
	}

    // Update is called once per frame
    void Update()
    {
		if(!disableSource)
		{
			source = sourceTransform.position;
		}
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		
	}
}
