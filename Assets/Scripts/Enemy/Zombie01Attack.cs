using UnityEngine;
using System.Collections;

public class Zombie01Attack : CustomProjectile {

	public override void Customize() {
		this.setCanPunch (true);
		this.setKnockback (100f);
		this.setKnockbackVertical (100f);
		this.setDamage (10f);
	}
	
	/**
	 * Damages the player continuously while it is in the hitbox area
	 **/
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			//Debug.Log ("Enemy about to attack player.");
			DamageableObject player = other.gameObject.GetComponent<DamageableObject> ();

			int direction;
			if (transform.position.x - player.transform.position.x > 0)
				direction = -1;
			else
				direction = 1;

			player.knockback (direction,this.getKnockback (),this.getKnockbackVertical());
			player.damage (this.getDamage ());
		}
	}

	void Start () {
		this.Customize ();
	}

	void Update() {
	}

	void FixedUpdate() {
	}

}
