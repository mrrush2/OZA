using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongsOBJ : MonoBehaviour 
{
	public List<Combo> comboList = new List<Combo>();		// Holds all songs unlocked by the player.
	public int specialAttackValue = 0;

	// Defines Combo object.
	public class Combo 
	{
		public string name;
		public int indexOfFullSong;			// The new "specialAttackValue." In multi-part songs, this number indicates which piece it is.
		public string songValue;			// The notes of a song as input by the numpad.
		public int intervals;				// The number of "beats" in any one song, in terms of the time between the first two notes of it.
		public float damage;				// The damage of any combo attack
		public int effectID;				// The ID of the effect
		public bool finalPart;				// Controls whether there are more parts in any specific combo

		// Constructor for Combos.
		public Combo(string name, int indexOfFullSong, string songValue, int intervals, float damage, int effectID, bool finalPart)
		{
			this.name = name;
			this.indexOfFullSong = indexOfFullSong;
			this.songValue = songValue;
			this.intervals = intervals;
			this.damage = damage;
			this.effectID = effectID;
			this.finalPart = finalPart;
		}
	}



	////// COMBO DEFINITIONS //////
	public Combo MHLL1 = new Combo ("Mary Had a Little Lamb", 0, "6545666", 7, 10f, 0, false);			// Mary Had a Little Lamb
	public Combo MHLL2 = new Combo ("Mary Had a Little Lamb", 1, "6545666"+"555", 4, 10f, 0, false);
	public Combo MHLL3 = new Combo ("Mary Had a Little Lamb", 2, "6545666"+"555"+"688", 4, 10f, 0, false);
	public Combo MHLL4 = new Combo ("Mary Had a Little Lamb", 3, "6545666"+"555"+"688"+"6545666655654", 14, 20f, 0, true);


	// For debug purposes, initialize with songs in the list.
	void Awake()
	{
	//	comboList.Add (MHLL1);
	//	comboList.Add (MHLL2);
	//	comboList.Add (MHLL3);
	//	comboList.Add (MHLL4);
	}


}
