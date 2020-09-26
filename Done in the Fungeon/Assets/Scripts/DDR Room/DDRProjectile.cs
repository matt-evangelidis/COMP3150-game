using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRProjectile : MonoBehaviour
{
	public Vector3 direction;
	private Vector3 currentDirection;
	
	public Transform defaultPos;
	
	public float speed;
	
    // Start is called before the first frame update
    void Start()
    {
        currentDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(currentDirection * speed * Time.deltaTime);
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "PlayerAttack")
		{
			currentDirection = direction;
		}
		
		if(c.gameObject.tag  == "Enemy")
		{
			Debug.Log("Poop");
			transform.position = defaultPos.position;
			currentDirection = Vector3.zero;
		}
	}
}
