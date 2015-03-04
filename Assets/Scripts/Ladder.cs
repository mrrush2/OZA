using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	public bool onLadder = false;
	// Use this for initialization
	void OnTriggerEnter(Collider2D other)
	{
		if (other.tag == "Player") 
			onLadder = true;
	}
	void OnTriggerExit(Collider2D other)
	{
		if (other.tag == "Player")
			onLadder = false;
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
