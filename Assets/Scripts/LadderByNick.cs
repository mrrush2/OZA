using UnityEngine;
using System.Collections;

public class LadderByNick : MonoBehaviour {
	
	public GameObject player;
	public CharacterKontroller playerScript;



	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called constantly
	void Update () 
	{

	}

	void OnTriggerEnter2D(Collider2D player)
	{
		Debug.Log ("Entered Ladder");
	}
	void OnTriggerStay2D(Collider2D player)
	{
		if (Input.GetKey (KeyCode.W)) // This is for going up the ladder
		{
			playerScript.onLadder = true;
			player.rigidbody2D.velocity = new Vector2(0,0);
			player.rigidbody2D.gravityScale = 0;
			player.transform.Translate (0, 1 * Time.deltaTime, 0);
		}
		if (Input.GetKey (KeyCode.S)) // This is for going down the ladder
		{
			playerScript.onLadder = true;
			player.rigidbody2D.velocity = new Vector2(0,0);
			player.rigidbody2D.gravityScale = 0;
			player.transform.Translate (0, -1 * Time.deltaTime, 0);
		}
		if (Input.GetKeyDown (KeyCode.Space) && player.rigidbody2D.gravityScale == 0) // This is for jumping off the ladder
		{
			playerScript.onLadder = false;
			player.rigidbody2D.gravityScale = 1;
			player.rigidbody2D.AddForce (new Vector2(0, playerScript.jumpForce));
		}
		if (playerScript.onLadder) // This centers the player on the ladder
		{
			player.transform.Translate ((this.transform.position.x - player.transform.position.x) , 0, 0);
		}
		if (playerScript.onLadder && playerScript.grounded) 
		{
			playerScript.onLadder = false;
			player.rigidbody2D.gravityScale = 1;
		}



	}
	void OnTriggerExit2D(Collider2D player)
	{
		Debug.Log ("Exited Ladder");
		player.rigidbody2D.gravityScale = 1;
		playerScript.onLadder = false;
	}
}
