using UnityEngine;
using System.Collections;

public class ComboTiming : MonoBehaviour {

	GameObject player;						// A reference to the player object.
	CombatControllerIII combatController;	// A reference to the combat controller script on the player.
	Combos comboScript;

	public float baseNoteInterval = 0.5f; // for the "par time"

	public float firstNoteTime;			// Time when the first note of a combo is played.								ONLY PUBLIC FOR DEBUG
	public float secondNoteTime;		// Time when the second note of a combo is played.								ONLY PUBLIC FOR DEBUG
	public float noteInterval;			// Time between the first and second notes, as a baseline for "beats."			ONLY PUBLIC FOR DEBUG
	public float songTime;				// Time it takes to play an entire song.										YOU GET THE IDEA
	public float songTimeAccounter = 0;	// Accounts for previous song time.
	public float predictedSongTime;		// Time the script expects a song to take to play based on the note interval
										// that the player specifies with their first two notes, and the "beats"
										// defined by the song.
	public float baseTime;					// The "par" time of a song. Based on some standard note Interval and the
										// total beats in a combo, defined by the combo.
	public float shittynessConstant;		// How far away songTime is from predictedSongTime.
	public float speedBonus;				// How far away songTime is from baseTime (par.)


	private bool firstSet = false;
	private bool secondSet = false;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");			// Finds the player
		combatController = player.GetComponent<CombatControllerIII>();	// Ref to combat script
		comboScript = player.GetComponent<Combos>();					// Ref to combo script
	}
	
	// Update is called constantly
	void Update () {

		// Manual Reset
		if (Input.GetKeyDown (KeyCode.Keypad0) || Input.GetKeyDown (combatController.keyReset)) 
		{
			Reset ();
		}


		// Sets the time of the first note played.
		if (combatController.songValue.Length == 1 && !firstSet)
		{
			firstNoteTime = Time.time;
			firstSet = true;
		}
		// Sets the time of the second note played.
		if (combatController.songValue.Length >= 2 && !secondSet)
		{
			secondNoteTime = Time.time;
			secondSet = true;
			// Sets the note interval based on the two times.
			noteInterval = secondNoteTime - firstNoteTime;
		}
	}

	//Separate method to calculate the new damage, which is passed into attacks.
	public float CalculateNewDamage()
	{
		float newDamage;
		
		comboScript.playedCombo = false; // Resets as it goes so it can be called again afterwards.
		
		songTimeAccounter = songTime;									// Makes sure ever part of a multi-part combo has a time that concerns only itself, not the full combo.
		songTime = Time.time - firstNoteTime - songTimeAccounter;		// Actually does the math for that ^^^
		predictedSongTime = noteInterval * comboScript.intervals;		// Comes up with how fast you would finish the song if you played perfectly in time.
		shittynessConstant = Mathf.Abs(predictedSongTime - songTime);	// Tells you how much you suck at being in time.
		
		
		baseTime = comboScript.intervals * baseNoteInterval;			// Comes up with the par time for a song based on its intervals (beats.)
		speedBonus = baseTime - songTime;								// Calculates your speed bonus
		if (speedBonus < 0)
			speedBonus = 0;												// No negative bonuses! I am a merciful god.
		
		newDamage = comboScript.Damage - shittynessConstant + speedBonus;	// Calculates the new damage after all that silly math.
		
		
		// Auto Resets, only after the final piece of a combo.
		if (combatController.songValue.Length == 0) 
		{
			Reset ();
		}	
		return newDamage;	
	}



	void Reset()
	{
		firstSet = false;		// So we can get a new time for the next combo.
		secondSet = false;		// Ditto
		firstNoteTime = 0;		// Dittoo
		secondNoteTime = 0;		// Dittooo
		songTime = 0;			// Makes sure to reset the time it actually took to play the song so the Accounter grabs the CORRECT value.
		songTimeAccounter = 0;	// Probably not necessary, but I'm paranoid.
	}
}
