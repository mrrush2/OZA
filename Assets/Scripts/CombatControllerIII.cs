using UnityEngine;
using System.Collections;

public class CombatControllerIII : MonoBehaviour {

	GameObject player; //Player refernce.
	CharacterKontroller playerScript;
	Combos comboScript;

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
	//public float specialAttackValue = 0; //On a per-special basis, this controls how many times a combo runs when activated.	
	// For visual manipulation
	
	public Animator anim;
	public Light auraLight;

	public int R = 8; 
	string instrument = "Violin"; 
	public bool majorKey = true; 
	public void ChangeKeyMajor(int S)
	{
		majorKey = true; 
		songValue = "";
		noteFirstS   = Resources.Load ("Sounds/" + instrument + "/" + S) as AudioClip;
		noteSecondS  = Resources.Load ("Sounds/" + instrument + "/" + (S + 2)) as AudioClip;
		noteThirdS   = Resources.Load ("Sounds/" + instrument + "/" + (S + 4)) as AudioClip;
		noteFourthS  = Resources.Load ("Sounds/" + instrument + "/" + (S + 5)) as AudioClip;
		noteFifthS   = Resources.Load ("Sounds/" + instrument + "/" + (S + 7)) as AudioClip;
		noteSixthS   = Resources.Load ("Sounds/" + instrument + "/" + (S + 9)) as AudioClip;
		noteSeventhS = Resources.Load ("Sounds/" + instrument + "/" + (S + 11)) as AudioClip;
		noteEighthS  = Resources.Load ("Sounds/" + instrument + "/" + (S + 12)) as AudioClip;
		noteNinthS   = Resources.Load ("Sounds/" + instrument + "/" + (S + 14)) as AudioClip;
	}
	
	public void ChangeKeyMinor(int S)
	{
		majorKey = false; 
		songValue = ""; 
		noteFirstS   = Resources.Load ("Sounds/" + instrument + "/" + S) as AudioClip;
		noteSecondS  = Resources.Load ("Sounds/" + instrument + "/" + (S + 2)) as AudioClip;
		noteThirdS   = Resources.Load ("Sounds/" + instrument + "/" + (S + 3)) as AudioClip;
		noteFourthS  = Resources.Load ("Sounds/" + instrument + "/" + (S + 5)) as AudioClip;
		noteFifthS   = Resources.Load ("Sounds/" + instrument + "/" + (S + 7)) as AudioClip;
		noteSixthS   = Resources.Load ("Sounds/" + instrument + "/" + (S + 8)) as AudioClip;
		noteSeventhS = Resources.Load ("Sounds/" + instrument + "/" + (S + 10)) as AudioClip;
		noteEighthS  = Resources.Load ("Sounds/" + instrument + "/" + (S + 12)) as AudioClip;
		noteNinthS   = Resources.Load ("Sounds/" + instrument + "/" + (S + 14)) as AudioClip;
	}

	//public void SharpAssign (int S) 


// Use this for initialization
	void Start () 
		{
			player = GameObject.FindGameObjectWithTag ("Player"); //Initialize player references.
			playerScript = player.GetComponent<CharacterKontroller>();
			comboScript = player.GetComponent<Combos>();
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
		

		//Temporary Key change mechanism using keybindings. Will bind to UI later hopefully
		if (Input.GetKeyDown (KeyCode.Alpha1)) //A Major
		{
			R = 5;
			ChangeKeyMajor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) //A Minor
		{
			R = 5;
			ChangeKeyMinor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) //C Major
		{
			R = 8;
			ChangeKeyMajor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) //C Minor
		{
			R = 8; 
			ChangeKeyMinor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) //G Major 
		{
			R = 3;
			ChangeKeyMajor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha6)) //G Minor
		{
			R = 3; 
			ChangeKeyMinor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha7)) //Eb Major
		{
			R = 11;
			ChangeKeyMajor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)) //Eb Minor
		{
			R = 11; 
			ChangeKeyMinor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha9)) //D Major
		{
			R = 10; 
			ChangeKeyMajor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Alpha0)) //D Major
		{
			R = 10; 
			ChangeKeyMinor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Minus)) //A Major
		{
			R = 4; 
			ChangeKeyMajor(R); 
		}
		if (Input.GetKeyDown (KeyCode.Equals)) //B Minor
		{
			R = 7; 
			ChangeKeyMinor (R);
		}

		//Temporary Instrument change mechanism using keybindings. 
		if (Input.GetKeyDown (KeyCode.Z)) //Change to trumpet 
		{
			instrument = "Trumpet"; 
			if (majorKey) ChangeKeyMajor(R);
			else ChangeKeyMinor(R);
		}
		if (Input.GetKeyDown (KeyCode.X)) //Change to trumpet 
		{
			instrument = "Violin"; 
			if (majorKey) ChangeKeyMajor(R);
			else ChangeKeyMinor(R);
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
			comboScript.specialAttackValue = 0;
		}
		
		// Check for combos
		comboScript.ComboCheck();
		
	}

////// PROJECTILES AND EFFECTS ////// 

	public void FireSm () {	
		noteSmInstance = Instantiate(musicNoteSm, noteOrigin.position, noteOrigin.rotation) as Rigidbody2D;	
		noteSmInstance.velocity = new Vector2((heWhoShoots.localScale.x * 4), 0);	
	}

	public void FireMd () {
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

}