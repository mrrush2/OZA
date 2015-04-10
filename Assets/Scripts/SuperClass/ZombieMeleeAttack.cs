using UnityEngine;
using System.Collections;

public class ZombieMeleeAttack : CustomProjectile {

	public float damageValue = 20f,
				 knockbackValue = 3f;

	// Manipulation methods
	
	/**
	 * Damages the player continuously while it is in the hitbox area
	 **/
	void OnTriggerStay2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {
			Debug.Log ("Enemy about to attack player.");
			DamageableObject player = other.gameObject.GetComponent<DamageableObject> ();
			player.damage (this.getDamage ());
		}
	}

	// Use this for initialization
	void Start () {
		this.setDamage (damageValue);
		this.setKnockback (knockbackValue);
		this.setCanPunch (true);
	}
	
	void Update() {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}
}
