using UnityEngine;
using System.Collections;

public class Arpegginade : CustomProjectile {

	GameObject nade;
	float boomTimer = 0f;
	Rigidbody2D chordElement;
	
	GameObject player;
	ComboEffects effect;
	CombatControllerIII combat;
	
	int explosionBitCount = 18;
	float radianMult;
	
	AudioClip noteOne;
	AudioClip noteTwo;
	AudioClip noteThree;

	// Use this for initialization
	void Awake () {
		nade = gameObject; // ref to the object this script is attached to
		
		player = GameObject.FindGameObjectWithTag("Player");
		effect = player.GetComponent<ComboEffects>();
		combat = player.GetComponent<CombatControllerIII>();
		radianMult =  6.28f / (float)explosionBitCount;		// For nice equal spacing of notes in a radial manner
		nade.rigidbody2D.AddForce (new Vector2((player.transform.localScale.x * 120f) + (player.rigidbody2D.velocity.x*20),150f + (player.rigidbody2D.velocity.y*30)));	// Manually hardcoded initial velocity
		nade.rigidbody2D.AddTorque (-1f);
		
		noteOne = combat.noteFirstS;
		noteTwo = combat.noteThirdS;
		noteThree = combat.noteFifthS;
	}
	
	// Update is called once per frame
	void Update () {
		boomTimer += Time.deltaTime;
		if (boomTimer > 1.75f)
		{
			ChordSplosion();
		}
	}
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Enemy") 
		{
			ChordSplosion();
		}
	}
	
	void ChordSplosion () 
	{
		for (int i = 0; i < explosionBitCount; i++)
		{
			chordElement = Instantiate(effect.med, nade.transform.position, player.transform.rotation) as Rigidbody2D;	
			CustomProjectile note = chordElement.gameObject.GetComponent<CustomProjectile>();			
			note.setDamage (7f);
			note.setSpeed (0f);
			note.setTimeout (1f);
			note.setCanPunch (true);
			note.setKnockback (2f);
			chordElement.rigidbody2D.AddForce(new Vector2((64*Mathf.Cos(radianMult*i)), 64*Mathf.Sin(radianMult*i)));
			// MATH MAKES PRETTY EXPLOSIONS	
		}
		AudioSource.PlayClipAtPoint(noteOne, nade.transform.position);
		AudioSource.PlayClipAtPoint(noteTwo, nade.transform.position);
		AudioSource.PlayClipAtPoint(noteThree, nade.transform.position);
		CameraLogic.ShakeItUp(0.25f, 0.2f, 1.0f);
		die();
	}
}
