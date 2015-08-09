using UnityEngine;
using System.Collections;

public class LadderEndZone : MonoBehaviour 
{
	private PlayerController player;
	private BoxCollider2D bCollider2D;

	// Use this for initialization
	void Start () 
	{
		bCollider2D = GetComponent<BoxCollider2D>();

		player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// This enables the player to jump at the top of the ladder, and to go down a ladder once they have climbed it. 
		if (player.grounded == true && Input.GetAxis("Vertical") < 0)
		{
			bCollider2D.isTrigger = true;
		}
		else if (player.grounded == true)
		{
			bCollider2D.isTrigger = false;
		}
		else 
		{
			bCollider2D.isTrigger = true;
		}
	}
}
