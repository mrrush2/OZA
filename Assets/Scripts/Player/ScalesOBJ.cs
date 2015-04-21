using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScalesOBJ : MonoBehaviour 
{
	GameObject player;									// Ref to player
	CombatControllerIII combat;							// Ref to combat script
	public List<Scale> scaleList = new List<Scale>();	// The list of all scales unlocked
	
	public bool currentKeyMajor = true;
	public int currentRoot = 8;
	public Scale currentScale;

	void Awake ()
	{
		currentScale = cMajor; // Init so buttons know what color to be.
						
		player = GameObject.FindGameObjectWithTag ("Player"); //Initialize player references.
		combat = player.GetComponent<CombatControllerIII>();
		// DEBUG: Adds cMajor and Minor to the list, for testing purposes.
		scaleList.Add (cMajor);
		//scaleList.Add (cMinor);
		
	}

	public class Scale
	{
		public string name;		// Instance vars
		public int root;		
		public bool major;
		public string description;

		// Constructor for scales
		public Scale(string name, int root, bool major, string description)
		{
			this.name = name;
			this.root = root;
			this.major = major;
			this.description = description;
		}

	}


	////// COMBO DEFINITIONS //////
	public static Scale cMajor = new Scale ("C Major", 8, true, "This is a C Major scale. Puns go here.");
	public static Scale cMinor = new Scale ("C Minor", 8, false, "C minor needs a description, too.");
	


	// The change key function
	public void ChangeKey(Scale scale)
	{
		if (scale.major)
		{
			combat.ChangeKeyMajor (scale.root);
			currentScale = scale;
			currentKeyMajor = true;
			currentRoot = scale.root;
		}
		else
		{
			combat.ChangeKeyMinor (scale.root);
			currentScale = scale;
			currentKeyMajor = false;
			currentRoot = scale.root;
		}
	}


}
