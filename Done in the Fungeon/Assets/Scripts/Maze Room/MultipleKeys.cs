using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleKeys : MonoBehaviour
{
	public Key[] keys;
	public GameObject[] exitDoors;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		int keyCount = 0;
        foreach(Key i in keys)
		{
			if(i.Collected == true)
			{
				keyCount++;
			}
		}
		
		if(keyCount == keys.Length)
		{
			foreach(GameObject i in exitDoors)
			{
				i.SetActive(false);
			}
			this.enabled = false;
		}
    }
}
