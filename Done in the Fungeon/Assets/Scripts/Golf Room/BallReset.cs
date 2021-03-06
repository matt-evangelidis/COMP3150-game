﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallReset : MonoBehaviour
{
	public Rigidbody2D rb2d;
	public Transform startPosition;
	private float knockback;
	public KnockableEntity damageable;
	public int hitsRemaining;
	private int currentHitsRemaining;
	public bool infiniteHits;
	
	private float resetTimer;
	private float maxTime = 10.0f;
	
	public Text number;

	void Start()
	{
		knockback = damageable.knockbackResistance;
		currentHitsRemaining = hitsRemaining;
	}
	
    // Update is called once per frame
    void Update()
    {
		if(infiniteHits)
		{
			number.text = "∞";
		}
		else
		{
			number.text = currentHitsRemaining.ToString();
		}
		
        if(rb2d.velocity == Vector2.zero && currentHitsRemaining <= 0)
		{
			Reset();
		}
		else
		{
			if(currentHitsRemaining > 0)
			{
				damageable.knockbackResistance = knockback;
			}
			else
			{
				damageable.knockbackResistance = 1;
			}
		}
		
		// Reset the ball if it gets stuck for more than 10 seconds.
		if(currentHitsRemaining <= 0)
		{
			resetTimer += Time.deltaTime;
			if(resetTimer > maxTime)
			{
				Reset();
				resetTimer = 0;
			}
		}
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "PlayerAttack" && currentHitsRemaining > 0 && !infiniteHits)
		{
			currentHitsRemaining--;
		}
	}
	
	public void Reset()
	{
		currentHitsRemaining = hitsRemaining;
		transform.position = startPosition.position;
		rb2d.velocity = Vector3.zero;
		damageable.knockbackResistance = knockback;
	}
}
