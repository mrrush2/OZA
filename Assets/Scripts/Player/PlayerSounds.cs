using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour 
{
	GameObject player;
	
	public AudioClip stepSound;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	
	public void Step ()
	{
		//audio.pitch = Random.Range (1, 2);
		AudioSource.PlayClipAtPoint(stepSound, player.transform.position);
	}
}
