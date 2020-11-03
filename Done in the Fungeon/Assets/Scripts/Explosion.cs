using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	private float explosionTime = 0.1f;
	
    void Update()
    {
        if(explosionTime > 0)
		{
			explosionTime -= Time.deltaTime;
		}
		else
		{
			Destroy(gameObject);
		}
    }
}
