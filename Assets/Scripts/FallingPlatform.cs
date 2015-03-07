using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {

	float originX;
	float originY;

	// Use this for initialization
	void Start () 
	{

		originX = transform.position.x; //Get initial positions
		originY = transform.position.y;

	}
	
	// Update is called constantly
	void Update () 
	{
		transform.Translate (originX - transform.position.x, 0, 0); //Locks the X-axis of the platform
		if (rigidbody2D.velocity.y < 0)
			rigidbody2D.AddForce (new Vector2(0, -20));
	}

	// FixedUpdate is called once per frame
	void FixedUpdate ()
	{

	}

}
