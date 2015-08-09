using UnityEngine;
using System.Collections;

public class LadderController : MonoBehaviour 
{
	private PlayerController player;

	// Use this for initialization
	void Start () 
	{
		player = FindObjectOfType<PlayerController>();
	}

    // When Mega Man comes across a ladder, make it so the player can climb on the ladder.
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name == "Player")
		{
			player.onLadder = true;
		}
	}

	// When Mega Man comes off a ladder, make it so the player can't do anything with a ladder anymore.
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.name == "Player")
		{
			player.onLadder = false;
		}
	}
}
