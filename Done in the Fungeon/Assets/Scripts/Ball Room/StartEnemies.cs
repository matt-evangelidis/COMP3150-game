using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemies : MonoBehaviour
{
	// temporary, delete this script later
	public EnemyAI_MoveTowards enemy1;
	public EnemyAI_MoveTowards enemy2;
	public EnemyAI_MoveTowards enemy3;
	public EnemyAI_MoveTowards enemy4;
	public EnemyAI_MoveTowards enemy5;
	public EnemyAI_MoveTowards enemy6;
	public EnemyAI_MoveTowards enemy7;
	public EnemyAI_MoveTowards enemy8;
	
	void Start()
	{
		enemy1.movementSpeed = 0;
		enemy2.movementSpeed = 0;
		enemy3.movementSpeed = 0;
		enemy4.movementSpeed = 0;
		enemy5.movementSpeed = 0;
		enemy6.movementSpeed = 0;
		enemy7.movementSpeed = 0;
		enemy8.movementSpeed = 0;
		enemy1.playerPos = GameObject.Find("/Player").GetComponent<PlayerGridPos>();
		enemy2.playerPos = GameObject.Find("/Player").GetComponent<PlayerGridPos>();
		enemy3.playerPos = GameObject.Find("/Player").GetComponent<PlayerGridPos>();
		enemy4.playerPos = GameObject.Find("/Player").GetComponent<PlayerGridPos>();
		enemy5.playerPos = GameObject.Find("/Player").GetComponent<PlayerGridPos>();
		enemy6.playerPos = GameObject.Find("/Player").GetComponent<PlayerGridPos>();
		enemy7.playerPos = GameObject.Find("/Player").GetComponent<PlayerGridPos>();
		enemy8.playerPos = GameObject.Find("/Player").GetComponent<PlayerGridPos>();
	}
	
    void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			enemy1.movementSpeed = 3000;
			enemy2.movementSpeed = 3000;
			enemy3.movementSpeed = 3000;
			enemy4.movementSpeed = 3000;
			enemy5.movementSpeed = 3000;
			enemy6.movementSpeed = 3000;
			enemy7.movementSpeed = 3000;
			enemy8.movementSpeed = 3000;
		}
	}
}
