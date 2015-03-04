using UnityEngine;
using System.Collections;

public class FallThroughPlatform : MonoBehaviour {
	GameObject player;
	GameObject groundCheck;

	bool plat = false;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D player)
	{
		Debug.Log ("On platform");
	}
	void OnTriggerStay2D(Collider2D player)
	{
		if (Input.GetKey (KeyCode.S) || (player.transform.position.y-.28) < this.transform.position.y) // This is for falling through / jumping up onto the platform
		{
			plat = true;
		}	
		if (plat) 
		{
			Physics2D.IgnoreCollision (collider2D, player, true);
		}
	}
	void OnTriggerExit2D(Collider2D player)
	{
		Debug.Log ("Fell through platform");
		Physics2D.IgnoreCollision (collider2D, player, false);
		plat = false;
	}
}
