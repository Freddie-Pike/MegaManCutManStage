using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	// Moving variables.
	public float moveSpeed;

	// Jumping variables.
	public float jumpShortSpeed = 3f;
	public float jumpSpeed = 6f;
	bool jump = false;
	bool jumpCancel = false;
	
	// Ground checking variables.
	public Transform groundCheck;
	public float groundCheckRadious;
	public LayerMask whatIsGround;
	private bool grounded;

	// Animation variables.
	private Animator anim;

	// Shooting pellet variables.
	public Transform firePoint;
	public GameObject busterPellet;
	
	// Use this for initialization.
	void Start () 
	{
		anim = GetComponent<Animator> (); // Get Animator.
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
		if (Input.GetKeyDown (KeyCode.Space) && grounded)
		{
			jump = true;
		}

		// If jump key is let go, then jumpCancel is true. 
		if (Input.GetKeyUp (KeyCode.Space) && !grounded)
		{
			jumpCancel = true;
		}

		// Move left as depended on moveSpeed.
		if (Input.GetKey (KeyCode.D))
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}

		// Move right as depended on moveSpeed.
		if (Input.GetKey (KeyCode.A))
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}

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

		// If shoot key is tapped, let mega man shoot.
		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			anim.SetBool ("IsShooting", true); // For shooting animations.
			Instantiate (busterPellet, firePoint.position, firePoint.rotation);
		}
		else
		{
			anim.SetBool ("IsShooting", false); // For shooting animations.
		}
	}
}
