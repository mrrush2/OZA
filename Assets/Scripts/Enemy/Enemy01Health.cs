using UnityEngine;
using System.Collections;

public class Enemy01Health : DamageableObject {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "PlayerAttack") {
			Debug.Log ("Zombie collider triggered as attack hit.");
			CustomProjectile attack = other.gameObject.GetComponent<CustomProjectile>();
			damage (attack.getDamage());
			attack.die ();
		}
		Debug.Log ("-Zombie collision- HP: " + getHealth ());
	}

}
