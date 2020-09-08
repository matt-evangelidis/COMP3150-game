using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using node;
using System.Linq;

public class EnemyAI_MoveTowards : MonoBehaviour
{
	public GenerateNav nav;
	public Node target;
	private List<List<Node>> grid;
	public Node startNode;
	public Transform dot;
	private Transform nodeTarget;
	private Transform currentlyOn;
	public float newPathTime;
	private float newPathTimer;
	private Queue<Transform> path;
	
	private Vector3 movementVector;
	private Rigidbody2D rb2d;
	public float movementSpeed;
	
    // Start is called before the first frame update
    void Start()
    {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		
		grid = nav.getGrid();
		startNode = grid[0][0];
		//startNode = findNode(currentlyOn); // the issue here is that this runs before it even knows where it is
		target = grid[13][13];
		
		path = generatePath(startNode, target);
		nodeTarget = path.Dequeue();
		
		// for debugging
		/*for(int i = 0;i<path.Count;i++)
		{
			Transform d = Instantiate(dot);
			d.position = path[i].position;
		}*/
		
		newPathTimer = newPathTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(newPathTimer > 0)
		{
			newPathTimer -= Time.deltaTime;
			//startNode = findNode(currentlyOn);
			//path = generatePath(startNode, target);
		}
		else
		{
			if(!currentlyOn.Equals(target.position)) {
				if(currentlyOn.Equals(nodeTarget))
				{
					nodeTarget = path.Dequeue();
				}
				
				movementVector = nodeTarget.position - currentlyOn.position;
				movementVector = movementVector.normalized;
				rb2d.AddForce(movementVector * Time.deltaTime * movementSpeed);
			}
			
			// move towards the next node
			// when the node is touched, pop the first item in the queue off and make that the next target
		}
    }
	
	// Code logic from https://www.peachpit.com/articles/article.aspx?p=101142
	private Queue<Transform> generatePath(Node sta, Node tar)
	{
		Queue<Node> toVisit = new Queue<Node>();
		List<Node> visited = new List<Node>();
		
		if(sta.Equals(tar))
		{
			return buildPath(sta);
		}
		else
		{
			sta.parent = null;
			visited.Add(sta);
			for(int i = 0;i<sta.neighbours.Count;i++)
			{
				sta.neighbours[i].parent = sta;
				toVisit.Enqueue(sta.neighbours[i]);
			}
		}
		
		while(toVisit.Count > 0) // I should be using .Any here, but it doesn't work for some reason
		{
			Node currentNode = toVisit.Dequeue();
			
			if(currentNode.Equals(tar))
			{
				return buildPath(currentNode);
			}
			else
			{
				visited.Add(currentNode);
				
				for(int i = 0;i<currentNode.neighbours.Count;i++)
				{
					if(!visited.Contains(currentNode.neighbours[i]) && !toVisit.Contains(currentNode.neighbours[i]))
					{
						currentNode.neighbours[i].parent = currentNode;
						toVisit.Enqueue(currentNode.neighbours[i]);
					}
				}
			}
		}
		
		return null;
	}
	
	private Queue<Transform> buildPath(Node n)
	{
		Queue<Transform> path = new Queue<Transform>();
		while(n.parent != null)
		{
			path.Enqueue(n.position); // maybe make it game objects instead of transforms, we'll see
			n = n.parent;
		}
		path = new Queue<Transform>(path.Reverse());
		return path;
	}
	
	void OnTriggerStay2D(Collider2D c)
	{
		if(c.gameObject.tag == "nav_enable")
		{
			currentlyOn = c.transform;
		}
	}
	
	/*private Node findNode(Transform t) {
		for(int i = 0;i<grid.Count;i++)
		{
			for(int j = 0;j<grid[i].Count;j++)
			{
				if(t.Equals(grid[i][j].position))
				{
					return grid[i][j];
				}
			}
		}
		return null;
	}*/
}
