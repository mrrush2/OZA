using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour 
{
	GameObject player;
	
	AudioClip stepSound;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		stepSound = Resources.Load ("Sounds/Generic/Footstep") as AudioClip;

	}

	
	public void Step ()
	{
		//audio.pitch = Random.Range (1, 2);
		AudioSource.PlayClipAtPoint(stepSound, player.transform.position);
	}
}
