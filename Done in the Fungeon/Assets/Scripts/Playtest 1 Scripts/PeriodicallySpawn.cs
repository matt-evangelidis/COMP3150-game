using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicallySpawn : MonoBehaviour
{
	public float spawnPeriod;
	private float spawnTimer;
	
	public EnemySpawner spawnScript;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer < 0)
		{
			spawnScript.CreateEnemy();
			spawnTimer = spawnPeriod;
		}
		else
		{
			spawnTimer -= Time.deltaTime;
		}
    }
}
