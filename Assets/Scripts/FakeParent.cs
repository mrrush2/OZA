using UnityEngine;
using System.Collections;

public class FakeParent : MonoBehaviour {

	// This script will "parent" this object to the target object.
	public bool followX,
				followY,
				followZ;

	public float offsetX,
				 offsetY,
				 offsetZ;


	public GameObject other;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called constantly
	void Update () {


		if (followX) 
		{
			transform.position = new Vector3(other.transform.position.x + offsetX, this.transform.position.y, this.transform.position.z);
		}
		if (followY) 
		{
			transform.position = new Vector3(this.transform.position.x, other.transform.position.y + offsetY, this.transform.position.z);
		}
		if (followZ) 
		{
			transform.position = new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z + offsetZ);
		}
	}
}
