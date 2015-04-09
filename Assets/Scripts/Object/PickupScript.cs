using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupScript : MonoBehaviour {
	
	GameObject player;
	InstrumentsOBJ instruments;
	ScalesOBJ scales;
	SongsOBJ songs;

	public bool unlockInst = false;
	public bool unlockScale = false;
	public bool unlockSong = false;
	
	public bool DEBUG = false;
	
	public string[] instrumentUnlocks;
	public string[] scaleUnlocks;
	public string[] songUnlocks;
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		instruments = player.GetComponent<InstrumentsOBJ> ();
		scales = player.GetComponent<ScalesOBJ> ();
		songs = player.GetComponent<SongsOBJ> ();
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Unlock();
			Destroy (this.gameObject);
		}
	}
	
	void Unlock()
	{

		if (DEBUG)
			instruments.instrumentList.Add (instruments.trumpet);


		if (unlockInst)
		{
			foreach (InstrumentsOBJ.Instrument i in instruments.instrumentList)
			{
				foreach (string a in instrumentUnlocks)
				{
					if (i.name.Equals (a))
						instruments.instrumentList.Add (i);
				}
			}
		}
		if (unlockScale)
		{
			foreach (ScalesOBJ.Scale i in scales.scaleList)
			{
				foreach (string a in scaleUnlocks)
				{
					if (i.name.Equals (a))
						scales.scaleList.Add (i);
				}
			}
		}
		if (unlockSong)
		{
			foreach (SongsOBJ.Combo i in songs.comboList)
			{
				foreach (string a in songUnlocks)
				{
					if (i.name.Equals (a))
						songs.comboList.Add (i);
				}
			}
		}
	}

	
}
