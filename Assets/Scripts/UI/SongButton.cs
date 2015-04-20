using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SongButton : MonoBehaviour {

	GameObject player;					// Ref to player
	SongsOBJ songs;					// Ref to scale script
	
	public Button songButton;			// Ref to this button
	public string name;					// The name of the scale this button activates.
	SongsOBJ.Combo songToView;
	
	AudioClip clickSound;
	
	public static string nameOfCurrentSong;
	
	// Use this for initialization
	void Awake ()
	{
		songButton = this.GetComponent<Button>();				// Init this button ref
		player = GameObject.FindGameObjectWithTag ("Player"); 	// Init player references
		songs = player.GetComponent<SongsOBJ>();				// Init scale script ref
		
		clickSound = Resources.Load ("Sounds/Generic/ClickBeep") as AudioClip;
	}
	
	
	void Start () 
	{
		for (int i = 0; i < songs.comboList.Count; i++)	// For every scale in list
		{ 
			if (songs.comboList[i].name.Equals(name))	// If a combo in the list has a name that is the same as this button's name variable
				songToView = songs.comboList[i];
		}	
		
		
		songButton.onClick.AddListener(() => 				// Adds an event to the button
		{ 
			nameOfCurrentSong = songToView.name;
			// Add functions here to show various info about the chosen instrument		
		});
		
		
		
	}
	
	public void Click()
	{
		AudioSource.PlayClipAtPoint (clickSound, player.transform.position);
	}

}