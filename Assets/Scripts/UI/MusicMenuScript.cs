using UnityEngine;
using System.Collections;

public class MusicMenuScript : MonoBehaviour {

	public Canvas mainCanvas;				//All of the different menu sections are divided based on their canvas.
	public Canvas musicMenuCanvas;
	public Canvas instrumentsMenuCanvas;
	public Canvas scalesMenuCanvas;
	public Canvas songsMenuCanvas;
	public Transform menuObject;			//The item that opens the menu
	public LayerMask menuOpener;			//Player, basically
	public float openRadius = 0.25f;		//How close the player must be

	// Control variables
	bool menuCanBeOpened = false;
	bool menuCurrentlyOpen = false;

	// Sounds
	AudioClip clickSound;
	AudioClip menuOpenSound;
	AudioClip menuCloseSound;

	// Visual
	Animator anim;
	public Light standLight;

	// Use this for initialization
	void Start() {
		clickSound = Resources.Load ("Sounds/Generic/ClickBeep") as AudioClip;
		menuOpenSound = Resources.Load ("Sounds/Generic/Open") as AudioClip;
		menuCloseSound = Resources.Load ("Sounds/Generic/Close") as AudioClip;
		
				
		anim = GetComponent < Animator > ();
		KillAllMenus();
	}
	
	// Update is called once per frame
	void Update () {
		menuCanBeOpened = Physics2D.OverlapCircle(menuObject.position, openRadius, menuOpener); //When in range, menu can be opened
		standLight.enabled = menuCanBeOpened;
		anim.SetBool("Animating", menuCanBeOpened); // Animate the menu object if the menu can be opened
		if (!menuCurrentlyOpen && menuCanBeOpened && Input.GetKeyDown(KeyCode.E)) 	// Opening menu
		{
			KillAllMenus();	//Ensures a fresh menu state
			mainCanvas.enabled = true;
			musicMenuCanvas.enabled = true;
			MenuOpenSound();
			menuCurrentlyOpen = true;
		}
		if (menuCurrentlyOpen && Input.GetKeyDown (KeyCode.Escape)) 	//Closing menu
		{
			MenuCloseSound();
			KillAllMenus();
		}

	
	}
	public void KillAllMenus() {
		mainCanvas.enabled = false;
		musicMenuCanvas.enabled = false;
		instrumentsMenuCanvas.enabled = false;
		scalesMenuCanvas.enabled = false;
		songsMenuCanvas.enabled = false;
		menuCurrentlyOpen = false;
	}
	// Unity's UI tools require public void functions if you want to call them through their system.
	// All menu sounds are functions below.
	// This is an easy way to call sounds through the UI toolset.
	public void ClickSound() {
		if (menuCurrentlyOpen)
			AudioSource.PlayClipAtPoint (clickSound, menuObject.position);
	}
	public void MenuOpenSound() {
		if (!menuCurrentlyOpen)
			AudioSource.PlayClipAtPoint (menuOpenSound, menuObject.position);
	}
	public void MenuCloseSound() {
		if (menuCurrentlyOpen)
			AudioSource.PlayClipAtPoint (menuCloseSound, menuObject.position);
	}


}
