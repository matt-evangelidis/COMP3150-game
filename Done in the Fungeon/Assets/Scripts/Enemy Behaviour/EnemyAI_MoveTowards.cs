using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using node;
using System.Linq;

public class EnemyAI_MoveTowards : EnemyPathfinding
{
	
	public PlayerGridPos playerPos;
	
	public void Awake()
	{
		GameObject player = GameObject.Find("/Player");
		playerPos = player.GetComponent<PlayerGridPos>();
	}
	
    public override void Start()
    {
		base.Start();
    }

    public override void Update()
    {
		base.Update();
    }
	
	public override void recalculatePath()
	{
		startNode = currentlyOn;
		target = playerPos.currentlyOn;
		//nodeTarget = startNode; //Enemy movement is a little janky with this, dequeuing the first thing makes enemies cut corners, but they move smoother
		path = generatePath(startNode, target);
		nodeTarget = path.Dequeue();
	}
}
