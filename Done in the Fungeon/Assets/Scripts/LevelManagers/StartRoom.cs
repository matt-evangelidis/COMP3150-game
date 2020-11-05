using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartRoom : MonoBehaviour
{
	private GameObject player;
	public GameObject[] objects;
	
	void Awake()
	{
		player = GameObject.Find("/Player");
		if(player.GetComponent<LevelsComplete>().IsComplete(SceneManager.GetActiveScene().name))
		{
			foreach(GameObject i in objects)
			{
				i.SetActive(false);
			}
		}
	}
}
