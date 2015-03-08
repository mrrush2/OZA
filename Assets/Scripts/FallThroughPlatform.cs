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

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			if (Input.GetKey (KeyCode.S) || (player.transform.position.y - .28) < this.transform.position.y) { // This is for falling through / jumping up onto the platform
				plat = true;
			}	
			if (plat) {
				Physics2D.IgnoreCollision (collider2D, other, true);
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			Physics2D.IgnoreCollision (collider2D, other, false);
			plat = false;
		}
	}
}
