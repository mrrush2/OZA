using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupScript : MonoBehaviour {
	
	GameObject player;
	InstrumentsOBJ instruments;
	ScalesOBJ scales;
	SongsOBJ songs;
	
	public bool trumpet = false;
	
	public bool cMinor = false;
	
	public bool MHLL = false;
	
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
		if (trumpet)
			instruments.instrumentList.Add (instruments.trumpet);
			
		if (cMinor)
			scales.scaleList.Add (scales.cMinor);
			
		if (MHLL)
		{
			songs.comboList.Add (songs.MHLL1);
			songs.comboList.Add (songs.MHLL2);
			songs.comboList.Add (songs.MHLL3);
			songs.comboList.Add (songs.MHLL4);
		}
	}

	
}
