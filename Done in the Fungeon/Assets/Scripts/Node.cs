using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace node
{
	public class Node
	{
		public Node parent;
		public Vector2 name;
		public Transform position;
		public List<Node> neighbours;
	}
}