using UnityEngine;
using System.Collections;

public class Enemy01Attack : MonoBehaviour {

	public Rigidbody2D zombieAttack;
	public Transform attackOrigin;

	// Use this for initialization
	void Start () {

		//Rigidbody2D attackInstance = Instantiate(zombieAttack, transform.position, transform.rotation) as Rigidbody2D;	
		CustomProjectile attack = zombieAttack.gameObject.GetComponent<CustomProjectile>();
		attack.setDirection (-1);//(heWhoShoots.localScale.x);
		attack.setTimeout (0);
		attack.setDamage (15);
		attack.setSpeed (0);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector2 (attackOrigin.position.x, attackOrigin.position.y);
	}
}
