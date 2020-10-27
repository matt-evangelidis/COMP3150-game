using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom2SetPlayer : MonoBehaviour
{
	public float setPlayerTime;
	private float setPlayerTimer;
	
	public HoldPlayer holdPlayer;
	
	public EnemyAI_MoveTowards enemyAI0;
	public EnemyAI_MoveTowards enemyAI1;
	public EnemyAI_MoveTowards enemyAI2;
	public EnemyAI_MoveTowards enemyAI3;
	public EnemyAI_MoveTowards enemyAI4;
	public EnemyAI_MoveTowards enemyAI5;
	public EnemyAI_MoveTowards enemyAI6;
	public EnemyAI_TargetPlayer targetPlayer0;
	public EnemyAI_TargetPlayer targetPlayer1;
	public EnemyAI_TargetPlayer targetPlayer2;
	public EnemyAI_TargetPlayer targetPlayer3;
	public EnemyAI_TargetPlayer targetPlayer4;
	public EnemyAI_TargetPlayer targetPlayer5;
	public EnemyAI_TargetPlayer targetPlayer6;
	
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
			enemyAI0.playerPos = holdPlayer.player.gameObject.GetComponent<PlayerGridPos>();
			targetPlayer0.target = holdPlayer.player.transform;
			enemyAI1.playerPos = holdPlayer.player.gameObject.GetComponent<PlayerGridPos>();
			targetPlayer1.target = holdPlayer.player.transform;
			enemyAI2.playerPos = holdPlayer.player.gameObject.GetComponent<PlayerGridPos>();
			targetPlayer2.target = holdPlayer.player.transform;
			enemyAI3.playerPos = holdPlayer.player.gameObject.GetComponent<PlayerGridPos>();
			targetPlayer3.target = holdPlayer.player.transform;
			enemyAI4.playerPos = holdPlayer.player.gameObject.GetComponent<PlayerGridPos>();
			targetPlayer4.target = holdPlayer.player.transform;
			enemyAI5.playerPos = holdPlayer.player.gameObject.GetComponent<PlayerGridPos>();
			targetPlayer5.target = holdPlayer.player.transform;
			enemyAI6.playerPos = holdPlayer.player.gameObject.GetComponent<PlayerGridPos>();
			targetPlayer6.target = holdPlayer.player.transform;
			this.enabled = false;
		}
    }
}
