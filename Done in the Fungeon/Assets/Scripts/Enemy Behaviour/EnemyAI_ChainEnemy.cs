using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_ChainEnemy : EnemyPathfinding
{
    public EnemyPathfinding target;
	
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
		Transform follow = target.CurrentlyOn;
		//nodeTarget = startNode; //Enemy movement is a little janky with this, dequeuing the first thing makes enemies cut corners, but they move smoother
		path = generatePath(startNode, follow);
		nodeTarget = path.Dequeue();
	}
}
