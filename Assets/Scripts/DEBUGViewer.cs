using UnityEngine;
using System.Collections;

public class DEBUGViewer : MonoBehaviour 
{
	GameObject player;
	SongsOBJ songs;
	
	
	// Vars to test
	public string songCurrentlySelected;
	public string scaleCurrentlySelected;
	public string instCurrentlySelected;
	public string[] songValueTest;
	public string[] getScaleTest;
	
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
		songValueTest = songs.getNotesArray(songs.Scale);
		getScaleTest = songs.getNotesOfScale(8, false);
	}
	
}
