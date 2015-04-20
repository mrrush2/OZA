using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleButton : MonoBehaviour {


	GameObject player;					// Ref to player
	ScalesOBJ scales;					// Ref to scale script
	ScalesOBJ.Scale scaleToActivate;	// Which scale this button activates
	
	public Button scaleButton;			// Ref to this button
	public string name;					// The name of the scale this button activates.
	
	
	ColorBlock buttonColors;	// Temp var for changing colors
	
	AudioClip clickSound;
	
	public static string nameOfCurrentScale;
	
	// Useful color definitions.
	Color transparent = new Color(0f, 0f, 0f, 0f);
	Color mouseover = new Color(0.7f, 0.7f, 0.7f, 1f);
	Color selected = new Color(0f, 0.7f, 0f, 1f);
	
	// Use this for initialization
	void Awake ()
	{
		scaleButton = this.GetComponent<Button>();				// Init this button ref
		player = GameObject.FindGameObjectWithTag ("Player"); 	// Init player references
		scales = player.GetComponent<ScalesOBJ>();				// Init scale script ref
		
		clickSound = Resources.Load ("Sounds/Generic/ClickBeep") as AudioClip;
	}
	
	
	void Start () 
	{
		
		buttonColors = scaleButton.colors; // Definition of colors var for setting
		
		for (int i = 0; i < scales.scaleList.Count; i++)	// For every scale in list
		{ 
			if (scales.scaleList[i].name.Equals(name))		// If it's the right scale by name
				scaleToActivate = scales.scaleList[i];		// Set it
		}	
	
	
		scaleButton.onClick.AddListener(() => 				// Adds an event to the button
		{ 
			scales.ChangeKey (scaleToActivate);				// Changes the key
			nameOfCurrentScale = scaleToActivate.name;
		});
		

		
	}
	
	void Update ()
	{
		if (scaleButton.name.Equals(scales.currentScale.name))	// When selected, this button is green
		{
			buttonColors.normalColor = selected;
			buttonColors.highlightedColor = selected;
			scaleButton.colors = buttonColors; // To update to the new values
		}
		else 											// When unselected, be normal			
		{
			buttonColors.normalColor = transparent;
			buttonColors.highlightedColor = mouseover;
			scaleButton.colors = buttonColors; // To update to the new values
		}		
	}

	public void Click()
	{
		AudioSource.PlayClipAtPoint (clickSound, player.transform.position);
	}
}
