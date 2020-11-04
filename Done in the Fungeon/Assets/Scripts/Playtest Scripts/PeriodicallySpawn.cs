using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicallySpawn : MonoBehaviour
{
	public float spawnPeriod;
	private float spawnTimer = 0.0f;
	
	public ExplodingEnemySpawner spawnScript;
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer > 0)
		{
			spawnTimer -= Time.deltaTime;
		}
		else
		{
			spawnScript.CreateEnemy();
			spawnTimer = spawnPeriod;
		}
    }
}
