﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public int maxHP;
	[Tooltip("")]
    public float moveSpeed = 1f;
	[Tooltip("")]
    public float attackingMoveSpeed = 0.2f;
	[Tooltip("The time the damage zones of your first 4 basic attacks last.")]
	public float attackDuration;
	[Tooltip("The time the damage zone of attack 5 lasts.")]
	public float attack5Duration;
	[Tooltip("The time you need to idle before your combo resets.")]
	public float comboTime;
	[Tooltip("The time before you can move again after attack 5.")]
	public float comboEndLag;
	[Tooltip("The delay between attacks.")]
	public float attackEndLag;
	[Tooltip("The amount of time you dash for. You cannot move or during during this time.")]
	public float dashTime;
	[Tooltip("The speed you move during your dash.")]
	public float dashSpeed; // dash speed for translate movement was good at 50
	[Tooltip("The speed you move during your charged attack.")]
	public float chargedDashSpeed;
	[Tooltip("The time before you can next dash after a dash.")]
	public float dashCooldown;
	[Tooltip("The amount of time a charged attack takes to charge.")]
	public float chargeTime;
	[Tooltip("The amount of time the charged dash attack damage zone lasts for and how long the dash lasts.")]
	public float chargedDashTime;
	[Tooltip("The amount of time the damage zone of the dash attack lasts for.")]
	public float chargedAttackTime;
	public float hurtTime;
	[Tooltip("How long the player is invincible for after taking damage")]
	public float damageIFrames;
	[Tooltip("The amount the player will be slowed when in contact with slowers")]
	public float slowModifier;
	private float currentSlow = 1;
	
	public Animator animator; // for attacks
	private int comboCount;
	public Animator character;
	
	private int currentHP;

	private float rotation;
	
	private bool attackPressed;
	
	private bool immune;
	private bool invulnerable;
	private bool invincible;
	private bool hurt;
	private Vector3 knockbackVector;
	private float knockbackSpeed;
	
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
	private float hurtTimer;
	private float damageDelayTimer;
	private float damageStayTimer; // the time it takes before you can be damaged again if you stand in a damage zone
	private float hurtInvincibilityTimer;

	public SpriteRenderer sprite;
	private Rigidbody2D rb2d;
	private Vector2 position;
	
	public GameObject damageZone1;
	public GameObject damageZone2;
	public GameObject damageZone3;
	public GameObject damageZone4;
	public GameObject damageZone5;
	public GameObject chargedAttackDamageZone;
	public GameObject chargedDashDamageZone;
	public GameObject chargeHitBox;
	
	public Color chargingColour1;
	public Color chargingColour2;
	public Color chargedColour;
	public Color defaultColour;
	public Color invincibleColour;
	public Color damageColour;
	
	public CameraShake camShake;
	private SoundEffects sound;
	private bool soundPlayed;
	
	// temporarily here for playtesting
	//public EventTracking eventTracking;
	
	/*
	Notes:
	- The charge time should be approximately as long as a combo
	- There is less end lag on your attacks if you finish your combo
	- The player cannot turn while attacking because they can just spin
	- The bug below has been fixed. I just had to not let players aim during dashes because the direction of your dash is determined by your aiming direction, not the direction you are actually facing. It actually feels a bit better because you can continue attacking in the direction you dashed in.
		- There is a weird bug where you can change the direction of the movement of your charged attack without changing the direction of the charge itself. It's a bug, but I kind of like it because it gives you more options with your charged
	attack adds a little more depth to the mechanics.
	- The end lag after charged attacking felt a bit clunky, so I've allowed the player to interrupt it with basic attacks
	- I'm going to allow the player to strafe while holding the attack button. This allows for better control and was actually inspired by rune factory and other farming games.
	- The player can only ever take 1 damage at a time. Ease of programming and a design consideration.
	- With the way knockback works, you can't spam too quickly, so you have to time out your attacks so all of them land. You can spam, but you'll be dealing less damage.
	
	- Possible thing where you can reset your combo by turning. Adds a skill element where you need to time your charge cancels right.
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
		ComboTransition,
		Dash,
		ChargedDash,
		ChargedAttack,
		Hurt
	};
	private State state;
	
	// Direction
	enum Direction
	{
		Up,
		Down,
		Left,
		Right,
		UpLeft,
		UpRight,
		DownLeft,
		DownRight
	};
	private Direction direction;
	
    // Start is called before the first frame update
    void Start()
    {
		//maxHP = HealthIndicator.Instance.numOfHearts;
		currentHP = maxHP;
		//HealthIndicator.Instance.health = currentHP;
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		position = new Vector2(transform.position.x, transform.position.y);
		chargeTimer = chargeTime;
		comboCount = 0;
		attackPressed = false;
		damageDelayTimer = damageIFrames;
		sound = gameObject.GetComponent<SoundEffects>();
		
		invulnerable = false;
		immune = false;
    }

    // Update is called once per frame
    void Update()
    {
		switch(state) {
			case State.Default:
				Move(1);
				Aiming();
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
					attackAnimation(direction);
				}
				
				if(Input.GetButtonDown("Dash") && dashCooldownTimer < 0)
				{
					Dash();
				}
				
				if(hurt)
				{
					animator.SetBool("Hurt", true);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", false);
					currentHP -= 1;
						HealthIndicator.Instance.health -= 1;
					hurtTimer = hurtTime;
					state = State.Hurt;
					disableDamageZones();
					attackPressed = false;
				}
				
				break;
				
			case State.Combo1:
				Attack(damageZone1);
				Move(attackingMoveSpeed);
				Charge();
				Aiming();
				
				if(attackEndLagTimer <= 0 && attackPressed)
				{
					comboCount = 2;
					state = State.ComboTransition;
					soundPlayed = false;
				}
				
				if(Input.GetButtonDown("Dash") && dashCooldownTimer < 0)
				{
					Dash();
					soundPlayed = false;
				}
				
				if(hurt)
				{
					animator.SetBool("Hurt", true);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", false);
					currentHP -= 1;
						HealthIndicator.Instance.health -= 1;
					hurtTimer = hurtTime;
                    state = State.Hurt;
					disableDamageZones();
					attackPressed = false;
					soundPlayed = false;
				}
				
				break;
				
			case State.Combo2:
				Attack(damageZone2);
				Move(attackingMoveSpeed);
				Charge();
				Aiming();
				
				if(attackEndLagTimer <= 0 && attackPressed)
				{
					comboCount = 3;
					state = State.ComboTransition;
					soundPlayed = false;
				}
				
				if(Input.GetButtonDown("Dash") && dashCooldownTimer < 0)
				{
					Dash();
					soundPlayed = false;
				}
				
				if(hurt)
				{
					animator.SetBool("Hurt", true);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", false);
					currentHP -= 1;
						HealthIndicator.Instance.health -= 1;
					hurtTimer = hurtTime;
                    state = State.Hurt;
					disableDamageZones();
					attackPressed = false;
					soundPlayed = false;
				}
				
				break;
				
			case State.Combo3:
				Attack(damageZone3);
				Move(attackingMoveSpeed);
				Charge();
				Aiming();
				
				if(attackEndLagTimer <= 0 && attackPressed)
				{
					comboCount = 4;
					state = State.ComboTransition;
					soundPlayed = false;
				}
				
				if(Input.GetButtonDown("Dash") && dashCooldownTimer < 0)
				{
					Dash();
					soundPlayed = false;
				}
				
				if(hurt)
				{
					animator.SetBool("Hurt", true);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", false);
					currentHP -= 1;
						HealthIndicator.Instance.health -= 1;
					hurtTimer = hurtTime;
                    state = State.Hurt;
					disableDamageZones();
					attackPressed = false;
					soundPlayed = false;
				}
				
				break;
				
			case State.Combo4:
				Attack(damageZone4);
				Move(attackingMoveSpeed);
				Charge();
				Aiming();
				
				if(attackEndLagTimer <= 0 && attackPressed)
				{
					comboEndLagTimer = comboEndLag; // NOTE: This line is different. End lag needs to differ from combo time.
					// because the combo window is too long to work as end lag.
					comboCount = 5;
					state = State.ComboTransition;
					soundPlayed = false;
				}
				
				if(Input.GetButtonDown("Dash") && dashCooldownTimer < 0)
				{
					Dash();
					soundPlayed = false;
				}
				
				if(hurt)
				{
					animator.SetBool("Hurt", true);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", false);
					currentHP -= 1;
						HealthIndicator.Instance.health -= 1;
					hurtTimer = hurtTime;
                    state = State.Hurt;
					disableDamageZones();
					attackPressed = false;
					soundPlayed = false;
				}
				
				break;
				
			case State.Combo5:
				// Cannot use the same code as the previous ones because this one uses end lag instead of combo time
				comboEndLagTimer -= Time.deltaTime;
				
				if(!soundPlayed)
				{
					sound.PlaySound("Basic Attack 2");
					soundPlayed = true;
				}
				
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
				
				if(Input.GetButtonDown("Dash") && dashCooldownTimer < 0)
				{
					Dash();
					soundPlayed = false;
				}
				
				if(comboEndLagTimer < 0)
				{
					state = State.Default;
					comboCount = 0;
					animator.SetInteger("Attack", comboCount);
					soundPlayed = false;
				}
				
				if(hurt)
				{
					animator.SetBool("Hurt", true);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", false);
					currentHP -= 1;
						HealthIndicator.Instance.health -= 1;
					hurtTimer = hurtTime;
                    state = State.Hurt;
					disableDamageZones();
					attackPressed = false;
					soundPlayed = false;
				}
				
				Move(attackingMoveSpeed);
				Charge();
				Aiming();
				
				break;
			
			case State.ComboTransition:
				// No need for a timer here, there just needs to be a single frame between attacks so that your direction can update
				Move(attackingMoveSpeed);
				Charge();
				Aiming();
				Turning();
				
				attackPressed = false;
				comboTimer = comboTime;
				attackDurationTimer = attackDuration;
				attackEndLagTimer = attackEndLag;
				state = (State)comboCount;
				animator.SetInteger("Attack", comboCount);
				soundPlayed = false;
				
				break;
			
			case State.Dash:
				dashTimer -= Time.deltaTime;
				invulnerable = true;
				sprite.color = invincibleColour;
				
				Charge();
				
				/*if(direction == Direction.Up)
				{
					//transform.Translate(0, dashSpeed * Time.deltaTime, 0, Space.World);
					rb2d.AddForce(Vector2.up * dashSpeed * Time.deltaTime);
				}
				else if(direction == Direction.Down)
				{
					//transform.Translate(0, -dashSpeed * Time.deltaTime, 0, Space.World);
					rb2d.AddForce(Vector2.down * dashSpeed * Time.deltaTime);
				}
				else if(direction == Direction.Left)
				{
					//transform.Translate(-dashSpeed * Time.deltaTime, 0, 0, Space.World);
					rb2d.AddForce(Vector2.left * dashSpeed * Time.deltaTime);
				}
				else if(direction == Direction.Right)
				{
					//transform.Translate(dashSpeed * Time.deltaTime, 0, 0, Space.World);
					rb2d.AddForce(Vector2.right * dashSpeed * Time.deltaTime);
				}*/
				
				if(!soundPlayed)
				{
					sound.PlaySound("Dash");
					soundPlayed = true;
				}
				
				Vector2 dashDir;
				switch(direction)
				{
					case Direction.Left:
						rb2d.AddForce(Vector2.left * dashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.Right:
						rb2d.AddForce(Vector2.right * dashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.Up:
						rb2d.AddForce(Vector2.up * dashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.Down:
						rb2d.AddForce(Vector2.down * dashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.UpLeft:
						dashDir = new Vector2(-1,1);
						rb2d.AddForce(dashDir.normalized * dashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.UpRight:
						dashDir = new Vector2(1,1);
						rb2d.AddForce(dashDir.normalized * dashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.DownLeft:
						dashDir = new Vector2(-1,-1);
						rb2d.AddForce(dashDir.normalized * dashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.DownRight:
						dashDir = new Vector2(1,-1);
						rb2d.AddForce(dashDir.normalized * dashSpeed * currentSlow * Time.deltaTime);
						break;
				}
				
				if(dashTimer < 0) {
					Turning();
					state = State.Default;
					sprite.color = defaultColour;
					dashCooldownTimer = dashCooldown;
					invulnerable = false;
					soundPlayed = false;
				}
				
				if(Input.GetButtonDown("Attack"))
				{
					comboTimer = comboTime;
					attackDurationTimer = attackDuration;
					attackEndLagTimer = attackEndLag;
					sprite.color = defaultColour;
					state = State.Combo1;
					comboCount = 1;
					animator.SetInteger("Attack", comboCount);
					attackAnimation(direction);
					soundPlayed = false;
				}
				
				if(hurt)
				{
					animator.SetBool("Hurt", true);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", false);
					currentHP -= 1;
						HealthIndicator.Instance.health -= 1;
					hurtTimer = hurtTime;
                    state = State.Hurt;
					disableDamageZones();
					invulnerable = false;
					attackPressed = false;
					soundPlayed = false;
				}
				
				break;
				
			case State.ChargedDash:
				immune = true;
				chargedDashTimer -= Time.deltaTime;
				
				chargedDashDamageZone.gameObject.SetActive(true);
				chargeHitBox.gameObject.SetActive(true);
				
				/*if(direction == Direction.Up)
				{
					//transform.Translate(0, dashSpeed * Time.deltaTime, 0, Space.World);
					rb2d.AddForce(Vector2.up * dashSpeed * Time.deltaTime);
				}
				else if(direction == Direction.Down)
				{
					//transform.Translate(0, -dashSpeed * Time.deltaTime, 0, Space.World);
					rb2d.AddForce(Vector2.down * dashSpeed * Time.deltaTime);
				}
				else if(direction == Direction.Left)
				{
					//transform.Translate(-dashSpeed * Time.deltaTime, 0, 0, Space.World);
					rb2d.AddForce(Vector2.left * dashSpeed * Time.deltaTime);
				}
				else if(direction == Direction.Right)
				{
					//transform.Translate(dashSpeed * Time.deltaTime, 0, 0, Space.World);
					rb2d.AddForce(Vector2.right * dashSpeed * Time.deltaTime);
				}*/
				
				if(!soundPlayed)
				{
					sound.PlaySound("Basic Attack 2");
					soundPlayed = true;
				}
				
				Vector2 chargeDir;
				switch(direction)
				{
					case Direction.Left:
						rb2d.AddForce(Vector2.left * chargedDashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.Right:
						rb2d.AddForce(Vector2.right * chargedDashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.Up:
						rb2d.AddForce(Vector2.up * chargedDashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.Down:
						rb2d.AddForce(Vector2.down * chargedDashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.UpLeft:
						chargeDir = new Vector2(-1,1);
						rb2d.AddForce(chargeDir.normalized * chargedDashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.UpRight:
						chargeDir = new Vector2(1,1);
						rb2d.AddForce(chargeDir.normalized * chargedDashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.DownLeft:
						chargeDir = new Vector2(-1,-1);
						rb2d.AddForce(chargeDir.normalized * chargedDashSpeed * currentSlow * Time.deltaTime);
						break;
					case Direction.DownRight:
						chargeDir = new Vector2(1,-1);
						rb2d.AddForce(chargeDir.normalized * chargedDashSpeed * currentSlow * Time.deltaTime);
						break;
				}
				
				if(chargedDashTimer < 0)
				{
					chargedDashDamageZone.gameObject.SetActive(false);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", true);
					state = State.ChargedAttack;
					chargedAttackTimer = chargedAttackTime;
					soundPlayed = false;
				}
				
				if(hurt)
				{
					animator.SetBool("Hurt", true);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", false);
					currentHP -= 1;
						HealthIndicator.Instance.health -= 1;
					hurtTimer = hurtTime;
                    state = State.Hurt;
					disableDamageZones();
					immune = false;
					attackPressed = false;
					soundPlayed = false;
				}
				
				break;
				
			case State.ChargedAttack:
				immune = true;
				chargedAttackTimer -= Time.deltaTime;
				
				chargedAttackDamageZone.gameObject.SetActive(true);
				chargeHitBox.gameObject.SetActive(true);
				
				if(chargedAttackTimer < 0)
				{
					chargedAttackDamageZone.gameObject.SetActive(false);
					chargeHitBox.gameObject.SetActive(false);
					animator.SetBool("Charged Attacking", false);
					state = State.Default;
					immune = false;
					soundPlayed = false;
				}
				
				// You can interrupt your charged attack your basic attack string if you wish to start straight away
				if(Input.GetButtonDown("Attack"))
				{
					disableDamageZones();
					comboTimer = comboTime;
					attackDurationTimer = attackDuration;
					attackEndLagTimer = attackEndLag;
					state = State.Combo1;
					comboCount = 1;
					animator.SetInteger("Attack", comboCount);
					attackAnimation(direction);
					immune = false;
					soundPlayed = false;
				}
				
				if(hurt)
				{
					animator.SetBool("Hurt", true);
					animator.SetBool("Charged", false);
					animator.SetBool("Charged Attacking", false);
					currentHP -= 1;
						HealthIndicator.Instance.health -= 1;
					hurtTimer = hurtTime;
                    state = State.Hurt;
					disableDamageZones();
					immune = false;
					attackPressed = false;
					soundPlayed = false;
				}
				
				break;
			case State.Hurt:
				//invulnerable = true;
				
				if(hurtTimer > 0)
				{
					hurtTimer -= Time.deltaTime;
					sprite.color = damageColour;
					rb2d.AddForce(knockbackVector * Time.deltaTime * knockbackSpeed);
				}
				else
				{
					animator.SetBool("Hurt", false);
					state = State.Default;
					sprite.color = defaultColour;
					hurt = false;
					//invulnerable = false;
					soundPlayed = false;
				}
				
				break;
		}
		
		// Global dash cooldown
		if(dashCooldownTimer >= 0)
		{
			dashCooldownTimer -= Time.deltaTime;
		}
		
		// Invincibility frames after getting hurt
		if(hurtInvincibilityTimer >= 0)
		{
			hurtInvincibilityTimer -= Time.deltaTime;
			invincible = true;
			if((int)(hurtInvincibilityTimer*8)%2 == 0)
			{
				sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
			}
			else
			{
				sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
			}
		}
		else
		{
			invincible = false;
		}
		
		sprite.gameObject.transform.rotation = Quaternion.identity;
	}
	
	// Add this to any state where you can move
	void Move(float moveModifier) {
		float velocity = moveSpeed * moveModifier * currentSlow * Time.deltaTime;
		float verticalMove = Input.GetAxis("Vertical") * velocity;
		float horizontalMove = Input.GetAxis("Horizontal") * velocity;
		//transform.Translate(horizontalMove, verticalMove, 0, Space.World);
		//position.x += horizontalMove;
		//position.y += verticalMove;
		//rb2d.MovePosition(position);
		
		position.x = horizontalMove;
		position.y = verticalMove;
		rb2d.AddForce(position);
	}
	
	// Add this to any state where you can turn
	void Turning() {
		switch(direction)
		{
			case Direction.Left:
				rotation = 90f;
				break;
			case Direction.Right:
				rotation = -90f;
				break;
			case Direction.Up:
				rotation = 0f;
				break;
			case Direction.Down:
				rotation = 180f;
				break;
			case Direction.UpLeft:
				rotation = 45f;
				break;
			case Direction.UpRight:
				rotation = -45f;
				break;
			case Direction.DownLeft:
				rotation = 135f;
				break;
			case Direction.DownRight:
				rotation = -135f;
				break;
		}
		
		transform.localRotation = Quaternion.Euler(0, 0, rotation);
	}
	
	// The reason this is separate from turning is that you can still aim without actually turning your character in some states
	void Aiming() {
		if(!Input.GetButton("Attack"))
		{
			if(Input.GetButton("Left") && Input.GetButton("Up"))
			{
				direction = Direction.UpLeft;
				
				character.SetBool("WalkingBack", true);
				character.SetBool("WalkingForward", false);
				character.SetBool("WalkingLeft", false);
				character.SetBool("WalkingRight", false);
				character.SetBool("Idle", false);
			}
			else if(Input.GetButton("Left") && Input.GetButton("Down"))
			{
				direction = Direction.DownLeft;
				
				character.SetBool("WalkingBack", false);
				character.SetBool("WalkingForward", true);
				character.SetBool("WalkingLeft", false);
				character.SetBool("WalkingRight", false);
				character.SetBool("Idle", false);
			}
			else if(Input.GetButton("Right") && Input.GetButton("Up"))
			{
				direction = Direction.UpRight;
				
				character.SetBool("WalkingBack", true);
				character.SetBool("WalkingForward", false);
				character.SetBool("WalkingLeft", false);
				character.SetBool("WalkingRight", false);
				character.SetBool("Idle", false);
			}
			else if(Input.GetButton("Right") && Input.GetButton("Down"))
			{
				direction = Direction.DownRight;
				
				character.SetBool("WalkingBack", false);
				character.SetBool("WalkingForward", true);
				character.SetBool("WalkingLeft", false);
				character.SetBool("WalkingRight", false);
				character.SetBool("Idle", false);
			}
			else if (Input.GetButton("Left"))
			{
				direction = Direction.Left;
				
				character.SetBool("WalkingBack", false);
				character.SetBool("WalkingForward", false);
				character.SetBool("WalkingLeft", true);
				character.SetBool("WalkingRight", false);
				character.SetBool("Idle", false);
			}
			else if (Input.GetButton("Right"))
			{
				direction = Direction.Right;
				
				character.SetBool("WalkingBack", false);
				character.SetBool("WalkingForward", false);
				character.SetBool("WalkingLeft", false);
				character.SetBool("WalkingRight", true);
				character.SetBool("Idle", false);
			}
			else if (Input.GetButton("Up"))
			{
				direction = Direction.Up;
				
				character.SetBool("WalkingBack", true);
				character.SetBool("WalkingForward", false);
				character.SetBool("WalkingLeft", false);
				character.SetBool("WalkingRight", false);
				character.SetBool("Idle", false);
			}
			else if (Input.GetButton("Down"))
			{
				direction = Direction.Down;
				
				character.SetBool("WalkingBack", false);
				character.SetBool("WalkingForward", true);
				character.SetBool("WalkingLeft", false);
				character.SetBool("WalkingRight", false);
				character.SetBool("Idle", false);
			}
			else
			{
				// make sure the player stops instantly when none of the movement keys are pressed
				rb2d.velocity = Vector3.zero;
				
				character.SetBool("WalkingBack", false);
				character.SetBool("WalkingForward", false);
				character.SetBool("WalkingLeft", false);
				character.SetBool("WalkingRight", false);
				character.SetBool("Idle", true);
			}
			
			transform.localRotation = Quaternion.Euler(0, 0, rotation);
		}
	}
	
	void Attack(GameObject damageZone) {
		comboTimer -= Time.deltaTime;
		
		if(!soundPlayed)
		{
			sound.PlaySound("Basic Attack 1");
			soundPlayed = true;
		}
		
		if(attackEndLagTimer > 0)
		{
			attackEndLagTimer -= Time.deltaTime;
		}
		
		if(Input.GetButtonDown("Attack"))
		{
			attackPressed = true;
			attackAnimation(direction);
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
		if(Input.GetButton("Charge") && chargeTimer >= chargeTime/2)
		{
			if(!soundPlayed)
			{
				sound.PlaySound("Charge");
				soundPlayed = true;
			}
			
			chargeTimer -= Time.deltaTime;
			sprite.color = chargingColour1;
		}
		if(Input.GetButton("Charge") && chargeTimer < chargeTime/2 && chargeTimer > 0)
		{
			chargeTimer -= Time.deltaTime;
			sprite.color = chargingColour2;
		}
		else if(Input.GetButton("Charge") && chargeTimer <= 0)
		{
			sprite.color = chargedColour;
		}
		
		if(Input.GetButtonUp("Charge") && chargeTimer > 0)
		{
			chargeTimer = chargeTime;
			sprite.color = defaultColour;
			soundPlayed = false;
			sound.StopSound();
		}
		else if(Input.GetButtonUp("Charge") && chargeTimer <= 0)
		{
			Turning(); // Need to set the rotation angle whenever the dash goes off
			attackPressed = false; // So the input buffer doesn't carry into the next combo
			chargeTimer = chargeTime;
			chargedDashTimer = chargedDashTime;
			animator.SetBool("Charged", true);
			state = State.ChargedDash;
			sprite.color = defaultColour;
			comboCount = 0; // Reset the combo counter for the animator
			animator.SetInteger("Attack", comboCount);
			disableDamageZones();
			soundPlayed = false;
		}
	}
	
	void Dash()
	{
		disableDamageZones();
		dashTimer = dashTime;
		state = State.Dash;
		comboCount = 0;
		animator.SetInteger("Attack", comboCount);
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
		chargeHitBox.gameObject.SetActive(false);
		
		immune = false;
		invulnerable = false;
		soundPlayed = false;
	}
	
	void OnTriggerStay2D(Collider2D c)
	{
		// if you stay in the damage zone for longer than the hurt time, you get damaged again
		if(c.gameObject.tag == "damager" && !invulnerable)
			if(damageStayTimer > 0)
			{
				damageStayTimer -= Time.deltaTime;
			}
			else
			{
				takeDamage(c);
			}
			
			if(c.gameObject.tag == "slower" && !invulnerable)
			{
				currentSlow = slowModifier;
			}
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{	
		// take damage once
		if(c.gameObject.tag == "damager")
		{
			if(!invincible)
			{
				damageStayTimer = 0;
				if(!invulnerable)
				{
					if(immune && (c.gameObject.GetComponent<Damager>().damageType == 1)) // not invulnerable, but immune. Still take damage if the damager is a projectile.
					{
						takeDamage(c);
					}
					else if(!immune) // not invulnerable and not immune
					{
						takeDamage(c);
					}
				}
				else if(c.gameObject.GetComponent<Damager>().damageType == 2) // if the damager is unavoidable
				{
					takeDamage(c);
				}
			}
		}
		
		// Health pickups
		if(c.gameObject.tag == "HealthPickup")
		{
			HealthIndicator.Instance.health = Mathf.Clamp(HealthIndicator.Instance.health + HealthIndicator.Instance.numOfHearts/2, 0, HealthIndicator.Instance.numOfHearts);
			Destroy(c.gameObject);
		}
		
		// Attack pickups
		if(c.gameObject.tag == "AttackUp")
		{
			damageZone1.GetComponent<EnemyDamager>().damage++;
			damageZone2.GetComponent<EnemyDamager>().damage++;
			damageZone3.GetComponent<EnemyDamager>().damage++;
			damageZone4.GetComponent<EnemyDamager>().damage++;
			damageZone5.GetComponent<EnemyDamager>().damage++;
			chargedAttackDamageZone.GetComponent<EnemyDamager>().damage++;
			chargedDashDamageZone.GetComponent<EnemyDamager>().damage++;
			chargeHitBox.GetComponent<EnemyDamager>().damage++;
			Destroy(c.gameObject);
		}
		
		// HP Up pickups
		if(c.gameObject.tag == "HealthUp")
		{
			HealthIndicator.Instance.numOfHearts += 1;
			HealthIndicator.Instance.health += 1;
			Destroy(c.gameObject);
		}
		
		if(c.gameObject.tag == "slower" && !invulnerable)
		{
			currentSlow = slowModifier;
		}
	}
	
	void OnTriggerExit2D(Collider2D c)
	{
		if(c.gameObject.tag == "slower")
		{
			currentSlow = 1;
		}
	}
	
	
	
	void takeDamage(Collider2D c)
	{
		//camShake.Shake();
		damageDelayTimer = damageIFrames;
		damageStayTimer = damageIFrames;
		hurtInvincibilityTimer = damageIFrames;
		sound.PlaySound("Hurt");
		hurt = true;
		comboCount = 0;
		
		knockbackVector = transform.position - c.gameObject.GetComponent<Damager>().source;
		knockbackVector = knockbackVector.normalized;
		knockbackSpeed = c.gameObject.GetComponent<Damager>().knockbackPower;
		
		// for playtesting
		//eventTracking.addDamageEvent(c.gameObject.name);
	}
	
	public void findCamera()
	{
		camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
	}
	
	void attackAnimation(Direction dir)
	{
		character.SetBool("WalkingBack", false);
		character.SetBool("WalkingForward", false);
		character.SetBool("WalkingLeft", false);
		character.SetBool("WalkingRight", false);
		character.SetBool("Idle", false);
		
		switch(direction)
		{
			case Direction.Left:
				character.SetTrigger("LeftAttack");
				break;
			case Direction.Right:
				character.SetTrigger("RightAttack");
				break;
			case Direction.Up:
				character.SetTrigger("BackAttack");
				break;
			case Direction.Down:
				character.SetTrigger("ForwardAttack");
				break;
			case Direction.UpLeft:
				character.SetTrigger("BackAttack");
				break;
			case Direction.UpRight:
				character.SetTrigger("BackAttack");
				break;
			case Direction.DownLeft:
				character.SetTrigger("ForwardAttack");
				break;
			case Direction.DownRight:
				character.SetTrigger("ForwardAttack");
				break;
		}
	}
}
