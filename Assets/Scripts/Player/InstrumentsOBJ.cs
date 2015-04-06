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
		instrumentList.Add (trumpet);
	}

	public class Instrument
	{
		public string name;
		public string spritesheetName;

		//Constructor
		public Instrument(string name)
		{
			this.name = name;
			this.spritesheetName = name;
		}
		public Instrument(string name, string spritesheetName)
		{
			this.name = name;
			this.spritesheetName = spritesheetName;
		}
	}


	////// INSTRUMENT DEFINITIONS //////
	Instrument violin = new Instrument ("Violin");
	Instrument trumpet = new Instrument ("Trumpet");


	public void ChangeInstrument(Instrument instrument)
	{
		combat.instrument = instrument.name;
		skin.instrument = instrument.spritesheetName;
		if (scales.currentKeyMajor) combat.ChangeKeyMajor(scales.currentRoot);
		else combat.ChangeKeyMinor(scales.currentRoot);
	}
}
