using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRDamageZone : MonoBehaviour
{
	public float damageTime;
	private float damageTimer;
	
	public GameObject damageZone;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(damageTimer > 0)
		{
			damageZone.SetActive(true);
			damageTimer -= Time.deltaTime;
		}
		else
		{
			damageZone.SetActive(false);
		}
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Enemy")
		{
			damageTimer = damageTime;
			DDRArrowSpawner.ReturnToPool(c.gameObject);
		}
	}
}
