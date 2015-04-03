using UnityEngine;
using System.Collections;

public class Combos : MonoBehaviour {

	GameObject player;
	CombatControllerIII combat;

	public int specialAttackValue = 0;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		combat = player.GetComponent<CombatControllerIII>();
	}

	public void ComboCheck () 
	{
		//Will revamp to get note values like "19 17 15 17 19 19 19 19" && majorKey to make it simpler 
		if (specialAttackValue == 0 && combat.songValue.Equals ("6545666") && combat.R == 8 && (combat.majorKey)) {  //Mary Had a Little Lamb 1 in the Key of C Major  
			combat.FireMd ();
			specialAttackValue++;
		}
		if (specialAttackValue == 1 && combat.songValue.Equals ("6545666" + "555")) {  //Mary Had a Little Lamb 2  
			combat.FireMd ();
			specialAttackValue++;
		}
		if (specialAttackValue == 2 && combat.songValue.Equals ("6545666" + "555" + "688")) {  //Mary Had a Little Lamb 3  
			combat.FireMd ();
			specialAttackValue++;
		}
		if (specialAttackValue == 3 && combat.songValue.Equals ("6545666" + "555" + "688" + "6545666655654")) {  //Mary Had a Little Lamb 4  
			combat.FireMd ();
			specialAttackValue = 0;
			combat.songValue = "";
		}
		
	}
}
