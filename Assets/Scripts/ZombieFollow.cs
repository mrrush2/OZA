using UnityEngine;
using System.Collections;

public class ZombieFollow : MonoBehaviour {
	public Transform target;//set target from inspector instead of looking in Update
	public float speed = .5f;

	void Update() {
		//rotate to look at the player
		target = GameObject.FindWithTag ("Player").transform;
		Vector3 forwardMovement = new Vector3 (0, 0, -1);
		transform.LookAt(target.position);
		//correcting the original rotation
		transform.Rotate(new Vector3(0,-90,0));
			transform.position += (transform.TransformDirection (Vector2.right) * speed * Time.deltaTime) * 10;

	}

}
