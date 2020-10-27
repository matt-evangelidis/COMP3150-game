using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoom2Starter : MonoBehaviour
{
	public EnemyAI_Patrol enemy0;
	public EnemyAI_Patrol enemy1;
	
	private float speed0;
	private float speed1;
	
    // Start is called before the first frame update
    void Start()
    {
        speed0 = enemy0.movementSpeed;
		speed1 = enemy1.movementSpeed;
		
		enemy0.movementSpeed = 0;
		enemy1.movementSpeed = 0;
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		enemy0.movementSpeed = speed0;
		enemy1.movementSpeed = speed1;
	}
}
