using UnityEngine;
using System.Collections;

public class AggroTime : MonoBehaviour {

	public BackgroundMusics2 DelayNote;
	ZombieFollow Following;
	public Transform playerposition; 
	public float currenttime;
	AudioClip Percuss;
	
	// Use this for initialization
	void Start () {
		currenttime = 0;
		Percuss = Resources.Load ("Sounds/Kickdrum") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
			if (Time.time - currenttime > 0.5){
				currenttime = Time.time;
			AudioSource.PlayClipAtPoint(Percuss, playerposition.position);
			//	GetComponent<BackgroundMusics2> ().NotePlay ();
			}
	}
}
