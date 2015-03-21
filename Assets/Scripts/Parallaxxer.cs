using UnityEngine;
using System.Collections;

public class Parallaxxer : MonoBehaviour {

	public GameObject controller;
	public int parallaxValue = 1;
	float newX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		newX = (-1) * (controller.transform.position.x / parallaxValue);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}
}
