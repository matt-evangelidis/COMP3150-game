using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyPlayer : MonoBehaviour
{
	public GameObject player;
	
	void Awake()
	{
		DontDestroyOnLoad(player);
	}
}
