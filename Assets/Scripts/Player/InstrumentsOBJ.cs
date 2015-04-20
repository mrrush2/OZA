using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstrumentsOBJ : MonoBehaviour 
{
	GameObject player;
	CombatControllerIII combat;
	ScalesOBJ scales;
	ReSkinPlayer skin;
	public List<Instrument> instrumentList = new List<Instrument>();

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player"); //Initialize player references.
		combat = player.GetComponent<CombatControllerIII>();
		scales = player.GetComponent<ScalesOBJ>();	// Need this for setting instrument by getting the current scale info.
		skin = player.GetComponent<ReSkinPlayer>();

		//DEBUG adds instruments to test
		instrumentList.Add (violin);
		//instrumentList.Add (trumpet);
		
		// All instruments in the game must be added to this list.
	}

	public class Instrument
	{
		public string name;
		public string spritesheetName;
		public string perk1,
					  perk2,
					  perk3;
		public string description;

		//Constructors
		public Instrument(string name, string perk1, string perk2, string perk3, string description)
		{
			this.name = name;
			this.spritesheetName = name;
			this.perk1 = perk1;
			this.perk2 = perk2;
			this.perk3 = perk3;
			this.description = description;
		}
		public Instrument(string name, string spritesheetName, string perk1, string perk2, string perk3, string description)
		{
			this.name = name;
			this.spritesheetName = spritesheetName;
			this.perk1 = perk1;
			this.perk2 = perk2;
			this.perk3 = perk3;
			this.description = description;
		}
	}


	////// INSTRUMENT DEFINITIONS //////
	public Instrument violin = new Instrument ("Violin", "testPerk1", "testPerk2", "testPerk3", "It's a violin.");
	public Instrument trumpet = new Instrument ("Trumpet", "testPerk4", "testPerk5", "testperk6", "Toot toot.");
	
	


	public void ChangeInstrument(Instrument instrument)
	{
		combat.instrument = instrument.name;
		skin.instrument = instrument.spritesheetName;
		if (scales.currentKeyMajor) combat.ChangeKeyMajor(scales.currentRoot);
		else combat.ChangeKeyMinor(scales.currentRoot);
	}
}
