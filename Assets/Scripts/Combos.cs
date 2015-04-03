using UnityEngine;
using System.Collections;

public class Combos : MonoBehaviour {

	GameObject player;
	CombatControllerIII combat;

	public int specialAttackValue = 0;
	public bool playedCombo = false;

	public int intervals;
	public float Damage;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		combat = player.GetComponent<CombatControllerIII>();
	}

	public void ComboCheck () 
	{
		//Will revamp to get note values like "19 17 15 17 19 19 19 19" && majorKey to make it simpler 
		if (specialAttackValue == 0 && combat.songValue.Equals ("6545666") && combat.R == 8 && (combat.majorKey)) {  //Mary Had a Little Lamb 1 in the Key of C Major  
			playedCombo = true;			// Cannot happen inside TimingInfo, must be set before for the timing script to pick up the values.
			TimingInfo (7, 10f);		// Passes this song's specific info out to be modified by the timing script.
			combat.FireMd ();			// Fires the projectile. TODO: Push the damage value into it as parameter.
			specialAttackValue++;		// Keeps track of what part of the combo can be played.
		}
		if (specialAttackValue == 1 && combat.songValue.Equals ("6545666" + "555")) {  //Mary Had a Little Lamb 2  
			playedCombo = true;
			TimingInfo (4, 10f);
			combat.FireMd ();
			specialAttackValue++;
		}
		if (specialAttackValue == 2 && combat.songValue.Equals ("6545666" + "555" + "688")) {  //Mary Had a Little Lamb 3  
			playedCombo = true;
			TimingInfo (4, 10f);
			combat.FireMd ();
			specialAttackValue++;
		}
		if (specialAttackValue == 3 && combat.songValue.Equals ("6545666" + "555" + "688" + "6545666655654")) {  //Mary Had a Little Lamb 4  
			playedCombo = true;
			TimingInfo (14, 20f);
			combat.FireMd ();
			specialAttackValue = 0;		// Reset because song is complete.
			combat.songValue = "";		// Reset this too, same reason.
		}
		
	}

	// For passing a combo's timing info into the ComboTiming script.
	void TimingInfo(int comboIntervals, float baseDamage)
	{
		intervals = comboIntervals;
		Damage = baseDamage;
	}
}
