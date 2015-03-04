
using UnityEngine;
using System.Collections;

public class CombatController2 : MonoBehaviour {
	
	//                      C   C#  D   D#  E   F   F#  G   G#  A    A#   B    C    C#   D    D#   E    F    F#   G    G#   A    A#   B    C
	//float[] noteValues = {1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f, 15f, 16f, 17f, 18f, 19f, 20f, 21f, 22f, 23f, 24f, 25f};
	// This was supposed to be useful until I decided it was stupid. Now it's just a good reference. Also everything is a float becuase of an original plan I had.
	
	/*
    // Basic C scale comes preloaded here
    public String noteFirst = "1";
    public String noteSecond = "2";
    public String noteThird = "3";
    public String noteFourth = "4";
    public String noteFifth = "5";
    public String noteSixth = "6";
    public String noteSeventh = "7";
    public String noteEighth = "8";
    public String noteNinth = "9";
    */

	// Custom key bindings
	public KeyCode keyC = KeyCode.B,
				   keyD = KeyCode.N,
				   keyE = KeyCode.M,
				   keyF = KeyCode.G,
				   keyG = KeyCode.H,
				   keyA = KeyCode.J,
				   keyB = KeyCode.T,
				   keyC2 = KeyCode.Y,
				   keyReset = KeyCode.V;
	
	public string[] noteRep = {"1", "2", "3", "4", "5", "6", "7", "8", "9"}; 
	
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
	// public int randomizerIndex; //For use in the randomizer array
	// float[] randomNumbers = {1f, 3f, 4f, 6f, 7f, 6f, 3f, 3f, 7f, 3f, 8f, 6f, 9f, 3f, 9f, 7f, 2f, 6f, 2f, 7f, 1f, 6f, 9f, 2f, 7f, 4f, 3f, 6f, 8f, 8f, 8f};   //A stupid array
	
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
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		////// AURA STUFF //////

		auraAnim.SetBool ("IsPlaying", (!songValue.Equals (""))); //Aura definition. Governs when it should animate.
		
		if (songValue.Equals (""))  //If aura exists, light will shine
			auraLight.enabled = false;
		else
			auraLight.enabled = true;

		
		////// NOTE PLAYING //////
		
		// All of the keys that can be played are below, referencing the currently active scale
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad1) || Input.GetKeyDown (keyC))
		{
			NotePress(1);
			AudioSource.PlayClipAtPoint(noteFirstS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad2) || Input.GetKeyDown (keyD))
		{
			NotePress(2);
			AudioSource.PlayClipAtPoint(noteSecondS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad3) || Input.GetKeyDown (keyE))
		{
			NotePress(3);
			AudioSource.PlayClipAtPoint(noteThirdS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad4) || Input.GetKeyDown (keyF))
		{
			NotePress(4);
			AudioSource.PlayClipAtPoint(noteFourthS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad5) || Input.GetKeyDown (keyG)) 
		{
			NotePress(5);
			AudioSource.PlayClipAtPoint(noteFifthS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad6) || Input.GetKeyDown (keyA))
		{
			NotePress(6);
			AudioSource.PlayClipAtPoint(noteSixthS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad7) || Input.GetKeyDown (keyB))
		{
			NotePress(7);
			AudioSource.PlayClipAtPoint(noteSeventhS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad8) || Input.GetKeyDown (keyC2))
		{
			NotePress(8);
			AudioSource.PlayClipAtPoint(noteEighthS, noteOrigin.position);
		}   
		if (ninthUnlocked && canPlay && Input.GetKeyDown (KeyCode.Keypad9)) //Extra note
		{
			NotePress(9);
			AudioSource.PlayClipAtPoint(noteNinthS, noteOrigin.position);
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
		
		if (specialAttackValue == 0 && songValue.Equals("6545666"))  //Mary Had a Little Lamb 1
		{  
			FireMd();
			specialAttackValue++;
		}
		if (specialAttackValue == 1 && songValue.Equals("6545666"+"555"))  //Mary Had a Little Lamb 2
		{  
			FireMd();
			specialAttackValue++;
		}
		if (specialAttackValue == 2 && songValue.Equals("6545666"+"555"+"688"))  //Mary Had a Little Lamb 3
		{  
			FireMd();
			specialAttackValue++;
		}
		if (specialAttackValue == 3 && songValue.Equals("6545666"+"555"+"688"+"6545666655654"))  //Mary Had a Little Lamb 4
		{  
			FireMd();
			specialAttackValue = 0;
			songValue = "";
		}
		
	}
	
}


