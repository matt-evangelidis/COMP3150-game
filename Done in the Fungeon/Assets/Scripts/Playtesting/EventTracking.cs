using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventTracking : MonoBehaviour
{
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
			temp += "stage: " + stage;
			temp += ", source: " + damagerName;
			temp += ", time: " + time;
			return temp;
		}
	}
	
	List<DamageEvent> damageEvents = new List<DamageEvent>();
	
	void addDamageEvent(string s, string d, float t)
	{
		DamageEvent temp = new DamageEvent(s, d, t);
		damageEvents.Add(temp);
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private float timer;
    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
		if(Input.GetKeyDown("h"))
		{
			addDamageEvent("stage1", "Patrolling Enemy", timer);
		}
		if(Input.GetKeyDown("j"))
		{
			foreach(DamageEvent d in damageEvents)
			{
				Debug.Log(d);
			}
		}
    }
}
