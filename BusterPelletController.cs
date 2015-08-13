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

	// Destroy enemy on contact with pellet. Enemies along with health system to be added. 
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Enemy")
		{
			Destroy (gameObject);
		}

	}

	// When object is destroyed, decreased the pellet counter so there can only be 3 pellets on screen. 
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "Enemy")
		{
			player.pelletCounter--;
		}
	}

	// When Buster pellet is offscreen, destroy it so it doesn't mess with performance. Also decrease pellet counter
	// so only 3 pellets are visible on screen.
	void OnBecameInvisible()
	{
		player.pelletCounter--;
		Destroy (gameObject);
	}
}
