using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMapNoMovement : MonoBehaviour
{
    public string to;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		SceneManager.LoadScene(to);
	}
}
