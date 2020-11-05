using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsComplete : MonoBehaviour
{
	// This class keeps track of the levels visited by the player. The method is to be called when a challenge room is complete, which varies based on the room.
	
	public List<string> levelsCompleted = new List<string>();
	
	public void RoomComplete()
	{
		if(!levelsCompleted.Contains(SceneManager.GetActiveScene().name))
		{
			levelsCompleted.Add(SceneManager.GetActiveScene().name);
		}
	}
	
	public bool IsComplete(string name)
	{
		return levelsCompleted.Contains(name);
	}
}
