using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using node;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyPathfinding : MonoBehaviour
{
	// basically, the only thing that changes between its subclasses is the logic of acquiring targets
	// Pathfinding needs a rigidbody and a dynamic collider to function.
	
	public GenerateNav nav;
	protected Transform target;
	protected List<List<Transform>> grid;
	public Transform startNode;
	protected Transform nodeTarget; //used in pathing 
	protected Transform currentlyOn;
	public float newPathTime; // Time between path updates
	protected float newPathTimer;
	protected Queue<Transform> path = new Queue<Transform>();
	
	private Vector2 movementVector;
	private Rigidbody2D rb2d;
	public float movementSpeed;
	
    // Start is called before the first frame update
    public virtual void Start()
    {
		currentlyOn = transform;
		nodeTarget = transform;
		target = startNode;
		
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		
		path = generatePath(startNode, target);
		nodeTarget = startNode;
		
		newPathTimer = newPathTime;
		
		grid = nav.getGrid();
    }
	
	public virtual void Update()
	{
		if(newPathTimer > 0)
		{
			newPathTimer -= Time.deltaTime;
			movement(true);
		}
		else
		{
			newPathTimer = newPathTime;
			recalculatePath();
		}
	}
	
	// Code logic from https://www.peachpit.com/articles/article.aspx?p=101142
	protected Queue<Transform> generatePath(Transform sta, Transform tar)
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
	
	protected void movement(bool rigidbodyMove)
	{
		// these are necessary because these distance things take the z coordinate into account and there are some
		// weird discrepencies there
		Vector2 transform2d = transform.position;
		Vector2 nodeTarget2d = nodeTarget.position;
		
		if(Vector3.Distance(transform2d, nodeTarget2d) > 0.1f)
		{
			movementVector = nodeTarget2d - transform2d;
			movementVector = movementVector.normalized;
			
			if(rigidbodyMove)
			{
				rb2d.AddForce(movementVector * Time.deltaTime * movementSpeed);
			}
			else
			{
				transform.Translate(movementVector * Time.deltaTime * movementSpeed);
			}
		}
		else
		{
			if(path == null)
			{
				
			}
			else if(path.Count != 0)
			{
				nodeTarget = path.Dequeue();
			}
		}
	}
	
	abstract public void recalculatePath();
}
