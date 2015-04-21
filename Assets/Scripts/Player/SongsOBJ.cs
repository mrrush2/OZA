using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongsOBJ : MonoBehaviour 
{

	
	public List<Combo> comboList = new List<Combo>();		// Holds all songs unlocked by the player.
	public List<Combo> significantComboList = new List<Combo>();		// Holds all songs which get a button.
	public int specialAttackValue = 0;

	// Defines Combo object.
	public class Combo 
	{
		public string name;
		public ScalesOBJ.Scale scale;
		public int indexOfFullSong;			// The new "specialAttackValue." In multi-part songs, this number indicates which piece it is.
		public string songValue;			// The notes of a song as input by the numpad.
		public int intervals;				// The number of "beats" in any one song, in terms of the time between the first two notes of it.
		public float damage;				// The damage of any combo attack
		public string effect;				// The ID of the effect
		public bool finalPart;				// Controls whether there are more parts in any specific combo

		// Constructor for Combos.
		public Combo(string name, ScalesOBJ.Scale scale, int indexOfFullSong, string songValue, int intervals, float damage, string effect, bool finalPart)
		{
			this.name = name;
			this.scale = scale;
			this.indexOfFullSong = indexOfFullSong;
			this.songValue = songValue;
			this.intervals = intervals;
			this.damage = damage;
			this.effect = effect;
			this.finalPart = finalPart;
		}
		

	}



	////// COMBO DEFINITIONS //////
	/// ("Song Name", "scale", "Part of Song", "Notes to Play it", "# of Notes Including Rests", "Damage", "Effect", "If End of Song")
	/// C Major
	public Combo MHLL1 = new Combo ("Mary Had a Little Lamb", ScalesOBJ.cMajor, 0, "6545666", 7, 4f, "Arpegginade", false);			// Mary Had a Little Lamb
	public Combo MHLL2 = new Combo ("Mary Had a Little Lamb", ScalesOBJ.cMajor, 1, "6545666"+"555", 4, 10f, "Med", false);
	public Combo MHLL3 = new Combo ("Mary Had a Little Lamb", ScalesOBJ.cMajor, 2, "6545666"+"555"+"688", 4, 10f, "Med", false);
	public Combo MHLL4 = new Combo ("Mary Had a Little Lamb", ScalesOBJ.cMajor, 3, "6545666"+"555"+"688"+"6545666655654", 14, 20f, "SlowMed", true);
	
	public Combo Scale = new Combo("C Major Scale", ScalesOBJ.cMinor, 0, "123456789", 9, 10f, "Med", true);


	// For debug purposes, initialize with songs in the list.
	void Awake()
	{
	//	comboList.Add (MHLL1);
	//	comboList.Add (MHLL2);
	//	comboList.Add (MHLL3);
	//	comboList.Add (MHLL4);
	}
	
	
	public string[] getNotesArray(Combo combo)
	{
		//	E	F	F#	G	Ab	A	Bb	B	C	C#	D	Eb
		//	0	1	2	3	4	5	6	7	8	9	10	11
		char[] rawNotes = combo.songValue.ToCharArray();
		string[] notesArray = new string[combo.songValue.Length];
		for (int i = 0; i < notesArray.Length; i ++)
		{
			notesArray[i] = rawNotes[i].ToString();			// Copies the base values of the notes to the string array.
			
			//int correctionFactor = 0;	// Accounts for the intervals of major and minor keys.
			// Base corrections for major keys
			// The weird spaces at the end of the corrections stop corrections from being overwritten, but still allow Parse to work later. It's a hack.
			if (combo.scale.major)
			{
				if (notesArray[i].Equals("1")) notesArray[i] = "0 ";
				if (notesArray[i].Equals("2")) notesArray[i] = "2 ";
				if (notesArray[i].Equals("3")) notesArray[i] = "4 ";
				if (notesArray[i].Equals("4")) notesArray[i] = "5 ";
				if (notesArray[i].Equals("5")) notesArray[i] = "7 ";
				if (notesArray[i].Equals("6")) notesArray[i] = "9 ";
				if (notesArray[i].Equals("7")) notesArray[i] = "11 ";
				if (notesArray[i].Equals("8")) notesArray[i] = "12 ";
				if (notesArray[i].Equals("9")) notesArray[i] = "14 ";
			}
			// Base corrections for minor keys
			else
			{
				if (notesArray[i].Equals("1")) notesArray[i] = "0 ";
				if (notesArray[i].Equals("2")) notesArray[i] = "2 ";
				if (notesArray[i].Equals("3")) notesArray[i] = "3 ";
				if (notesArray[i].Equals("4")) notesArray[i] = "5 ";
				if (notesArray[i].Equals("5")) notesArray[i] = "7 ";
				if (notesArray[i].Equals("6")) notesArray[i] = "8 ";
				if (notesArray[i].Equals("7")) notesArray[i] = "10 ";
				if (notesArray[i].Equals("8")) notesArray[i] = "12 ";
				if (notesArray[i].Equals("9")) notesArray[i] = "14 ";
			}
			
			
			notesArray[i] = ((int.Parse(notesArray[i]) + combo.scale.root) % 12).ToString();	// Maths it to translateable offsets
			
			
			if (notesArray[i].Equals("0")) notesArray[i] = "E";			// The follwing 12 statements translate the array to the actual notes.
			if (notesArray[i].Equals("1")) notesArray[i] = "F";
			if (notesArray[i].Equals("2")) notesArray[i] = "F♯";
			if (notesArray[i].Equals("3")) notesArray[i] = "G";
			if (notesArray[i].Equals("4")) notesArray[i] = "A♭";
			if (notesArray[i].Equals("5")) notesArray[i] = "A";
			if (notesArray[i].Equals("6")) notesArray[i] = "B♭";
			if (notesArray[i].Equals("7")) notesArray[i] = "B";
			if (notesArray[i].Equals("8")) notesArray[i] = "C";
			if (notesArray[i].Equals("9")) notesArray[i] = "C♯";
			if (notesArray[i].Equals("10")) notesArray[i] = "D";
			if (notesArray[i].Equals("11")) notesArray[i] = "E♭";
		}
		
		return notesArray;
	}
	public string[] getNotesOfScale(int root, bool major)
	{
		string[] notesArray = {"1", "2", "3", "4", "5", "6", "7", "8", "9"};
		ScalesOBJ.Scale scaleToTest = new ScalesOBJ.Scale("Scale", root, major, "This scale should not exist");
		Combo scaleNotes = new Combo("Scale", scaleToTest, 0, "123456789", 9, 9001f, "(╯°□°)╯︵ ┻━┻", true);
		return getNotesArray(scaleNotes);
	}


}