using UnityEngine;
using System.Collections;

public class ComboTiming : MonoBehaviour {

	GameObject player;						// A reference to the player object.
	CombatControllerIII combatController;	// A reference to the combat controller script on the player.

	public float baseNoteInterval = 0.25f; //15 frames = a quarter second.

	public float firstNoteTime;		// Time when the first note of a combo is played.							ONLY PUBLIC FOR DEBUG
	public float secondNoteTime;		// Time when the second note of a combo is played.						ONLY PUBLIC FOR DEBUG
	public float noteInterval;		// Time between the first and second notes, as a baseline for "beats."		ONLY PUBLIC FOR DEBUG
	float songTime;			// Time it takes to play an entire song.
	float predictedSongTime;	// Time the script expects a song to take to play based on the note interval
								// that the player specifies with their first two notes, and the "beats"
								// defined by the song.
	float baseTime;			// The "par" time of a song. Based on some standard note Interval and the
								// total beats in a combo, defined by the combo.
	float shittynessConstant;	// How far away songTime is from predictedSongTime.
	float speedBonus;			// How far away songTime is from baseTime (par.)

	private bool firstSet = false;
	private bool secondSet = false;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		combatController = player.GetComponent<CombatControllerIII>();
	}
	
	// Update is called constantly
	void Update () {

		// Simple reset.
		if (combatController.songValue.Length == 0) 
		{
			firstSet = false;
			secondSet = false;
			firstNoteTime = 0;
			secondNoteTime = 0;
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
						

/*					// Requires new variables in the soon-to-exist ComboCheck class.

		if (a combo is completed)
		{
			songTime = Time.time - firstNoteTime;
			predictedSongTime = noteInterval * songPlayed.intervals;
			shittynessConstant = Mathf.Abs(predictedSongTime - songTime);
								
								
			baseTime = songPlayed.intervals * baseNoteInterval;
			speedBonus = baseTime - songTime;
			if (speedBonus < 0)
				speedBonus = 0;
										
			dvalue = songPlayed.baseDamage - shittynessConstant + speedBonus;
		}
*/

	}
}
