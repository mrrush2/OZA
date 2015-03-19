using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour {

	GameObject player;
	bool followingPlayer = true;

	float Xpos, Ypos, Zpos;


	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		Xpos = player.transform.position.x;
		Ypos = player.transform.position.y;
		Zpos = -10;
		transform.position = new Vector3 (Xpos, Ypos, Zpos);
	}
	
	// Update is called constantly
	void Update ()
	{
		if (followingPlayer)
		{
			////// X //////
			if ((transform.position.x < player.transform.position.x + 1) && player.transform.localScale == new Vector3(1, 1, 1))
			{
				transform.Translate (5 * Time.deltaTime,0 , 0);
			}


			if ((transform.position.x > player.transform.position.x - 1) && player.transform.localScale == new Vector3(-1, 1, 1))
			{
				transform.Translate (-5 * Time.deltaTime,0 , 0);
			}
		
			////// Y //////
			Ypos = player.transform.position.y;

			////// Set Position //////
			transform.position = new Vector3(transform.position.x, Ypos, Zpos);
		}
	}

	// FixedUpdate is called once per frame
	void FixedUpdate()
	{

	}
}
