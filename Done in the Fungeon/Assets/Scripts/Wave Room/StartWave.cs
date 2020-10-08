using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
	public Animator animator;
	
	void OnTriggerEnter2D(Collider2D c)
	{
		animator.SetTrigger("Start");
	}
}
