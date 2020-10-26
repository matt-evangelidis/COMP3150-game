using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHazardOnCollision : MonoBehaviour
{
    public EnemyAI_Patrol patrol;
	private float speed;
	
	void Start()
	{
		speed = patrol.movementSpeed;
		patrol.movementSpeed = 0;
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		patrol.movementSpeed = speed;
	}
}
