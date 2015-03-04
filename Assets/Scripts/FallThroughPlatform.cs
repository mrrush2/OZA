using UnityEngine;
using System.Collections;

public class FallThroughPlatform : MonoBehaviour {
	public GameObject player;
	bool plat = false;
	// Use this for initialization
	void Start () {
	
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
		if (Input.GetKey (KeyCode.S)) // This is for falling through the platform
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
