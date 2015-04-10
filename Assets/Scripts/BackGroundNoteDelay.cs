using UnityEngine;
using System.Collections;

public class BackGroundNoteDelay : MonoBehaviour {

	public BackgroundMusics2 DelayNote;
	double thisDelay;
	public Transform playerposition; 
	public float currenttime;

	// Use this for initialization
	void Start () {
		thisDelay = 1.75;
		currenttime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > thisDelay) {
			if (Time.time - currenttime > 1){
				currenttime = Time.time;
				GetComponent<BackgroundMusics2> ().NotePlay ();
			}
		}
	}
}
