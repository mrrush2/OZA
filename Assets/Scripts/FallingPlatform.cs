using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {

	float originX;
	float originY;
	Transform origin;
	public Rigidbody2D platform;
	GameObject platformInstance;



	// Use this for initialization
	void Start () 
	{

		originX = transform.position.x; //Get initial positions
		originY = transform.position.y;
		origin = this.transform;

	}
	
	// Update is called constantly
	void Update () 
	{
		transform.Translate (originX - transform.position.x, 0, 0); //Locks the X-axis of the platform
		if (rigidbody2D.velocity.y < 0) // Fake Gravity
			rigidbody2D.AddForce (new Vector2(0, -20));
/*		if (rigidbody2D.velocity.y < -1) 
		{
			Destroy (this.gameObject);
			platformInstance = Instantiate ( platform, new Vector3(originX, originY, 0), origin.rotation) as GameObject;
			platformInstance.rigidbody2D.velocity = new Vector2(0,0);
		} 
*/

	}

	// FixedUpdate is called once per frame
	void FixedUpdate ()
	{

	}

}
