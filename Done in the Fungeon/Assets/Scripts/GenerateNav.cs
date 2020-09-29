using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using node;

public class GenerateNav : MonoBehaviour
{
	private List<List<Transform>> grid;
	private const int gridSize = 13;
	
	public bool enableDiagonal;
	
	void Awake()
	{
		generate();
	}
	
	public void generate()
	{
		if(enableDiagonal)
		{
			generateGridWithDiagonals();
		}
		else
		{
			generateGrid();
		}
	}
	
	public List<List<Transform>> getGrid()
	{
		generate();
		return grid;
	}
	
	public void generateGrid()
	{
		// basically get all of the children into a list of list of structs
		grid = new List<List<Transform>>();
		for(int i = 0;i<transform.childCount;i++)
		{
			List<Transform> tempList = new List<Transform>();
			for(int j = 0;j<transform.GetChild(i).transform.childCount;j++)
			{
				Transform currentNode = transform.GetChild(i).GetChild(j);
				Node n = new Node();
				
				n.name = new Vector2Int(i, j);
				n.neighbours = new List<Transform>();
				currentNode.gameObject.GetComponent<NodeMonobehaviour>().node = n;
				
				tempList.Add(currentNode);
				
				/*Node n = new Node();
				n.name = new Vector2(i, j);
				n.position = transform.GetChild(i).GetChild(j);
				n.neighbours = new List<Node>();
				tempList.Add(n);*/
			}
			grid.Add(tempList);
		}
		
		// generate neighbour lists
		for(int i = 0;i<grid.Count;i++)
		{
			for(int j = 0;j<grid[i].Count;j++)
			{	
				// neighbours added in the order: above, right, below, left
				//if(i-1 >= 0 && grid[i-1][j].position.gameObject.tag == "nav_enable")
				if(i-1 >= 0 && grid[i-1][j].gameObject.tag == "nav_enable")
				{
					//grid[i][j].neighbours.Add(grid[i-1][j]);
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i-1][j]);
				}
				
				//if(j-1 >= 0 && grid[i][j-1].position.gameObject.tag == "nav_enable")
				if(j-1 >= 0 && grid[i][j-1].gameObject.tag == "nav_enable")
				{
					//grid[i][j].neighbours.Add(grid[i][j-1]);
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i][j-1]);
				}
				
				//if(i+1 <= gridSize && grid[i+1][j].position.gameObject.tag == "nav_enable")
				if(i+1 <= gridSize && grid[i+1][j].gameObject.tag == "nav_enable")
				{
					//grid[i][j].neighbours.Add(grid[i+1][j]);
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i+1][j]);
				}
				
				//if(j+1 <= gridSize && grid[i][j+1].position.gameObject.tag == "nav_enable")
				if(j+1 <= gridSize && grid[i][j+1].gameObject.tag == "nav_enable")
				{
					//grid[i][j].neighbours.Add(grid[i][j+1]);
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i][j+1]);
				}
				
				//Debug.Log("(" + i + ", " + j + ") " + grid[i][j].position);
			}
		}
		
		
		
		
		// this prints the node of every neighbour
		/*for(int i = 0;i<grid.Count;i++)
		{
			for(int j = 0;j<grid[i].Count;j++)
			{
				for(int k = 0;k<grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Count;k++)
				{
				Debug.Log("(" + i + ", " + j + ") " + grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours[k]);
				}
			}
		}*/
	}
	
	public void generateGridWithDiagonals()
	{
		// basically get all of the children into a list of list of structs
		grid = new List<List<Transform>>();
		for(int i = 0;i<transform.childCount;i++)
		{
			List<Transform> tempList = new List<Transform>();
			for(int j = 0;j<transform.GetChild(i).transform.childCount;j++)
			{
				Transform currentNode = transform.GetChild(i).GetChild(j);
				Node n = new Node();
				
				n.name = new Vector2Int(i, j);
				n.neighbours = new List<Transform>();
				currentNode.gameObject.GetComponent<NodeMonobehaviour>().node = n;
				
				tempList.Add(currentNode);
			}
			grid.Add(tempList);
		}
		
		// generate neighbour lists
		for(int i = 0;i<grid.Count;i++)
		{
			for(int j = 0;j<grid[i].Count;j++)
			{	
				// neighbours added in the order: above, right, below, left
				if(i-1 >= 0 && grid[i-1][j].gameObject.tag == "nav_enable")
				{
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i-1][j]);
				}
				
				if(j-1 >= 0 && grid[i][j-1].gameObject.tag == "nav_enable")
				{
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i][j-1]);
				}
				
				if(i+1 <= gridSize && grid[i+1][j].gameObject.tag == "nav_enable")
				{
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i+1][j]);
				}
				
				if(j+1 <= gridSize && grid[i][j+1].gameObject.tag == "nav_enable")
				{
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i][j+1]);
				}
				
				//diagonals
				if(i-1 >= 0 && j+1 <= gridSize && grid[i-1][j+1].gameObject.tag == "nav_enable")
				{
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i-1][j+1]);
				}
				
				if(i-1 >= 0 && j-1 >= 0 && grid[i-1][j-1].gameObject.tag == "nav_enable")
				{
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i-1][j-1]);
				}
				
				if(i+1 <= gridSize && j-1 >= 0 && grid[i+1][j-1].gameObject.tag == "nav_enable")
				{
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i+1][j-1]);
				}
				
				if(i+1 <= gridSize && j+1 <= gridSize && grid[i+1][j+1].gameObject.tag == "nav_enable")
				{
					grid[i][j].gameObject.GetComponent<NodeMonobehaviour>().node.neighbours.Add(grid[i+1][j+1]);
				}
			}
		}
	}
}
