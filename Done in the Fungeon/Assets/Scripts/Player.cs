using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
	public float attackDuration;
	public float attack5Duration;
	public float comboTime;
	public float comboEndLag;
	public float attackEndLag;
	public float dashTime;
	public float dashSpeed;
	public float dashCooldown;
	public float chargeTime;
	public float chargedDashTime;
	public float chargedAttackTime;
	
	public Animator animator;
	private int comboCount;

	private float rotation;
	
	private bool attackPressed;
	
	private float attackDurationTimer;
	private float attack5DurationTimer;
	private float comboTimer;
	private float attackEndLagTimer;
	private float comboEndLagTimer;
	private float dashTimer;
	private float dashCooldownTimer;
	private float chargeTimer;
	private float chargedDashTimer;
	private float chargedAttackTimer;

	private SpriteRenderer sprite;
	
	public GameObject damageZone1;
	public GameObject damageZone2;
	public GameObject damageZone3;
	public GameObject damageZone4;
	public GameObject damageZone5;
	public GameObject chargedAttackDamageZone;
	public GameObject chargedDashDamageZone;
	
	public Color chargingColour;
	public Color chargedColour;
	public Color defaultColour;
	
	/*
	Notes:
	- The charge time should be approximately as long as a combo
	*/
	
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
		ChargedDash,
		ChargedAttack
	};
	private State state;
	
	// Direction
	enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}
	private Direction direction;
	
    // Start is called before the first frame update
    void Start()
    {
		sprite = gameObject.GetComponent<SpriteRenderer>();
		chargeTimer = chargeTime;
		comboCount = 0;
		attackPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
		switch(state) {
			case State.Default:
				Move();
				Turning();
				Charge();
				
				if(Input.GetButtonDown("Attack"))
				{
					comboTimer = comboTime;
					attackDurationTimer = attackDuration;
					attackEndLagTimer = attackEndLag;
					state = State.Combo1;
					comboCount = 1;
					animator.SetInteger("Attack", comboCount);
				}
				
				if(Input.GetButtonDown("Dash") && dashCooldownTimer < 0)
				{
					dashTimer = dashTime;
					state = State.Dash;
				}
				
				break;
				
			case State.Combo1:
				Attack(damageZone1);
				Charge();
				
				if(attackEndLagTimer <= 0 && attackPressed)
				{
					attackPressed = false;
					comboTimer = comboTime;
					attackDurationTimer = attackDuration;
					attackEndLagTimer = attackEndLag;
					state = State.Combo2;
					comboCount = 2;
					animator.SetInteger("Attack", comboCount);
				}
				
				break;
				
			case State.Combo2:
				Attack(damageZone2);
				Charge();
				
				if(attackEndLagTimer <= 0 && attackPressed)
				{
					attackPressed = false;
					comboTimer = comboTime;
					attackDurationTimer = attackDuration;
					attackEndLagTimer = attackEndLag;
					state = State.Combo3;
					comboCount = 3;
					animator.SetInteger("Attack", comboCount);
				}
				break;
				
			case State.Combo3:
				Attack(damageZone3);
				Charge();
				
				if(attackEndLagTimer <= 0 && attackPressed)
				{
					attackPressed = false;
					comboTimer = comboTime;
					attackDurationTimer = attackDuration;
					attackEndLagTimer = attackEndLag;
					state = State.Combo4;
					comboCount = 4;
					animator.SetInteger("Attack", comboCount);
				}
				
				break;
				
			case State.Combo4:
				Attack(damageZone4);
				Charge();
				
				if(attackEndLagTimer <= 0 && attackPressed)
				{
					attackPressed = false;
					comboEndLagTimer = comboEndLag; // NOTE: This line is different. End lag needs to differ from combo time.
					// because the combo window is too long to work as end lag.
					attackDurationTimer = attackDuration;
					attackEndLagTimer = attackEndLag;
					state = State.Combo5;
					comboCount = 5;
					animator.SetInteger("Attack", comboCount);
				}
				
				break;
				
			case State.Combo5:
				// Cannot use the same code as the previous ones because this one uses end lag instead of combo time
				comboEndLagTimer -= Time.deltaTime;
				
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
				
				if(comboEndLagTimer < 0)
				{
					state = State.Default;
					comboCount = 0;
					animator.SetInteger("Attack", comboCount);
				}
				
				Charge();
				
				break;
				
			case State.Dash:
				dashTimer -= Time.deltaTime;
				
				Charge();
				
				if(direction == Direction.Up)
				{
					transform.Translate(0, dashSpeed * Time.deltaTime, 0, Space.World);
				}
				else if(direction == Direction.Down)
				{
					transform.Translate(0, -dashSpeed * Time.deltaTime, 0, Space.World);
				}
				else if(direction == Direction.Left)
				{
					transform.Translate(-dashSpeed * Time.deltaTime, 0, 0, Space.World);
				}
				else if(direction == Direction.Right)
				{
					transform.Translate(dashSpeed * Time.deltaTime, 0, 0, Space.World);
				}
				
				if(dashTimer < 0) {
					state = State.Default;
					dashCooldownTimer = dashCooldown;
				}
				
				break;
				
			case State.ChargedDash:
				chargedDashTimer -= Time.deltaTime;
				
				chargedDashDamageZone.gameObject.SetActive(true);
				
				if(direction == Direction.Up)
				{
					transform.Translate(0, dashSpeed * Time.deltaTime, 0, Space.World);
				}
				else if(direction == Direction.Down)
				{
					transform.Translate(0, -dashSpeed * Time.deltaTime, 0, Space.World);
				}
				else if(direction == Direction.Left)
				{
					transform.Translate(-dashSpeed * Time.deltaTime, 0, 0, Space.World);
				}
				else if(direction == Direction.Right)
				{
					transform.Translate(dashSpeed * Time.deltaTime, 0, 0, Space.World);
				}
				
				if(chargedDashTimer < 0)
				{
					chargedDashDamageZone.gameObject.SetActive(false);
					state = State.ChargedAttack;
					chargedAttackTimer = chargedAttackTime;
				}
				
				break;
				
			case State.ChargedAttack:
				chargedAttackTimer -= Time.deltaTime;
				
				chargedAttackDamageZone.gameObject.SetActive(true);
				
				if(chargedAttackTimer < 0)
				{
					chargedAttackDamageZone.gameObject.SetActive(false);
					state = State.Default;
				}
				
				break;
		}
		
		Aiming();
		
		// Global dash cooldown
		if(dashCooldownTimer >= 0)
		{
			dashCooldownTimer -= Time.deltaTime;
		}
	}
	
	// Add this to any state where you can move
	void Move() {
		float velocity = moveSpeed * Time.deltaTime;
		float verticalMove = Input.GetAxis("Vertical") * velocity;
		float horizontalMove = Input.GetAxis("Horizontal") * velocity;
		transform.Translate(horizontalMove, verticalMove, 0, Space.World);
	}
	
	// Add this to any state where you can turn
	void Turning() {
		if (direction == Direction.Left)
		{
			rotation = 90f;
		}
		else if (direction == Direction.Right)
		{
			rotation = -90f;
		}
		else if (direction == Direction.Up)
		{
			rotation = 0f;
		}
		else if (direction == Direction.Down)
		{
			rotation = 180f;
		}
		
		transform.localRotation = Quaternion.Euler(0, 0, rotation);
	}
	
	// You can always aim in different directions, even if the character doesn't actually turn
	void Aiming() {
		if (Input.GetButton("Left"))
		{
			direction = Direction.Left;
		}
		else if (Input.GetButton("Right"))
		{
			direction = Direction.Right;
		}
		else if (Input.GetButton("Up"))
		{
			direction = Direction.Up;
		}
		else if (Input.GetButton("Down"))
		{
			direction = Direction.Down;
		}
		
		transform.localRotation = Quaternion.Euler(0, 0, rotation);
	}
	
	void Attack(GameObject damageZone) {
		comboTimer -= Time.deltaTime;
		
		if(attackEndLagTimer > 0)
		{
			attackEndLagTimer -= Time.deltaTime;
		}
		
		if(Input.GetButtonDown("Attack"))
		{
			attackPressed = true;
		}
		
		// Attack Duration
		if(attackDurationTimer >= 0)
		{
			attackDurationTimer -= Time.deltaTime;
			damageZone.gameObject.SetActive(true);
		}
		else
		{
			damageZone.gameObject.SetActive(false);
		}
		
		if(comboTimer < 0)
		{
			state = State.Default;
			comboCount = 0;
			animator.SetInteger("Attack", comboCount);
			disableDamageZones();
		}
	}
	
	// Add this to any state that can be interrupted by a charged attack
	void Charge() {
		if(Input.GetButton("Charge") && chargeTimer > 0)
		{
			chargeTimer -= Time.deltaTime;
			sprite.color = chargingColour;
		}
		else if(Input.GetButton("Charge") && chargeTimer <= 0)
		{
			sprite.color = chargedColour;
		}
		
		if(Input.GetButtonUp("Charge") && chargeTimer > 0)
		{
			chargeTimer = chargeTime;
			sprite.color = defaultColour;
		}
		else if(Input.GetButtonUp("Charge") && chargeTimer <= 0)
		{
			Turning(); // Need to set the rotation angle whenever the dash goes off
			attackPressed = false; // So the input buffer doesn't carry into the next combo
			chargeTimer = chargeTime;
			chargedDashTimer = chargedDashTime;
			state = State.ChargedDash;
			sprite.color = defaultColour;
			comboCount = 0; // Reset the combo counter for the animator
			animator.SetInteger("Attack", comboCount);
			disableDamageZones();
		}
	}
	
	// This is to stop damage zones from getting stuck if you interrupt a basic attack
	void disableDamageZones() {
		damageZone1.gameObject.SetActive(false);
		damageZone2.gameObject.SetActive(false);
		damageZone3.gameObject.SetActive(false);
		damageZone4.gameObject.SetActive(false);
		damageZone5.gameObject.SetActive(false);
		chargedAttackDamageZone.gameObject.SetActive(false);
		chargedDashDamageZone.gameObject.SetActive(false);
	}
}
