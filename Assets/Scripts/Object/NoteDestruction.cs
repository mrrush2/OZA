using UnityEngine;
using System.Collections;

public class NoteDestruction : MonoBehaviour {

	float secondsUntilDestroy = 10;
	float startTime;
	public LayerMask whatCanBeHit;
	bool noteHit = false;
	public Rigidbody2D thisNote;


	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		noteHit = Physics2D.OverlapCircle (thisNote.position , .02f, whatCanBeHit);
		if (noteHit || Time.time - startTime >= secondsUntilDestroy)
			Destroy (this.gameObject);
	}
}
