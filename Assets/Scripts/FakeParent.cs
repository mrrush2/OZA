using UnityEngine;
using System.Collections;

public class FakeParent : MonoBehaviour {

	// This script will "parent" this object to the target object.
	public GameObject other;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called constantly
	void Update () {
		transform.position = other.transform.position;
	}
}
