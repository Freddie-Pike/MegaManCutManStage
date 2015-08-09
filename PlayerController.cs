using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	// Rigidbody variable
	private Rigidbody2D rbody2D; 

	// Animation variables.
	private Animator anim;

	// Moving variables.
	public float moveSpeed;
	private float moveVelocity;

	// Jumping variables.
	public float jumpShortSpeed = 3f;
	public float jumpSpeed = 6f;
	bool jump = false;
	bool jumpCancel = false;
	
	// Ground checking variables.
	public Transform groundCheck;
	public float groundCheckRadious;
	public LayerMask whatIsGround;
	public bool grounded;

	// Shooting pellet variables.
	public Transform firePoint;
	public GameObject busterPellet;
	public float shotDelay;
	private float lastTimeFired;

	// Ladder variables
	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;
	private LadderEndZone ladderEndZone;
	private BoxCollider2D canClimbDown;
	
	// Use this for initialization.
	void Start () 
	{
		anim = GetComponent<Animator> (); // Get Animator.

		rbody2D = GetComponent<Rigidbody2D>();

		gravityStore = rbody2D.gravityScale;

	}

	void FixedUpdate()
	{
		// Normal Jump (full speed).
		if (jump) 
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
			jump = false;
		}

		// If jump is cancelled, allow mega man to fall to ground. 
		if (jumpCancel) 
		{
			// If velocity.y is less than the jumpShortSpeed variable, fall to ground.
			if (GetComponent<Rigidbody2D>().velocity.y > jumpShortSpeed)
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpShortSpeed);
			jumpCancel = false;
		}
	}

	// Update is called once per frame.
	void Update () 
	{
		// Boolean that will check whether mega man is on the ground or not. 
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadious, whatIsGround);

		anim.SetBool ("Grounded", grounded); // For Jumping animations.

		// If jump key is pressed down, then jump is true. 
		if (Input.GetButtonDown ("Jump") && grounded)
		{
			jump = true;
		}

		// If jump key is let go, then jumpCancel is true. 
		if (Input.GetButtonUp ("Jump") && !grounded)
		{
			jumpCancel = true;
		}


		// Walking 
		moveVelocity = moveSpeed * Input.GetAxisRaw("Horizontal");
		rbody2D.velocity = new Vector2(moveVelocity, rbody2D.velocity.y);
		anim.SetFloat ("Speed", Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x)); // For Walking animations.

		// If-else statement to determine which direction the player is facing, used to correct animations. 
		if (GetComponent<Rigidbody2D> ().velocity.x > 0) 
		{
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} 
		else if (GetComponent<Rigidbody2D>().velocity.x < 0)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}

		// If shoot key is tapped, let mega man shoot on a specific time delay.
		if (Input.GetButtonDown ("Fire1") && Time.time - lastTimeFired > shotDelay) 
		{
			lastTimeFired = Time.time; // Time we shot pellet
			Fire ();
		}
		else
		{
			anim.SetBool ("IsShooting", false); // For shooting animations.
		}


		// Change gravity when by ladder in order for mega man to move up it. 
		if (onLadder)
		{
			if (Input.GetAxis("Vertical") > 0)
			{
				ClimbLadder();
			}

			if (Input.GetAxis("Vertical") < 0)
			{
				ClimbLadder();
			}

			if (Input.GetAxis ("Vertical") == 0)
			{
				anim.SetBool ("Grounded", true);
			}

			if (Input.GetButton ("Jump"))
			{
				rbody2D.gravityScale = gravityStore;
				anim.SetBool ("onLadder", false);
				anim.SetBool ("Grounded", grounded);
			}
		}

		if (!onLadder)
		{
			anim.SetBool ("onLadder", false);
			rbody2D.gravityScale = gravityStore;
			anim.SetBool ("Grounded", grounded);
		}
	}

	// Allows Mega Man to climb up a ladder.
	void ClimbLadder()
	{
		anim.SetBool ("Grounded", true);
		anim.SetBool ("onLadder", true);
		rbody2D.gravityScale = 0f;
		climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
		rbody2D.velocity = new Vector2(rbody2D.velocity.x, climbVelocity);
	}

	// Creates the pellet that Mega Man will shoot. 
	void Fire()
	{
		anim.SetBool ("IsShooting", true); // For shooting animations.
		Instantiate (busterPellet, firePoint.position, firePoint.rotation);
	}
}
