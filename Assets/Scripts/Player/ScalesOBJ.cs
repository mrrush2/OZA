using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScalesOBJ : MonoBehaviour 
{
	GameObject player;									// Ref to player
	CombatControllerIII combat;							// Ref to combat script
	public List<Scale> scaleList = new List<Scale>();	// The list of all scales unlocked

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player"); //Initialize player references.
		combat = player.GetComponent<CombatControllerIII>();
		// DEBUG: Adds cMajor and Minor to the list, for testing purposes.
		scaleList.Add (cMajor);
		scaleList.Add (cMinor);
		
	}

	public class Scale
	{
		public string name;		// Instance vars
		public int root;		
		public bool major;

		// Constructor for scales
		public Scale(string name, int root, bool major)
		{
			this.name = name;
			this.root = root;
			this.major = major;
		}
		

	}


	////// COMBO DEFINITIONS //////
	Scale cMajor = new Scale ("C Major", 8, true);
	Scale cMinor = new Scale ("C Minor", 8, false);
	


	// The change key function
	public void ChangeKey(Scale scale)
	{
		if (scale.major)
		{
			combat.ChangeKeyMajor (scale.root);
		}
		else
		{
			combat.ChangeKeyMinor (scale.root);
		}
	}


}
