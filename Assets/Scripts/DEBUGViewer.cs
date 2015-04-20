using UnityEngine;
using System.Collections;

public class DEBUGViewer : MonoBehaviour 
{
	public string songCurrentlySelected;
	public string scaleCurrentlySelected;
	public string instCurrentlySelected;
	
	void FixedUpdate ()
	{
		songCurrentlySelected = SongButton.nameOfCurrentSong;
		scaleCurrentlySelected = ScaleButton.nameOfCurrentScale;
		instCurrentlySelected = InstrumentButton.nameOfCurrentInst;
	}
	
}
