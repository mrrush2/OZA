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
		player = GameObject.FindGameObjectWithTag ("Player");
		combatController = player.GetComponent<CombatControllerIII>();
		comboScript = player.GetComponent<Combos>();
	}
	
	// Update is called constantly
	void Update () {

		// Manual Reset
		if (Input.GetKeyDown (KeyCode.Keypad0) || Input.GetKeyDown (combatController.keyReset)) 
		{
			firstSet = false;
			secondSet = false;
			firstNoteTime = 0;
			secondNoteTime = 0;
			songTime = 0;
			songTimeAccounter = 0;
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
						

					// Requires new variables in the soon-to-exist ComboCheck class.

		if (comboScript.playedCombo)
		{
			comboScript.playedCombo = false;

			songTimeAccounter = songTime;
			songTime = Time.time - firstNoteTime - songTimeAccounter;
			predictedSongTime = noteInterval * comboScript.intervals;
			shittynessConstant = Mathf.Abs(predictedSongTime - songTime);
								
								
			baseTime = comboScript.intervals * baseNoteInterval;
			speedBonus = baseTime - songTime;
			if (speedBonus < 0)
				speedBonus = 0;
										
			comboScript.Damage = comboScript.Damage - shittynessConstant + speedBonus;

			// Auto Resets
			if (combatController.songValue.Length == 0) 
			{
				firstSet = false;
				secondSet = false;
				firstNoteTime = 0;
				secondNoteTime = 0;
				songTime = 0;
				songTimeAccounter = 0;
			}
			// Resets
			/*
			songTime = 0;
			predictedSongTime = 0;
			baseTime = 0;
			shittynessConstant = 0;
			speedBonus = 0;
			*/

		}


	}
}
