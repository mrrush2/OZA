using UnityEngine;
using System.Collections;

public class PlayerHealth : DamageableObject {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "EnemyAttack") {
			Debug.Log ("Player collider triggered as attack hit.");
			CustomProjectile attack = other.gameObject.GetComponent<CustomProjectile>();
			damage (attack.getDamage());
			attack.die ();
		}
		Debug.Log ("-Player collision- HP: " + getHealth ());
	}

}
