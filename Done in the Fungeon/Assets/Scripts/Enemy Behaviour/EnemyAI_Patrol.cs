using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Patrol : EnemyPathfinding
{
	public Transform[] patrolZones;
	private int patrolCounter;
	
    public override void Start()
    {
		base.Start();
		patrolCounter = 0;
    }

    public override void Update()
    {
		if(currentlyOn != target)
		{
			movement(false);
		}
		else
		{
			recalculatePath();
		}
    }
	
	public override void recalculatePath()
	{
		patrolCounter++;
		patrolCounter = patrolCounter%patrolZones.Length;
		startNode = currentlyOn;
		target = patrolZones[patrolCounter];
		nodeTarget = startNode; //Enemy movement is a little janky with this, dequeuing the first thing makes enemies cut corners, but they move smoother
		//Debug.Log(nodeTarget.gameObject.name);
		path = generatePath(startNode, target);
		
	}
}
