using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom1SetPlayer : MonoBehaviour
{
	public float setPlayerTime;
	private float setPlayerTimer;
	
	public HoldPlayer holdPlayer;
	
	public EnemyAI_MoveTowards enemyAI;
	public EnemyAI_TargetPlayer targetPlayer;
	
    // Start is called before the first frame update
    void Start()
    {
        setPlayerTimer = setPlayerTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(setPlayerTimer > 0)
		{
			setPlayerTimer -= Time.deltaTime;
		}
		else
		{
			enemyAI.playerPos = holdPlayer.player.gameObject.GetComponent<PlayerGridPos>();
			targetPlayer.target = holdPlayer.player.transform;
			this.enabled = false;
		}
    }
}
