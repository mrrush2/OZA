using UnityEngine;
using System.Collections;

public class DEBUGViewer : MonoBehaviour 
{
	GameObject player;
	SongsOBJ songs;
	
	
	
	public string songCurrentlySelected;
	public string scaleCurrentlySelected;
	public string instCurrentlySelected;
	public string[] songValueTest;
	
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		songs = player.GetComponent<SongsOBJ>();
	}
	
	void FixedUpdate ()
	{
		songCurrentlySelected = SongButton.nameOfCurrentSong;
		scaleCurrentlySelected = ScaleButton.nameOfCurrentScale;
		instCurrentlySelected = InstrumentButton.nameOfCurrentInst;
		songValueTest = songs.getNotesArray(songs.MHLL4);
	}
	
}
