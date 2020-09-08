using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace node
{
	public class Node
	{
		public Transform bfsParent; // for searching
		public Vector2Int name;
		public Transform position;
		public List<Transform> neighbours;
	}
}