using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour {

	GameObject player;
	bool followingPlayer = true;


	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called constantly
	void Update ()
	{
			if (followingPlayer)
			{
				transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
			}
	}

	// FixedUpdate is called once per frame
	void FixedUpdate()
	{

	}
}
