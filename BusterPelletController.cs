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

	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy (gameObject); // Destroy pellet when it hits an enemy.
	}
}
