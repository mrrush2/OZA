/*using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour {

	//                      C   C#  D   D#  E   F   F#  G   G#  A    A#   B    C    C#   D    D#   E    F    F#   G    G#   A    A#   B    C
	//float[] noteValues = {1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f, 11f, 12f, 13f, 14f, 15f, 16f, 17f, 18f, 19f, 20f, 21f, 22f, 23f, 24f, 25f};
	// This was supposed to be useful until I decided it was stupid. Now it's just a good reference. Also everything is a float becuase of an original plan I had.

	// Basic C scale comes preloaded here
	public float noteFirst = 1f;
	public float noteSecond = 3f;
	public float noteThird = 5f;
	public float noteFourth = 6f;
	public float noteFifth = 8f;
	public float noteSixth = 10f;
	public float noteSeventh = 12f;
	public float noteEighth = 13f;
	public float noteNinth = 15f;

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


	public bool ninthUnlocked = false; //Has the player unlocked the use of nine notes?


	public float songValue; //The variable that tracks the current value of your song.
	bool canPlay = true; //Checks if you are currently allowed to play.
	public int randomizerIndex; //For use in the randomizer array
	float[] randomNumbers = {1f, 3f, 4f, 6f, 7f, 6f, 3f, 3f, 7f, 3f, 8f, 6f, 9f, 3f, 9f, 7f, 2f, 6f, 2f, 7f, 1f, 6f, 9f, 2f, 7f, 4f, 3f, 6f, 8f, 8f, 8f};	//A stupid array

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


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		////// AURA STUFF //////

		anim.SetFloat ("SongValue", songValue); //Aura definition. Governs when it should animate.

		if (songValue >= 1)  //If aura exists, light will shine
			auraLight.enabled = true;
		else
			auraLight.enabled = false;


		////// NOTE PLAYING //////

		// All of the keys that can be played are below, referencing the currently active scale
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad1))
		{
			songValue += (noteFirst * randomNumbers[randomizerIndex]) + Mathf.Pow(randomizerIndex,2) + (noteFirst % (randomizerIndex + 1));
			randomizerIndex++;
			FireSm();
			AudioSource.PlayClipAtPoint(noteFirstS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad2))
		{
			songValue += (noteSecond * randomNumbers[randomizerIndex]) + Mathf.Pow(randomizerIndex,2) + (noteSecond % (randomizerIndex + 1));
			randomizerIndex++;
			FireSm();
			AudioSource.PlayClipAtPoint(noteSecondS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad3))
		{
			songValue += (noteThird * randomNumbers[randomizerIndex]) + Mathf.Pow(randomizerIndex,2) + (noteThird % (randomizerIndex + 1));
			randomizerIndex++;
			FireSm();
			AudioSource.PlayClipAtPoint(noteThirdS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad4))
		{
			songValue += (noteFourth * randomNumbers[randomizerIndex]) + Mathf.Pow(randomizerIndex,2) + (noteFourth % (randomizerIndex + 1));
			randomizerIndex++;
			FireSm();
			AudioSource.PlayClipAtPoint(noteFourthS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad5)) 
		{
			songValue += (noteFifth * randomNumbers[randomizerIndex]) + Mathf.Pow(randomizerIndex,2) + (noteFifth % (randomizerIndex + 1));
			randomizerIndex++;
			FireSm();
			AudioSource.PlayClipAtPoint(noteFifthS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad6))
		{
			songValue += (noteSixth * randomNumbers[randomizerIndex]) + Mathf.Pow(randomizerIndex,2) + (noteSixth % (randomizerIndex + 1));
			randomizerIndex++;
			FireSm();
			AudioSource.PlayClipAtPoint(noteSixthS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad7))
		{
			songValue += (noteSeventh * randomNumbers[randomizerIndex]) + Mathf.Pow(randomizerIndex,2) + (noteSeventh % (randomizerIndex + 1));
			randomizerIndex++;
			FireSm();
			AudioSource.PlayClipAtPoint(noteSeventhS, noteOrigin.position);
		}
		if (canPlay && Input.GetKeyDown (KeyCode.Keypad8))
		{
			songValue += (noteEighth * randomNumbers[randomizerIndex]) + Mathf.Pow(randomizerIndex,2) + (noteEighth % (randomizerIndex + 1));
			randomizerIndex++;
			FireSm();
			AudioSource.PlayClipAtPoint(noteEighthS, noteOrigin.position);
		}	
		if (ninthUnlocked && canPlay && Input.GetKeyDown (KeyCode.Keypad9))	//Extra note
		{
			songValue += (noteNinth * randomNumbers[randomizerIndex]) + Mathf.Pow(randomizerIndex,2) + (noteNinth % (randomizerIndex + 1));
			randomizerIndex++;
			FireSm();
			AudioSource.PlayClipAtPoint(noteNinthS, noteOrigin.position);
		}	


		////// RESETS //////

		if (Input.GetKeyDown (KeyCode.Keypad0)) //Reset button
		{
			randomizerIndex = 0;
			songValue = 0;
			specialAttackValue = 0;
		}

		if (randomizerIndex >= 29) //Resets the randomness array.
			randomizerIndex = 0;


		////// COMBOS //////

		if (specialAttackValue < 1 && songValue == 364)  //Mary Had a Little Lamb 1
		{  
			FireMd();
			specialAttackValue++;
		}
		if (specialAttackValue < 2 && songValue == 678)  //Mary Had a Little Lamb 2
		{  
			FireMd();
			specialAttackValue++;
		}
		if (specialAttackValue < 3 && songValue == 1329)  //Mary Had a Little Lamb 3
		{  
			FireMd();
			specialAttackValue++;
		}
		if (specialAttackValue < 4 && songValue == 6856)  //Mary Had a Little Lamb 4
		{  
			FireMd();
			specialAttackValue++;
			randomizerIndex = 0;
			songValue = 0;
			specialAttackValue = 0;
		}

	
	
	
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


}
*/ 