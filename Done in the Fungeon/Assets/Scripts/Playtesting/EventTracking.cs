using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
			//temp += "stage: " + stage;
			//temp += ", source: " + damagerName;
			//temp += ", time: " + time;
			return temp;
		}
	}
	
	List<DamageEvent> damageEvents = new List<DamageEvent>();
	
	public static int enemiesSpawned;
	public static float time;
	
	public void addDamageEvent(string s, string d)
	{
		DamageEvent temp = new DamageEvent(s, d, time);
		damageEvents.Add(temp);
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
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
		
		displayString = "e: " + enemiesSpawned + ", t: " + Math.Round(time,2) + ", \n" + de;
		
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
}
