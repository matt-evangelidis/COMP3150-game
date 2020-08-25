using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
	public float attackDuration;
	public float comboTime;

	private float rotation;
	
	private float attackDurationTimer;
	private float comboTimer;

	private SpriteRenderer sprite;

	public string left = "a";
	public string right = "d";
	public string up = "w";
	public string down = "s";
	
	public GameObject damageZone1;
	public GameObject damageZone2;
	public GameObject damageZone3;
	public GameObject damageZone4;
	public GameObject damageZone5;
	
	//States
	enum State
	{
		Default,
		Combo1,
		Combo2,
		Combo3,
		Combo4,
		Combo5,
		Dash,
		ChargedAttack
	};
	private State state;
	
    // Start is called before the first frame update
    void Start()
    {
		sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		switch(state) {
			case State.Default:
				if(Input.GetButtonDown("Attack"))
				{
					comboTimer = comboTime;
					attackDurationTimer = attackDuration;
					state = State.Combo1;
				}
				break;
			case State.Combo1:
				comboTimer -= Time.deltaTime;
				
				// Attack Duration
				if(attackDurationTimer >= 0)
				{
					attackDurationTimer -= Time.deltaTime;
					damageZone1.gameObject.SetActive(true);
				}
				else
				{
					damageZone1.gameObject.SetActive(false);
				}
				
				if(comboTimer < 0)
				{
					state = State.Default;
				}
				
				if(Input.GetButtonDown("Attack"))
				{
					comboTimer += comboTime;
					attackDurationTimer = attackDuration;
					state = State.Combo2;
				}
				break;
			case State.Combo2:
				comboTimer -= Time.deltaTime;
				
				// Attack Duration
				if(attackDurationTimer >= 0)
				{
					attackDurationTimer -= Time.deltaTime;
					damageZone2.gameObject.SetActive(true);
				}
				else
				{
					damageZone2.gameObject.SetActive(false);
				}
				
				if(comboTimer < 0)
				{
					state = State.Default;
				}
				
				if(Input.GetButtonDown("Attack"))
				{
					comboTimer += comboTime;
					attackDurationTimer = attackDuration;
					state = State.Combo3;
				}
				break;
			case State.Combo3:
				comboTimer -= Time.deltaTime;
				
				// Attack Duration
				if(attackDurationTimer >= 0)
				{
					attackDurationTimer -= Time.deltaTime;
					damageZone3.gameObject.SetActive(true);
				}
				else
				{
					damageZone3.gameObject.SetActive(false);
				}
				
				if(comboTimer < 0)
				{
					state = State.Default;
				}
				
				if(Input.GetButtonDown("Attack"))
				{
					comboTimer += comboTime;
					attackDurationTimer = attackDuration;
					state = State.Combo4;
				}
				break;
			case State.Combo4:
				comboTimer -= Time.deltaTime;
				
				// Attack Duration
				if(attackDurationTimer >= 0)
				{
					attackDurationTimer -= Time.deltaTime;
					damageZone4.gameObject.SetActive(true);
				}
				else
				{
					damageZone4.gameObject.SetActive(false);
				}
				
				if(comboTimer < 0)
				{
					state = State.Default;
				}
				
				if(Input.GetButtonDown("Attack"))
				{
					comboTimer += comboTime;
					attackDurationTimer = attackDuration;
					state = State.Combo5;
				}
				break;
			case State.Combo5:
				comboTimer -= Time.deltaTime;
				
				// Attack Duration
				if(attackDurationTimer >= 0)
				{
					attackDurationTimer -= Time.deltaTime;
					damageZone5.gameObject.SetActive(true);
				}
				else
				{
					damageZone5.gameObject.SetActive(false);
				}
				
				if(comboTimer < 0)
				{
					state = State.Default;
				}
				break;
			case State.Dash:
				break;
			case State.ChargedAttack:
				break;
		}
		
        float velocity = moveSpeed * Time.deltaTime;

        float verticalMove = Input.GetAxis("Vertical") * velocity;
        float horizontalMove = Input.GetAxis("Horizontal") * velocity;

		if (Input.GetKey(left))
		{
			rotation = 90f;
		}
		else if (Input.GetKey(right))
		{
			rotation = -90f;
		}
		else if (Input.GetKey(up))
		{
			rotation = 0f;
		}
		else if (Input.GetKey(down))
		{
			rotation = 180f;
		}

		transform.localRotation = Quaternion.Euler(0, 0, rotation);
        transform.Translate(horizontalMove, verticalMove, 0, Space.World);
		

		/*
		if(Input.GetButtonDown("Attack"))
		{
			attackDurationTimer += attackDuration;
		}
		
		if(attackDurationTimer >= 0) {
			damageZone.gameObject.SetActive(true);
			attackDurationTimer -= Time.deltaTime;
		} else {
			damageZone.gameObject.SetActive(false);
		}
		*/

		/*        if (Input.GetButton("up"))
				{
					transform.Translate(Vector3.up * verticalMove);
				}

				else if (Input.GetButton("down"))
				{
					transform.Translate(Vector3.down * verticalMove);
				}

				if (Input.GetButton("left"))
				{
					transform.Translate(Vector3.left * horizontalMove);
				}

				else if (Input.GetButton("right"))
				{
					transform.Translate(Vector3.right * horizontalMove);
				}*/
	}
}
