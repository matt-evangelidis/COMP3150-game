using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSumoEnemy : MonoBehaviour
{
    public EnemyAI_MoveTowards enemyAI;
	private float speed;
	
	void Start()
	{
		speed = enemyAI.movementSpeed;
		enemyAI.movementSpeed = 0;
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			enemyAI.movementSpeed = speed;
			gameObject.SetActive(false);
		}
	}
}
