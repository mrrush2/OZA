
using UnityEngine;
using System.Collections;
using System.Media;
//Originally System.Media was unusable thanks to us using Net 2.0 Subset, but was fixed by going into player settings


public class CombatController2 : MonoBehaviour {


	//                     E    F    F#   G    G#   A    A#   B    C    C#    D    D#     E     F    F#     G    G#     A    A#     B     C    C#     D    D#     E  
	//					   0    1    2    3    4    5    6    7    8    9    10    11    12    13    14    15    16    17    18    19    20    21    22    23    24        
	//We set our R (Root) to be 8 because that is the value of low C currently for the violin. Might consider making 8 the default key of any instrument? 
	//Considering a scale is created using 9 notes (R to R+14), our lowest scale is low E (0), and our highest scale is D (10), allowing R to vary between 0 and 10. Should make it 11... Note 25. 
	//This currently allows a possiblity of 11 major scales, and 11 minor scales (when they are unlocked). 
	//Also created a possiblity for changing instruments by creating a vairable for instrument name. V stands for violin at the time.
	//defining major or minor for song purposes...
	int R = 8; 
	string insmt = "V"; 
	public bool majorKey = true; 
	public string[] noteRep = {"1", "2", "3", "4", "5", "6", "7", "8", "9"}; 

	public string songValue = ""; //The variable that tracks the current value of your song.
	bool canPlay = true; //Checks if you are currently allowed to play.
	public bool ninthUnlocked = false; //Has the player unlocked the use of nine notes?

	// All variables used to fire physical notes.
	public Rigidbody2D musicNoteSm;
	public Rigidbody2D musicNoteMd;
	Rigidbody2D noteSmInstance;
	Rigidbody2D noteMdInstance;
	public Transform noteOrigin; 

	public Transform heWhoShoots;
	float specialAttackValue = 0; //On a per-special basis, this controls how many times a combo runs when activated.

	public KeyCode key1 = KeyCode.B,
				   key2 = KeyCode.N,
				   key3 = KeyCode.M,
				   key4 = KeyCode.G,
				   key5 = KeyCode.H,
				   key6 = KeyCode.J,
				   key7 = KeyCode.T,
				   key8= KeyCode.Y,
			   keyReset = KeyCode.V;

	//Sound holding clips are declared here.
	public AudioClip noteFirstS;
	public AudioClip noteSecondS;
	public AudioClip noteThirdS;
	public AudioClip noteFourthS;
	public AudioClip noteFifthS;
	public AudioClip noteSixthS;
	public AudioClip noteSeventhS;
	public AudioClip noteEighthS;
	public AudioClip noteNinthS;
	
	// For visual manipulation
	public Animator auraAnim;
	public Light auraLight;

	CharacterKontroller playerScript;
	GameObject player;
	
	// Use this for initialization
	//Initializes the notes for the player to play, as well as the instrument. 
	void Start () {
		if (majorKey)
			ChangeKeyMajor (R);
		else
			ChangeKeyMinor (R); 
	}

	//Methods Introduced for Key Changes 
	//								   1, 2, 3, 4, 5, 7, 7, 8
	//Major Keys follow the pattern of R, W, W, H, W, W, W, H
	//									 +2,+2,+1,+2,+2,+2,+1
	//Minor Keys follow the pattern of R, W, H, W, W, H, W, W
	//									 +2,+1,+2,+2,+1,+2,+2
	//R is Root for the starting note of the scale, W is a whole step (+2), and H is a half step (+1)
	// However if we initialize it in void Start() we can bypass the issue. Will rename playerN to NoteN later on. Will shorten directory with time (eliminate \Violin\ for example)
	//Resets songvalue to avoid a bug... 
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
	
	// Update is called once per frame
	void Update () {

		// Script reference for the onLadder boolean
		player = GameObject.FindGameObjectWithTag ("Player");
		playerScript = player.GetComponent<CharacterKontroller>();

		////// AURA STUFF //////
		
		auraAnim.SetBool ("IsPlaying", (!songValue.Equals (""))); //Aura definition. Governs when it should animate.
		
		if (songValue.Equals (""))  //If aura exists, light will shine
			auraLight.enabled = false;
		else
			auraLight.enabled = true;

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
		
		/*
        if (randomizerIndex >= 29) //Resets the randomness array.
            randomizerIndex = 0;
        */        
		
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
	void NotePress (int note) {
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

//Should add citation for sounds here cause we kinda stole em

