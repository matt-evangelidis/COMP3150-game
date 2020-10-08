using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour
{
	public string to;
	public enum Side
	{
		Top,
		Bottom,
		Left,
		Right
	};
	public Side side;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			switch(side)
			{
				case Side.Top:
					c.gameObject.transform.position = new Vector3(c.transform.position.x, -c.transform.position.y, c.transform.position.z);
					break;
				case Side.Bottom:
					c.gameObject.transform.position = new Vector3(c.transform.position.x, -c.transform.position.y, c.transform.position.z);
					break;
				case Side.Left:
					c.gameObject.transform.position = new Vector3(-c.transform.position.x, c.transform.position.y, c.transform.position.z);
					break;
				case Side.Right:
					c.gameObject.transform.position = new Vector3(-c.transform.position.x, c.transform.position.y, c.transform.position.z);
					break;
			}
			SceneManager.LoadScene(to);
		}
	}
}
