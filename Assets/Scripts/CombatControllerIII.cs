using UnityEngine;
using System.Collections;

public class CombatControllerIII : MonoBehaviour {

	GameObject player; //Player refernce.
	CharacterKontroller playerScript;

	public string[] noteRep = {"1", "2", "3", "4", "5", "6", "7", "8", "9"}; 

	// Keybindings.
	public KeyCode key1 = KeyCode.B,
	key2 = KeyCode.N,
	key3 = KeyCode.M,
	key4 = KeyCode.G,
	key5 = KeyCode.H,
	key6 = KeyCode.J,
	key7 = KeyCode.T,
	key8= KeyCode.Y,
	keyReset = KeyCode.V;

	// All of the sounds of the currently active scale are defined here
	public AudioClip noteFirstS;
	public AudioClip noteSecondS;
	public AudioClip noteThirdS;
	public AudioClip noteFourthS;
	public AudioClip noteFifthS;
	public AudioClip noteSixthS;
	public AudioClip noteSeventhS;
	public AudioClip noteEighthS;
	public AudioClip noteNinthS;
	// public AudioClip[] noteSound = new AudioClip[9];

	public bool ninthUnlocked = false; //Has the player unlocked the use of nine notes?

	public string songValue = ""; //The variable that tracks the current value of your song.

	bool canPlay = true; //Checks if you are currently allowed to play.

// All variables used to fire physical notes.
	public Rigidbody2D musicNoteSm;
	public Rigidbody2D musicNoteMd;
	Rigidbody2D noteSmInstance;
	Rigidbody2D noteMdInstance;
	public Transform noteOrigin;
	public Transform heWhoShoots;
	float specialAttackValue = 0; //On a per-special basis, this controls how many times a combo runs when activated.	
	// For visual manipulation
	
	public Animator anim;
	public Light auraLight;

	int R = 8; 
	public bool majorKey = true; 
	public void ChangeKeyMajor(int S)
	{
		majorKey = true; 
		songValue = "";
		noteFirstS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2; 
		noteSecondS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteThirdS = Resources.Load ("V" + S) as AudioClip;
		S = S + 1;
		noteFourthS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteFifthS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteSixthS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteSeventhS = Resources.Load ("V" + S) as AudioClip;
		S = S + 1;
		noteEighthS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteNinthS = Resources.Load ("V" + S) as AudioClip;
	}
	
	public void ChangeKeyMinor(int S)
	{
		majorKey = false; 
		songValue = ""; 
		noteFirstS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2; 
		noteSecondS = Resources.Load ("V" + S) as AudioClip;
		S = S + 1;
		noteThirdS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteFourthS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteFifthS = Resources.Load ("V" + S) as AudioClip;
		S = S + 1;
		noteSixthS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteSeventhS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteEighthS = Resources.Load ("V" + S) as AudioClip;
		S = S + 2;
		noteNinthS = Resources.Load ("V" + S) as AudioClip;
	}




// Use this for initialization
	void Start () 
		{
			player = GameObject.FindGameObjectWithTag ("Player"); //Initialize player references.
			playerScript = player.GetComponent<CharacterKontroller>();
		}

// Update is called constantly
	void Update () {
		
		////// AURA STUFF //////
		if (songValue.Equals ("")) //If aura exists, light will shine
		{  
			auraLight.enabled = false;
			anim.SetBool ("IsPlaying", false);
		}
		else 
		{
			auraLight.enabled = true;
			anim.SetBool ("IsPlaying", true);
		}
		

		////// CAN PLAY STUFF //////		
		canPlay = !playerScript.onLadder;
		
		
		//Temporary Key change mechanism using keybindings. 
		if (Input.GetKeyDown (KeyCode.Z)) //A Major
		{
			R = 5;
			ChangeKeyMajor(R); 
		}
		if (Input.GetKeyDown (KeyCode.X)) //C Minor
		{
			R = 8; 
			ChangeKeyMinor(R); 
		}
		if (Input.GetKeyDown (KeyCode.C)) //C Major
		{
			R = 8;
			ChangeKeyMajor(R); 
		}



			////// NOTE PLAYING //////
		
		// All of the keys that can be played are below, referencing the currently active scale
		
		if (canPlay && (Input.GetKeyDown (KeyCode.Keypad1) || Input.GetKeyDown (key1)))
		{
			NotePress(1);
			AudioSource.PlayClipAtPoint(noteFirstS, noteOrigin.position);
		}
		if (canPlay && (Input.GetKeyDown (KeyCode.Keypad2) || Input.GetKeyDown (key2)))
		{
			NotePress(2);
			AudioSource.PlayClipAtPoint(noteSecondS, noteOrigin.position);
			
		}
		if (canPlay && (Input.GetKeyDown (KeyCode.Keypad3) || Input.GetKeyDown (key3)))
		{
			NotePress(3);
			AudioSource.PlayClipAtPoint(noteThirdS, noteOrigin.position);
		}
		if (canPlay && (Input.GetKeyDown (KeyCode.Keypad4) || Input.GetKeyDown (key4)))
		{
			NotePress(4);
			AudioSource.PlayClipAtPoint(noteFourthS, noteOrigin.position);
		}
		if (canPlay && (Input.GetKeyDown (KeyCode.Keypad5) || Input.GetKeyDown (key5))) 
		{
			NotePress(5);
			AudioSource.PlayClipAtPoint(noteFifthS, noteOrigin.position);
		}
		if (canPlay && (Input.GetKeyDown (KeyCode.Keypad6) || Input.GetKeyDown (key6)))
		{
			NotePress(6);
			AudioSource.PlayClipAtPoint(noteSixthS, noteOrigin.position);
		}
		if (canPlay && (Input.GetKeyDown (KeyCode.Keypad7) || Input.GetKeyDown (key7)))
		{
			NotePress(7);
			AudioSource.PlayClipAtPoint(noteSeventhS, noteOrigin.position);
		}
		if (canPlay && (Input.GetKeyDown (KeyCode.Keypad8) || Input.GetKeyDown (key8)))
		{
			NotePress(8);
			AudioSource.PlayClipAtPoint(noteEighthS, noteOrigin.position);
		}   
		if (ninthUnlocked && canPlay && Input.GetKeyDown (KeyCode.Keypad9)) //Extra note
		{
			NotePress(9);
			AudioSource.PlayClipAtPoint (noteNinthS, noteOrigin.position);
		}   
		
		
		////// RESETS //////
		
		if (Input.GetKeyDown (KeyCode.Keypad0) || Input.GetKeyDown (keyReset)) //Reset button
		{
			songValue = "";
			specialAttackValue = 0;
		}
		
		// Check for combos
		ComboCheck();
		
	}

////// PROJECTILES AND EFFECTS ////// 

	void FireSm () {	
		noteSmInstance = Instantiate(musicNoteSm, noteOrigin.position, noteOrigin.rotation) as Rigidbody2D;	
		noteSmInstance.velocity = new Vector2((heWhoShoots.localScale.x * 4), 0);	
	}

	void FireMd () {
		noteMdInstance = Instantiate(musicNoteMd, noteOrigin.position, noteOrigin.rotation) as Rigidbody2D;	
		noteMdInstance.velocity = new Vector2((heWhoShoots.localScale.x * 4), 0);	
	}

	////// NOTE METHOD //////
	// This is called every time a note button is pressed

	void NotePress (int note) 
	{	
		songValue += noteRep[--note];	
		FireSm();	
		// AudioSource.PlayClipAtPoint(noteSound[--note], noteOrigin.position);	
	}

////// COMBO STUFF //////
// Checks for combos
	void ComboCheck () {
		//Will revamp to get note values like "19 17 15 17 19 19 19 19" && majorKey to make it simpler 
		if (specialAttackValue == 0 && songValue.Equals ("6545666") && R == 8 && (majorKey)) {  //Mary Had a Little Lamb 1 in the Key of C Major  
			FireMd ();
			specialAttackValue++;
		}
		if (specialAttackValue == 1 && songValue.Equals ("6545666" + "555")) {  //Mary Had a Little Lamb 2  
			FireMd ();
			specialAttackValue++;
		}
		if (specialAttackValue == 2 && songValue.Equals ("6545666" + "555" + "688")) {  //Mary Had a Little Lamb 3  
			FireMd ();
			specialAttackValue++;
		}
		if (specialAttackValue == 3 && songValue.Equals ("6545666" + "555" + "688" + "6545666655654")) {  //Mary Had a Little Lamb 4  
			FireMd ();
			specialAttackValue = 0;
			songValue = "";
		}
		
	}

}