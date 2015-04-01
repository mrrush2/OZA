using UnityEngine;
using System.Collections;

public class ComboTiming : MonoBehaviour {

	GameObject player;						// A reference to the player object.
	CombatControllerIII combatController;	// A reference to the combat controller script on the player.

	public int baseNoteInterval = 15; //15 frames = a quarter second.

	public int firstNoteTime;		// Time when the first note of a combo is played.
	public int secondNoteTime;		// Time when the second note of a combo is played.
	public int noteInterval;		// Time between the first and second notes, as a baseline for "beats."
	int songTime;			// Time it takes to play an entire song.
	int predictedSongTime;	// Time the script expects a song to take to play based on the note interval
								// that the player specifies with their first two notes, and the "beats"
								// defined by the song.
	int baseTime;			// The "par" time of a song. Based on some standard note Interval and the
								// total beats in a combo, defined by the combo.
	int shittynessConstant;	// How far away songTime is from predictedSongTime.
	int speedBonus;			// How far away songTime is from baseTime (par.)

	private bool firstSet = false;
	private bool secondSet = false;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		combatController = player.GetComponent<CombatControllerIII>();
	}
	
	// Update is called constantly
	void Update () {

		if (combatController.songValue.Length == 0) 
		{
			firstSet = false;
			secondSet = false;
		}

		// Sets the time of the first note played.
		if (combatController.songValue.Length == 1 && !firstSet)
		{
			firstNoteTime = Time.frameCount; // frameCount returns frames since game run.
			firstSet = true;
		}
		// Sets the time of the second note played.
		if (combatController.songValue.Length == 2 && !secondSet)
		{
			secondNoteTime = Time.frameCount;
			secondSet = true;
			// Sets the note interval based on the two times.
			noteInterval = secondNoteTime - firstNoteTime;
		}
						

/*					// Requires new variables in the soon-to-exist ComboCheck class.

		if (a combo is completed)
		{
			songTime = Time.frameCount - firstNoteTime;
			predictedSongTime = noteInterval * songPlayed.intervals;
			shittynessConstant = Mathf.Abs(predictedSongTime - songTime);
								
								
			baseTime = songPlayed.intervals * baseNoteInterval;
			speedBonus = baseTime - songTime;
			if (speedBonus < 0)
				speedBonus = 0;
										
			dvalue = baseDamage - shittynessConstant + speedBonus;
		}
*/

	}
}
