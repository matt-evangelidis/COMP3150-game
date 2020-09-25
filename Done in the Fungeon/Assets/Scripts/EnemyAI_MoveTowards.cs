using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using node;
using System.Linq;

public class EnemyAI_MoveTowards : MonoBehaviour
{
	public GenerateNav nav;
	public Transform target;
	private List<List<Transform>> grid;
	public Transform startNode;
	public Transform dot;
	private Transform nodeTarget;
	private Transform currentlyOn;
	public float newPathTime; // Time between path updates
	private float newPathTimer;
	private Queue<Transform> path;
	public PlayerGridPos playerPos;
	
	private Vector2 movementVector;
	private Rigidbody2D rb2d;
	public float movementSpeed;
	
	public Transform otherTestNode;
	
    // Start is called before the first frame update
    void Start()
    {
		currentlyOn = transform;
		nodeTarget = transform;
		
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		
		grid = nav.getGrid();
		//startNode = findNode(currentlyOn); // the issue here is that this runs before it even knows where it is
		
		/*for(int i = 0;i<grid.Count;i++)
		{
			for(int j = 0;j<grid[i].Count;j++)
			{
				for(int k = 0;k<grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Count;k++)
				{
					Debug.Log("(" + i + ", " + j + ") " + grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours[k].name);
				}
			}
		}*/
		
		path = generatePath(startNode, target);
		nodeTarget = startNode;
		
		Queue<Transform> testList = new Queue<Transform>(path);
		
		// for debugging
		for(int i = 0;i<path.Count;i++)
		{
			//Debug.Log(testList.Dequeue());
			Transform d = Instantiate(dot);
			d.position = testList.Dequeue().position;
			d.Translate(0,0,-2);
		}
		
		newPathTimer = newPathTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(newPathTimer > 0)
		{
			newPathTimer -= Time.deltaTime;
			
			// these are necessary because these distance things take the z coordinate into account and there are some
			// weird discrepencies there
			Vector2 transform2d = transform.position;
			Vector2 nodeTarget2d = nodeTarget.position;
			
			if(Vector3.Distance(transform2d, nodeTarget2d) > 0.1f)
			{
				movementVector = nodeTarget2d - transform2d;
				movementVector = movementVector.normalized;
				
				rb2d.AddForce(movementVector * Time.deltaTime * movementSpeed);
			}
			else
			{
				nodeTarget = path.Dequeue();
			}
			
			// move towards the next node
			// when the node is touched, pop the first item in the queue off and make that the next target
		}
		else
		{
			newPathTimer = newPathTime;
			startNode = currentlyOn;
			target = playerPos.currentlyOn;
			//nodeTarget = startNode;
			path = generatePath(startNode, target);
			nodeTarget = path.Dequeue();
		}
    }
	
	// Code logic from https://www.peachpit.com/articles/article.aspx?p=101142
	private Queue<Transform> generatePath(Transform sta, Transform tar)
	{
		Queue<Transform> toVisit = new Queue<Transform>();
		List<Transform> visited = new List<Transform>();
		
		if(sta.Equals(tar))
		{
			return buildPath(sta);
		}
		else
		{
			Node staNode = sta.gameObject.GetComponent<NodeMonobehaviour>().node;
			staNode.bfsParent = null;
			visited.Add(sta);
			for(int i = 0;i<staNode.neighbours.Count;i++)
			{
				staNode.neighbours[i].gameObject.GetComponent<NodeMonobehaviour>().node.bfsParent = sta;
				toVisit.Enqueue(staNode.neighbours[i]);
			}
		}
		
		while(toVisit.Count > 0) // I should be using .Any here, but it doesn't work for some reason
		{
			Transform currentNode = toVisit.Dequeue();
			Node currNode = currentNode.gameObject.GetComponent<NodeMonobehaviour>().node;
			
			if(currentNode.Equals(tar))
			{
				return buildPath(currentNode);
			}
			else
			{
				visited.Add(currentNode);
				
				for(int i = 0;i<currNode.neighbours.Count;i++)
				{
					if(!visited.Contains(currNode.neighbours[i]) && !toVisit.Contains(currNode.neighbours[i]))
					{
						currNode.neighbours[i].gameObject.GetComponent<NodeMonobehaviour>().node.bfsParent = currentNode;
						toVisit.Enqueue(currNode.neighbours[i]);
					}
				}
			}
		}
		
		return null;
	}
	
	private Queue<Transform> buildPath(Transform n)
	{
		Queue<Transform> path = new Queue<Transform>();
		//Node n = node.gameObject.GetComponent<NodeMonobehaviour>().node;
		/*for(int i = 0;i<grid.Count;i++)
		{
			for(int j = 0;j<grid[i].Count;j++)
			{
				Debug.Log(grid[i][j].name + ", " + grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.bfsParent.name);
			}
		}*/
		while(n.gameObject.GetComponent<NodeMonobehaviour>().node.bfsParent != null)
		{
			path.Enqueue(n);
			n = n.gameObject.GetComponent<NodeMonobehaviour>().node.bfsParent;
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
}
