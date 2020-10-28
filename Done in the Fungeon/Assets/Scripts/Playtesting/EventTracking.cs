using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventTracking : MonoBehaviour
{
	public Text playtestInfo;
	public GameObject playtestPanel;
	private string displayString;
	public bool end = false;
	
	public struct DamageEvent
	{
		public DamageEvent(string s, string d, float t)
		{
			stage = s;
			damagerName = d;
			time = Math.Round(t,2);
		}
		
		public string stage;
		public string damagerName;
		public double time;
		
		public override string ToString()
		{
			string temp = "";
			temp += "|" + stage + "," + damagerName + "," + time + "|";
			return temp;
		}
	}
	
	public struct LevelCompletionEvent
	{
		public LevelCompletionEvent(string s, float t, string e)
		{
			stage = s;
			time = Math.Round(t,2);
			extra = e;
		}
		
		public string stage;
		public double time;
		public string extra;
		
		public override string ToString()
		{
			string temp = "";
			temp += "| Level Complete: " + stage + "," + time + "," + extra + "|";
			return temp;
		}
	}
	
	List<DamageEvent> damageEvents = new List<DamageEvent>();
	List<LevelCompletionEvent> levelCompletionEvents = new List<LevelCompletionEvent>();
	
	public static float time;
	
	public void addDamageEvent(string d)
	{
		DamageEvent temp = new DamageEvent(levelAliases(), damagerAliases(d), time);
		damageEvents.Add(temp);
	}
	
	public void addLevelCompletionEvent(string e)
	{
		LevelCompletionEvent temp = new LevelCompletionEvent(levelAliases(), time, e);
		levelCompletionEvents.Add(temp);
	}
	
    // Update is called once per frame
    void Update()
    {
		if(!end)
		{
			time += Time.deltaTime;
		}
		
		string de = "";
		foreach(DamageEvent x in damageEvents)
		{
			de += x;
		}
		
		foreach(LevelCompletionEvent x in levelCompletionEvents)
		{
			de += x;
		}
		
		displayString = "time: " + Math.Round(time,2) + ", \n" + de;
		
		playtestInfo.text = displayString;
		
		if(Input.GetKeyDown("g"))
		{
			if(playtestPanel.activeInHierarchy)
			{
				playtestPanel.SetActive(false);
			}
			else
			{
				playtestPanel.SetActive(true);
			}
		}
    }
	
	string levelAliases()
	{
		if(SceneManager.GetActiveScene().name == "Combat Room 1")
		{
			return "CR1";
		}
		else if(SceneManager.GetActiveScene().name == "Combat Room 2")
		{
			return "CR2";
		}
		else if(SceneManager.GetActiveScene().name == "Double Room 1")
		{
			return "DR1";
		}
		else if(SceneManager.GetActiveScene().name == "Double Room 2")
		{
			return "DR2";
		}
		else if(SceneManager.GetActiveScene().name == "Double Room 3")
		{
			return "DR3";
		}
		else if(SceneManager.GetActiveScene().name == "Golf Room 1")
		{
			return "GR1";
		}
		else if(SceneManager.GetActiveScene().name == "Golf Room 2")
		{
			return "GR2";
		}
		else if(SceneManager.GetActiveScene().name == "Golf Room 3")
		{
			return "GR3";
		}
		else if(SceneManager.GetActiveScene().name == "Golf Room 4")
		{
			return "GR4";
		}
		else if(SceneManager.GetActiveScene().name == "Golf Room 5")
		{
			return "GR5";
		}
		else if(SceneManager.GetActiveScene().name == "Lava Maze 1")
		{
			return "LM1";
		}
		else if(SceneManager.GetActiveScene().name == "Lava Maze 2")
		{
			return "LM2";
		}
		else if(SceneManager.GetActiveScene().name == "Lava Maze 3")
		{
			return "LM3";
		}
		else if(SceneManager.GetActiveScene().name == "Maze Room 1")
		{
			return "MR1";
		}
		else if(SceneManager.GetActiveScene().name == "Maze Room 2")
		{
			return "MR2";
		}
		else if(SceneManager.GetActiveScene().name == "Maze Room 3")
		{
			return "MR3";
		}
		else if(SceneManager.GetActiveScene().name == "Maze Room 4")
		{
			return "MR4";
		}
		else
		{
			return "";
		}
	}
	
	string damagerAliases(string n)
	{
		if(n == "Sumo Enemy")
		{
			return "SUMO";
		}
		else if(n == "Combat Room 1 Lava")
		{
			return "CR1L";
		}
		else if(n == "Bomb Enemy")
		{
			return "BOMB";
		}
		else if(n == "Lava Room 1")
		{
			return "LR1";
		}
		else if(n == "Lava Room 2")
		{
			return "LR2";
		}
		else if(n == "Lava Room 3")
		{
			return "LR3";
		}
		else if(n == "Lava Room 2 Spinning Hazard 0")
		{
			return "LR2E0";
		}
		else if(n == "Lava Room 2 Spinning Hazard 1")
		{
			return "LR2E1";
		}
		else if(n == "Lava Room 2 Spinning Hazard 2")
		{
			return "LR2E2";
		}
		else if(n == "Lava Room 2 Spinning Hazard 3")
		{
			return "LR2E3";
		}
		else if(n == "Lava Room 2 Spinning Hazard 4")
		{
			return "LR2E4";
		}
		else if(n == "Lava Room 3 Spinning Hazard 0")
		{
			return "LR3E0";
		}
		else if(n == "Lava Room 3 Spinning Hazard 1")
		{
			return "LR3E1";
		}
		else if(n == "Lava Room 3 Spinning Hazard 2")
		{
			return "LR3E2";
		}
		else if(n == "Lava Room 3 Spinning Hazard 3")
		{
			return "LR3E3";
		}
		else if(n == "Lava Room 3 Spinning Hazard 4")
		{
			return "LR3E4";
		}
		else if(n == "Lava Room 3 Spinning Hazard 5")
		{
			return "LR3E5";
		}
		else if(n == "Maze Room 1 Spinning Hazard 0")
		{
			return "MR1E0";
		}
		else if(n == "Maze Room 1 Spinning Hazard 1")
		{
			return "MR1E1";
		}
		else if(n == "Maze Room 1 Big Spinning Hazard 0")
		{
			return "MR1E2";
		}
		else if(n == "Maze Room 1 Big Spinning Hazard 1")
		{
			return "MR1E3";
		}
		else if(n == "Maze Room 2 Spinning Hazard 0")
		{
			return "MR2E0";
		}
		else if(n == "Maze Room 2 Spinning Hazard 1")
		{
			return "MR2E1";
		}
		else if(n == "Maze Room 4 Spinning Hazard")
		{
			return "MR4E0";
		}
		else
		{
			return "";
		}
	}
}
