using UnityEngine;
using System.Collections;

public class BackGroundNoteDelay : MonoBehaviour {
	
	public double thisDelay = 1.75;
	public Transform playerposition; 
	public float currenttime;
	bool updates;
	bool ifplay;
	float ttime;

	// Use this for initialization
	void Start () {
		//thisDelay = 1.5;
		currenttime = 0;
		updates = false;
		ifplay = GetComponent<BackgroundMusics2> ().play;
	}
	
	// Update is called once per frame
	void Update () {
		ifplay = GetComponent<BackgroundMusics2> ().play;
		if (ifplay) {
			if (Time.time > thisDelay) {
				if (Time.time - currenttime > 1) {
					currenttime = Time.time;
					GetComponent<BackgroundMusics2> ().NotePlay ();
					updates = true;
				}
			}
		} else 
			thisDelay = thisDelay + Time.time;
	}
}
