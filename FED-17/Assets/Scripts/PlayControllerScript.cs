using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControllerScript : MonoBehaviour
{
	[Header("General")]
	public int playerId = 1;
	public bool dummy = false;

	[Header("Movement")]
	public float maxGroundSpeed = 10f;
	public float maxAirSpeed = 5f;
	bool facingRight = true;

	Rigidbody2D rb;

	
	public bool grounded = false;
	public Transform[] groundChecks;
	float groundRadius = 0.05f;
	public LayerMask whatIsGround;

	public float jumpForce = 700f;
	public int doubleJumpCounter = 0;
	int initialDoubleJumpCounter;
	bool jumpButton = false;
	bool jumpButtonDown = false;
	bool jumpButtonUp = false;

	float velo = 0f;
	public float maxVelocity = 12f;
	public float maxNegativeVelocity = -10f;

	[Header("Fighting")]
	public Collider2D punchTrigger;
	public Collider2D kickTrigger;
	public Collider2D upperCutTrigger;
	public Collider2D lowerKickTrigger;

	public float chargeAmountPerSecond = 1f;
	public float chargeAmount = 0f;
	public float chargeThreshold = 0.2f;
	public Transform projectileOrigin;
	public GameObject projectile;
	public float projectileSpeed;

	bool fire1Button = false;
	bool fire1ButtonDown = false;
	bool fire1ButtonUp = false;
	bool fire2Button = false;
	bool fire2ButtonDown = false;
	bool fire2ButtonUp = false;
	bool fire3Button = false;
	bool fire3ButtonDown = false;
	bool fire3ButtonUp = false;

	public enum eDamageTrigger { Punch, Kick, UpperCut, CircleKick, LowerKick };

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		initialDoubleJumpCounter = doubleJumpCounter;

		punchTrigger.GetComponent<DamageTrigger>().SetCallback( this );
		kickTrigger.GetComponent<DamageTrigger>().SetCallback( this );
		upperCutTrigger.GetComponent<DamageTrigger>().SetCallback( this );
		lowerKickTrigger.GetComponent<DamageTrigger>().SetCallback( this );
		punchTrigger.enabled = false;
		kickTrigger.enabled = false;
		upperCutTrigger.enabled = false;
		lowerKickTrigger.enabled = false;

		if( !projectileOrigin )
			Debug.LogError("No projectile origin set!");
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		foreach( Transform groundCheck in groundChecks )
		{
			grounded = false;
			Collider2D ground = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
			if( ground != null )
			{
				grounded = true;
				break;
			}
		}
		
		velo = rb.velocity.y;
		if( velo > maxVelocity )
			rb.velocity = new Vector2( rb.velocity.x, maxVelocity );
		else if( velo < maxNegativeVelocity )
			rb.velocity = new Vector2( rb.velocity.x, maxNegativeVelocity );

		float move = Input.GetAxis("Horizontal");
		if( dummy )
			move = 0f;
		float hSpeed = maxAirSpeed;
		if( grounded )
		{
			hSpeed = maxGroundSpeed;
			doubleJumpCounter = initialDoubleJumpCounter;
		}
		else if( !grounded && velo == 0f )
		{
			hSpeed = 0f;
		}
		rb.velocity = new Vector2( move * hSpeed, rb.velocity.y );

		// Flip sprite
		if( move > 0 && !facingRight )
			Flip();
		else if( move < 0 && facingRight )
			Flip();
	}

	void Update()
	{
		if( dummy )
			return;

		DetermineButtonStates();

		// Determine Jump Button down

		if( grounded && jumpButtonDown ) //Input.GetKeyDown(KeyCode.Space) )
		{	// Jump
			rb.AddForce( new Vector2(0, jumpForce), ForceMode2D.Impulse );
			doubleJumpCounter = initialDoubleJumpCounter;
			//Debug.Log("jump");
		}
		else if( !grounded && (doubleJumpCounter > 0) && jumpButtonDown ) //Input.GetKeyDown(KeyCode.Space) )
		{	// Double jump
			doubleJumpCounter--;
			rb.velocity = new Vector2(rb.velocity.x, 0f); // Neutralize previous vertical velocity
			rb.AddForce( new Vector2(0, jumpForce), ForceMode2D.Impulse );
			Debug.Log("double jump");
		}

		// Charging projectile
		if( fire1Button && (Input.GetAxisRaw("Horizontal") == 0f) )
		{
			chargeAmount += chargeAmountPerSecond * Time.deltaTime;
			if( chargeAmount > 1f )
				chargeAmount = 1f;
		}
		else if( (chargeAmount > chargeThreshold) && fire1ButtonUp /*&& (Input.GetAxisRaw("Horizontal") == 0f)*/ )
        {
			ShootProjectile( chargeAmount );
		}
		else
			chargeAmount = 0f; // Neutralize charge

		ResetButtonStates();
	}

	void ShootProjectile( float chargeAmount )
	{ 
		Debug.Log("Shoot charge: " + chargeAmount );
		GameObject projectileClone = Instantiate( projectile, projectileOrigin.position, Quaternion.identity ) as GameObject;
		float speed = projectileSpeed;
		if( !facingRight )
			speed = -projectileSpeed;
		projectileClone.GetComponent<Rigidbody2D>().velocity = new Vector2( speed, 0f );
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void DamageTriggerCallback( int Id, Collider2D collider )
	{
		Debug.Log( "Player: " + playerId + " received damage from : " + (eDamageTrigger)Id + " of player: " + collider.GetComponent<PlayControllerScript>().GetPlayerId() );
	}

	public int GetPlayerId()
	{
		return playerId;
	}

	void DetermineButtonStates()
	{
		// Jump Button
		if( (Input.GetAxisRaw("Jump") >= 1f) && !jumpButton )
		{	// Jump button down impulse
			jumpButton = true;
			jumpButtonDown = true;
			//Debug.Log("Jump button down impulse");
		}
		else if( (Input.GetAxisRaw("Jump") < 1f) && jumpButton )
		{	// Jump button up impulse
			jumpButton = false;
			jumpButtonUp = true;
			//Debug.Log("Jump button up impulse");
		}
		else if( Input.GetAxisRaw("Jump") < 1f )
		{	// Jump button unpressed
			jumpButtonUp = false;
			jumpButton = false;
		}

		// Fire1 Button
		if( (Input.GetAxisRaw("Fire1") >= 1f) && !fire1Button )
		{	// Jump button down impulse
			fire1Button = true;
			fire1ButtonDown = true;
			//Debug.Log("Fire1 button down impulse");
		}
		else if( (Input.GetAxisRaw("Fire1") < 1f) && fire1Button )
		{	// Jump button up impulse
			fire1Button = false;
			fire1ButtonUp = true;
			//Debug.Log("Fire1 button up impulse");
		}
		else if( Input.GetAxisRaw("Fire1") < 1f )
		{	// Jump button unpressed
			fire1ButtonUp = false;
			fire1Button = false;
		}

		// Fire2 Button
		if( (Input.GetAxisRaw("Fire2") >= 1f) && !fire2Button )
		{	// Jump button down impulse
			fire2Button = true;
			fire2ButtonDown = true;
			//Debug.Log("Fire1 button down impulse");
		}
		else if( (Input.GetAxisRaw("Fire2") < 1f) && fire2Button )
		{	// Jump button up impulse
			fire2Button = false;
			fire2ButtonUp = true;
			//Debug.Log("Fire1 button up impulse");
		}
		else if( Input.GetAxisRaw("Fire2") < 1f )
		{	// Jump button unpressed
			fire2ButtonUp = false;
			fire2Button = false;
		}

		// Fire3 Button
		if( (Input.GetAxisRaw("Fire3") >= 1f) && !fire3Button )
		{	// Jump button down impulse
			fire3Button = true;
			fire3ButtonDown = true;
			//Debug.Log("Fire1 button down impulse");
		}
		else if( (Input.GetAxisRaw("Fire3") < 1f) && fire3Button )
		{	// Jump button up impulse
			fire3Button = false;
			fire3ButtonUp = true;
			//Debug.Log("Fire1 button up impulse");
		}
		else if( Input.GetAxisRaw("Fire3") < 1f )
		{	// Jump button unpressed
			fire3ButtonUp = false;
			fire3Button = false;
		}
	}

	void ResetButtonStates()
	{
		// Reset button impulses
		fire1ButtonDown = false;
		fire1ButtonUp = false;
		fire2ButtonDown = false;
		fire2ButtonUp = false;
		fire3ButtonDown = false;
		fire3ButtonUp = false;
		jumpButtonDown = false;
		jumpButtonUp = false;
	}
}
