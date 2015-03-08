
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
	
	// For visual manipulation
	public Animator auraAnim;
	public Light auraLight;


	SoundPlayer player1, player2, player3, player4, player5, player6, player7, player8, player9; 
	
	// Use this for initialization
	//Initializes the notes for the player to play, as well as the instrument. 
	void Start () {
		if (majorKey)
			ChangeKeyMajor (R);
		else
			ChangeKeyMinor (R); 
	}
	
	//Method Introduced for Key changes 
	//Major Keys follow the pattern of R, W, W, H, W, W, W, H
	//Minor Keys follow the pattern of R, W, H, W, W, H, W, W
	//R is Root for the starting note of the scale, W is a whole step (+2), and H is a half step (+1)
	// Simple concatation fails because R is considered nonstatic and gives error CS0236: string fileloc = "Assests\\Sounds\\Violin\\V" + R +".wav";
	// However if we initialize it in void Start() we can bypass the issue. Will rename playerN to NoteN later on. Will shorten directory with time (eliminate \Violin\ for example)
	//Resets songvalue to avoid a bug... 
	public void ChangeKeyMajor(int S)
	{
		majorKey = true; 
		songValue = "";
		player1 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + S + ".wav");
		player2 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 2) + ".wav");
		player3 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 4) + ".wav");
		player4 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 5) + ".wav");
		player5 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 7) + ".wav");
		player6 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 9) + ".wav");
		player7 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 11) + ".wav");
		player8 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 12) + ".wav");
		player9 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 14) + ".wav");  
	}
	
	public void ChangeKeyMinor(int S)
	{
		majorKey = false; 
		songValue = ""; 
		player1 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + S + ".wav");
		player2 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 2) + ".wav");
		player3 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 3) + ".wav");
		player4 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 5) + ".wav");
		player5 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 7) + ".wav");
		player6 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 8) + ".wav");
		player7 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 10) + ".wav");
		player8 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 12) + ".wav");
		player9 = new SoundPlayer(@"Assets\Sounds\Violin\" + insmt + (S + 14) + ".wav");  
	}
	
	// Update is called once per frame
	void Update () {
		
		////// AURA STUFF //////
		
		auraAnim.SetBool ("IsPlaying", (!songValue.Equals (""))); //Aura definition. Governs when it should animate.
		
		if (songValue.Equals (""))  //If aura exists, light will shine
			auraLight.enabled = false;
		else
			auraLight.enabled = true;

		//Temporary Key change mechanism using keybindings. 
		if (Input.GetKeyDown (KeyCode.T))
		{
			R = 5; 
			ChangeKeyMajor(R); 
		}
		if (Input.GetKeyDown (KeyCode.R))
		{
			R = 8; 
			ChangeKeyMinor(R); 
		}
		if (Input.GetKeyDown (KeyCode.G))
		{
			R = 8; 
			ChangeKeyMajor(R); 
		}
		
		////// NOTE PLAYING //////
		// All of the keys that can be played are below, referencing the currently active scale
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad1))
		{
			NotePress(1);
			player1.Play ();
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad2))
		{
			NotePress(2);
			player2.Play ();
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad3))
		{
			NotePress(3);
			player3.Play ();
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad4))
		{
			NotePress(4);
			player4.Play ();
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad5)) 
		{
			NotePress(5);
			player5.Play ();
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad6))
		{
			NotePress(6);
			player6.Play ();
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad7))
		{
			NotePress(7);
			player7.Play ();
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad8))
		{
			NotePress(8);
			player8.Play ();
		}   
		if (ninthUnlocked && canPlay && Input.GetKeyDown (KeyCode.Keypad9)) //Extra note
		{
			NotePress(9);
			player9.Play ();
		}   
		
		
		////// RESETS //////
		
		if (Input.GetKeyDown (KeyCode.Keypad0)) //Reset button
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

