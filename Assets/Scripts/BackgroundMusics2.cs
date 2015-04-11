using UnityEngine;
using System.Collections;

public class BackgroundMusics2 : MonoBehaviour {

	public Transform playerposition; 
	public	float starttime; 
	ScalesOBJ	ScalesTime;

	int Root = 8;
	public int DDelay = 0;
	public AudioClip[] note = new AudioClip[10]; 

	public void LoadSounds(int S) //Sounds are loaded in the relative minor of the major key, will implement that if minor then keep minor..? 
	{
		note[0] = Resources.Load ("Sounds/SineSynth/" + (S- 3)) 	as AudioClip;	
		note[1] = Resources.Load ("Sounds/SineSynth/" + (S - 1)) 	as AudioClip;
		note[2] = Resources.Load ("Sounds/SineSynth/" + (S))	 	as AudioClip;
		note[3] = Resources.Load ("Sounds/SineSynth/" + (S + 2)) 	as AudioClip;
		note[4] = Resources.Load ("Sounds/SineSynth/" + (S + 4)) 	as AudioClip;
		note[5] = Resources.Load ("Sounds/SineSynth/" + (S + 5)) 	as AudioClip;
		note[6] = Resources.Load ("Sounds/SineSynth/" + (S + 7)) 	as AudioClip;
		note[7] = Resources.Load ("Sounds/SineSynth/" + (S + 9)) 	as AudioClip;
		note[8] = Resources.Load ("Sounds/SineSynth/Rest")			as AudioClip;
	}

	public int[,] NoteProbabilityGenerator()
	{
		int[,] NoteProbability = new int [10,10];
		for (int x = 0; x < 9; x++) 
		{
			int Prob = 0;										 //initial probability for the note is 0; 
			for (int y = 0; y < 10; y++) 
				{
					int	RandNum = Random.Range (0,9) + 5; 
					Prob = Prob + RandNum; 						//Compounds the probability 
					if (y == 9 || Prob >= 100) Prob = 100; 		//the last column (or any value where the compounded Prob > 100) has value of 100; 
					NoteProbability [x,y] = Prob; 
				}
		}
		return NoteProbability; 
	}

	bool playnote = false;
	public int send = 0; 
	int currentnote = 0, nextnote = 0; 
	public int[,] TrueProbability = new int[10,10]; //array

	public int BassNotes(int[,] ProbArray, int StartNote)
	{
		while (playnote == false) 
		{
			int CurrentRand = Random.Range (0, 104); 
			for (int nextnote = 0; nextnote < 9; nextnote++)
			{
				if ((CurrentRand >= ProbArray[StartNote,nextnote]) && (CurrentRand < ProbArray[StartNote,nextnote+1]))
				{
					send = nextnote;
					playnote = true;
				}
				if (CurrentRand >= ProbArray[StartNote,8]) 
				{
					send = 8; 
					playnote = true;
				}
			}
		}
		return send;
	}

	public void NotePlay()
	{
			nextnote = BassNotes (TrueProbability, currentnote);
			currentnote = nextnote; 
			AudioSource.PlayClipAtPoint (note [currentnote], playerposition.position);
			playnote = false;
	}

	//Used to initialize
	void Start () 
	{
		LoadSounds (Root);
		AudioSource.PlayClipAtPoint(note[0], playerposition.position);
		TrueProbability = NoteProbabilityGenerator ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((Time.time - starttime) > 1) {
			starttime = Time.time; 
			NotePlay ();
		}
		int R = ScalesTime.currentRoot;
		if (R != Root)
				LoadSounds (R);
		
	}
}
