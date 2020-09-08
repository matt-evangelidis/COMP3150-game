using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using node;

public class GenerateNav : MonoBehaviour
{
	private List<List<Node>> grid;
	private const int gridSize = 13;
	
	void Awake()
	{
		// basically get all of the children into a list of list of structs
		grid = new List<List<Node>>();
		for(int i = 0;i<transform.childCount;i++)
		{
			List<Node> tempList = new List<Node>();
			for(int j = 0;j<transform.GetChild(i).transform.childCount;j++)
			{
				Node n = new Node();
				n.name = new Vector2(i, j);
				n.position = transform.GetChild(i).GetChild(j);
				n.neighbours = new List<Node>();
				tempList.Add(n);
			}
			grid.Add(tempList);
		}
		
		// generate neighbour lists
		for(int i = 0;i<grid.Count;i++)
		{
			for(int j = 0;j<grid[i].Count;j++)
			{
				List<Node> neighbourList = new List<Node>();
				
				// neighbours added in the order: above, right, below, left
				if(i-1 >= 0 && grid[i-1][j].position.gameObject.tag == "nav_enable")
				{
					grid[i][j].neighbours.Add(grid[i-1][j]);
				}
				
				if(j-1 >= 0 && grid[i][j-1].position.gameObject.tag == "nav_enable")
				{
					grid[i][j].neighbours.Add(grid[i][j-1]);
				}
				
				if(i+1 <= gridSize && grid[i+1][j].position.gameObject.tag == "nav_enable")
				{
					grid[i][j].neighbours.Add(grid[i+1][j]);
				}
				
				if(j+1 <= gridSize && grid[i][j+1].position.gameObject.tag == "nav_enable")
				{
					grid[i][j].neighbours.Add(grid[i][j+1]);
				}
				
				//Debug.Log("(" + i + ", " + j + ") " + grid[i][j].position);
			}
		}
		
		/*for(int i = 0;i<grid.Count;i++)
		{
			for(int j = 0;j<grid[i].Count;j++)
			{
				for(int k = 0;k<grid[i][j].neighbours.Count;k++)
				{
					Debug.Log("(" + i + ", " + j + ") " + grid[i][j].neighbours[k].position);
				}
			}
		}*/
	}
	
	public List<List<Node>> getGrid()
	{
		return grid;
	}
}
