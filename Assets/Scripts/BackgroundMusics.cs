using UnityEngine;
using System.Collections;

public class BackgroundMusics : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

/*
Forgive me for weird formatting, my screen is slightly compressed right now. 

This is code written up in Java for writing Markov Chains, and for random (procedural) note generation. 
Will convert to C# at the hack thing tomorrow. 

I did not really use good variables but oh well. Will clean will clean. 

public class NDProbArrayTest {
	public static void main(String args[]){
		int i = 10; 
		int Z = 0;
		int[][] numArray = new int[i][i]; //creates a 10x10 array for assigning probabilities
		
		System.out.println("Note"+"\t"+"A"+"\t"+"B"+"\t"+"C"+"\t"+"D"+"\t"+"E"+"\t"+"F"+"\t"+"G"+"\t"+"A2"+"\t"+"Rest"+"\t"+"Nu");
		
		for (int x = 0; x < i-1; x++)
		{
			int N = 0; 
			System.out.print((char)(65+x)+"\t");
			for (int y = 0; y < i; y++)
			{
				Z=(int)(Math.random()*9)+5; //the plus five is there so in an event that you have like a 34 for A2, it can
											//be fudged to a higher value to lessen the chance of rests. idk, can remove.
				N = N + Z;
				numArray[x][y] = N; 
				if (y == 9) N = 100; 		//has the final 100 to be a comparison value, might not be necessary. probably isnt
				System.out.print(numArray[x][y] + "\t"); 
				if (y == 9) System.out.println();
			}
		}
		
		String[] charStuff = new String[]{"a", "b", "c#", "d", "e", "f#", "g#", "A", "0"}; 	//for printing out note name
																							//Can also apply to songs?
		
		
		System.out.println("\n");
		int x = 0; 
		int Q = (int)((Math.random()*8)); //randomly generate starting note
		System.out.print(charStuff[Q] + "\t");
		for (int I = 1; I < 400; I++)
		{
			Z = (int)((Math.random()*104)+1); 
			if (I%20 == 0) System.out.print("\n");
			x = 0; 
			for (boolean go = false;(!go); x++)
				{
					if ((Z >= numArray[0][x]) && (Z < numArray[0][x+1]))	//does a check on teh current note with the note
																			//next to it
					{
						System.out.print(charStuff[x] + "\t");
						break;
					}
					else if (Z >= 100 || x == 8)
					{
							System.out.print("0" + "\t"); //says that at 100, no note will play (rest). its a bad check...?
							break;
					}
				}
		}
	}
}

Can produce the following result: 

Note	A	B	C#	D	E	F#	G#	A2	Rest	Nu
A		12	25	32	38	48	58	64	71	78		100	
B		12	20	25	37	48	54	66	74	81		100	
C#		12	18	25	35	44	56	65	74	82		100	
D		6	12	20	32	41	47	53	61	73		100	
E		11	24	30	41	47	57	63	75	84		100	
F#		5	15	21	26	36	44	53	59	68		100	
G#		7	20	30	43	51	57	66	72	77		100	
A2		13	26	32	39	47	54	66	74	85		100	
Rest	10	19	25	36	49	55	63	68	78		100	

Basically, if the current note is A, there is ~13% chance of the next note being A, 9% that it will be B, 
and so on. The technique being used here is Markov Chains, where the next note is determined by the state of the
current system. 

See http://peabody.sapp.org/class/dmp2/lab/markov1/ for further reading. 

Using the probabilities that were (randomly) generated, we get the following 400 notes to be played: 

a	d	e	0	0	0	0	a	f#	e	f#	0	0	a	a	0	0	d	e	0	
d	0	a	0	b	A	a	0	0	e	A	0	g#	0	a	a	0	0	b	b	
d	A	0	0	0	d	d	e	g#	0	c#	0	0	a	0	0	b	0	0	A	
0	g#	b	c#	c#	f#	a	f#	b	b	0	0	a	d	c#	a	a	d	0	a	
f#	0	0	0	0	e	0	0	A	b	f#	0	d	a	d	a	g#	A	b	A	
e	0	a	a	0	d	0	f#	b	a	0	0	e	b	a	d	d	0	0	0	
0	a	0	a	b	0	b	c#	a	0	d	a	e	a	a	0	A	a	0	f#	
0	f#	e	0	0	a	e	d	d	a	0	e	0	0	A	0	a	0	0	d	
g#	0	0	f#	b	0	0	0	f#	0	f#	A	g#	a	0	0	0	a	a	g#	
0	e	0	d	d	g#	0	0	e	0	0	d	b	0	0	d	d	f#	a	a	
f#	b	0	e	A	0	e	0	A	0	b	e	a	f#	e	b	g#	g#	f#	b	
0	0	f#	0	d	A	0	a	0	e	a	0	g#	e	a	c#	0	0	a	e	
b	g#	b	0	e	0	A	b	f#	g#	0	c#	0	0	0	f#	0	0	0	0	
A	d	c#	c#	0	d	b	e	0	0	0	f#	c#	b	e	0	e	0	b	0	
A	0	a	0	0	0	0	e	0	0	0	0	0	0	0	g#	f#	e	a	0	
f#	0	g#	g#	c#	0	0	c#	b	e	e	0	e	a	0	d	0	a	c#	c#	
A	0	c#	e	d	0	A	0	a	g#	d	0	c#	0	c#	e	a	d	d	a	
f#	a	c#	0	a	d	e	0	0	a	a	d	0	0	f#	c#	0	g#	a	e	
d	e	0	f#	a	e	b	0	A	a	0	0	c#	A	b	a	d	g#	0	b	
0	d	0	d	f#	0	a	f#	0	0	e	g#	f#	0	0	c#	c#	0	g#	e	

The results show that 0 (rest note) is a common played note in the sequence. 

Perhaps we can have, say, four of these running (dependent on one probability) for note sounds, and one running
for a drum/effect track.


ALso for more reading: http://stackoverflow.com/questions/180858/procedural-music-generation-techniques

An idea is that if you are playing in a major key, the background music will be played in the relative minor
of the key that you are currently in. That way, the atmosphere is always sorta negative. 
*/ 