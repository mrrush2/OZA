using UnityEngine;
using System.Collections;

public class GenericNoteAttack : CustomProjectile {
	/**
	 * Damages the enemy on hit
	 **/
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			DamageableObject enemy = other.gameObject.GetComponent<DamageableObject> ();
			
			int directionKnockback;
			if (transform.position.x - enemy.transform.position.x > 0)
				directionKnockback = -1;
			else
				directionKnockback = 1;
			
			enemy.knockback (directionKnockback,this.getKnockback (),this.getKnockbackVertical());
			enemy.damage (this.getDamage ());
			this.die (true);
		}
	}
}
