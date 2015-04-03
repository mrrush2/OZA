using UnityEngine;
using System.Collections;

public class Combos : MonoBehaviour {

	GameObject player;
	CombatControllerIII combat;
	SongsOBJ songs;

	public int specialAttackValue = 0;
	public bool playedCombo = false;

	public int intervals;	// The number of "beats" in any one song, in terms of the time between the first two notes of it.
	public float Damage;	// The damage of any combo attack

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");	// Finds player
		combat = player.GetComponent<CombatControllerIII>();	// Ref to combat script
		songs = player.GetComponent<SongsOBJ> ();				// Ref to songs script
	}

	// For passing a combo's timing info into the ComboTiming script.
	void TimingInfo(int comboIntervals, float baseDamage)
	{
		intervals = comboIntervals;
		Damage = baseDamage;
	}
		
	public void ComboCheckII () //Electric Boogaloo
	{
		for (int i = 0; i < songs.comboList.Count; i++)
		{
			// If you play a song and it is the correct part of the combo.
			if ((combat.songValue == songs.comboList[i].songValue) && (songs.specialAttackValue == songs.comboList[i].indexOfFullSong))
			{
				playedCombo = true;			// Cannot happen inside TimingInfo, must be set before for the timing script to pick up the values.
				TimingInfo (songs.comboList[i].intervals, songs.comboList[i].damage);	// Sends out the timing info for this combo.
				combat.FireMd ();			// Will be generalized later to send out ANY effect.
				if (songs.comboList[i].finalPart)	// Last part?
				{
					songs.specialAttackValue = 0;		// Reset because song is complete.
					combat.songValue = "";					// Ditto.
				}
				else 	// Not last part, increment special.			
				{
					songs.specialAttackValue++;
				}
			}
		}
		// This reset code exists literally everywhere, which really gets confusing. But it works!
		if (Input.GetKeyDown (KeyCode.Keypad0) || Input.GetKeyDown (combat.keyReset))
			songs.specialAttackValue = 0;
	}
}
