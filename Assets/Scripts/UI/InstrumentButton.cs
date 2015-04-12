using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstrumentButton : MonoBehaviour 
{
	GameObject player;
	InstrumentsOBJ instruments;

	public Button instrumentButton;
	public string name;
	InstrumentsOBJ.Instrument instrumentChosen;
	CombatControllerIII combat;
	
	ColorBlock buttonColors;
	
	AudioClip clickSound;
	
	// Useful color definitions.
	Color transparent = new Color(0f, 0f, 0f, 0f);
	Color mouseover = new Color(0.7f, 0.7f, 0.7f, 1f);
	Color selected = new Color(0f, 0.7f, 0f, 1f);

	void Awake ()
	{
		instrumentButton = this.GetComponent<Button> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		instruments = player.GetComponent<InstrumentsOBJ> ();
		combat = player.GetComponent<CombatControllerIII> ();
		
		clickSound = Resources.Load ("Sounds/Generic/ClickBeep") as AudioClip;
	}

	void Start ()
	{

		buttonColors = instrumentButton.colors; // Definition of colors var for setting
				
		for (int i = 0; i < instruments.instrumentList.Count; i++) // Figures out what instrument this button activates.
		{
			if (instruments.instrumentList[i].name.Equals (name))
				instrumentChosen = instruments.instrumentList[i];
		}
		
		instrumentButton.onClick.AddListener(() => 				// Adds an event to the button
		{ 
			instruments.ChangeInstrument(instrumentChosen);	
		});
	}

	
	void Update ()
	{
		if (instrumentButton.name.Equals(combat.instrument))	// When selected, this button is green
		{
			buttonColors.normalColor = selected;
			buttonColors.highlightedColor = selected;
			instrumentButton.colors = buttonColors; // To update to the new values
		}
		else 											// When unselected, be normal			
		{
			buttonColors.normalColor = transparent;
			buttonColors.highlightedColor = mouseover;
			instrumentButton.colors = buttonColors; // To update to the new values
		}		
	}
	
	
	public void Click()
	{
		AudioSource.PlayClipAtPoint (clickSound, player.transform.position);
	}
}
