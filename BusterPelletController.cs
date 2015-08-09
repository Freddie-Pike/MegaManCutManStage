using UnityEngine;
using System.Collections;

public class BusterPelletController : MonoBehaviour 
{	
	public float speed; // Determine shooting speed. 
	public PlayerController player;
	
	// Use this for initialization.
	void Start () 
	{
		player = FindObjectOfType<PlayerController> (); // Get Player Controller.

		// If statement that determines which direction mega man's buster pellet goes.
		if (player.transform.localScale.x < 0) 
		{
			speed = -speed;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		// Determine the speed in which the projectile goes. 
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, GetComponent<Rigidbody2D> ().velocity.y);
	}

    // Destroy pellet when it has a collision with anything. 
	void OnCollisionEnter2D(Collision2D coll)
	{
		Destroy (gameObject);
	}
}
