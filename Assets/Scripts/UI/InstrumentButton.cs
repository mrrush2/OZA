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

	void Awake ()
	{
		instrumentButton = this.GetComponent<Button> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		instruments = player.GetComponent<InstrumentsOBJ> ();
	}

	void Start ()
	{
		for (int i = 0; i < instruments.instrumentList.Count; i++) 
		{
			if (instruments.instrumentList[i].name.Equals (name))
				instrumentChosen = instruments.instrumentList[i];
		}
		
		instrumentButton.onClick.AddListener(() => 				// Adds an event to the button
		{ 
			instruments.ChangeInstrument(instrumentChosen);	
		});
	}

}
