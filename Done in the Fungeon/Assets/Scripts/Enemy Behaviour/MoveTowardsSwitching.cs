using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For chasing enemies, they're not very good at hitting the player with regular targeting alone, so I'll have it so that enemies will move straight towards the player if they are within a certain distance.
public class MoveTowardsSwitching : MonoBehaviour
{
	public Transform player;
	public EnemyAI_MoveTowards pathfinding;
	public EnemyAI_TargetPlayer chase;
	public float switchDistance;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < switchDistance)
		{
			chase.enabled = true;
			pathfinding.enabled = false;
		}
		else
		{
			chase.enabled = false;
			pathfinding.enabled = true;
		}
    }
}
