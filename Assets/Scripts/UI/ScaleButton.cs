using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleButton : MonoBehaviour {


	GameObject player;					// Ref to player
	ScalesOBJ scales;					// Ref to scale script
	ScalesOBJ.Scale scaleToActivate;	// Which scale this button activates
	
	public Button scaleButton;			// Ref to this button
	public string name;					// The name of the scale this button activates.
	
	// Use this for initialization
	void Awake ()
	{
		scaleButton = this.GetComponent<Button>();				// Init this button ref
		player = GameObject.FindGameObjectWithTag ("Player"); 	// Init player references
		scales = player.GetComponent<ScalesOBJ>();				// Init scale script ref
	}
	
	
	void Start () 
	{
		for (int i = 0; i < scales.scaleList.Count; i++)	// For every scale in list
		{ 
			if (scales.scaleList[i].name.Equals(name))		// If it's the right scale by name
				scaleToActivate = scales.scaleList[i];		// Set it
		}	
	
	
		scaleButton.onClick.AddListener(() => 				// Adds an event to the button
		{ 
			scales.ChangeKey (scaleToActivate);				// Changes the k
		});
		

	}

}
