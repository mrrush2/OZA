using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour 
{
	GameObject player;
	SongsOBJ songs;
	GameObject scaleController;
	Text[] noteTextBoxes;



	public Text description;


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		songs = player.GetComponent<SongsOBJ>();
		scaleController = this.gameObject;
		noteTextBoxes = scaleController.GetComponentsInChildren<Text>();
	}


	public void SetInfo(ScalesOBJ.Scale newScale)
	{
		string newDesc = newScale.description;
		
		description.text = newDesc;
		SetAllScaleNotes(newScale);
	}
	
	public void SetAllScaleNotes(ScalesOBJ.Scale newScale)
	{
		string[] notes = songs.getNotesOfScale(newScale.root, newScale.major);
		for (int i = 0; i < notes.Length; i ++)
		{
			noteTextBoxes[i].text = notes[i]; 
		}
		
	}
}
