using UnityEngine;
using System.Collections;

/**
 * 
 * --- CustomProjectile superclass ---
 * 
 * This sets the basic perameters behind custom projectiles.
 * 
 * These parameters include:
 * - Damage
 * - Speed
 * - Gravity
 * - Sprite
 * - Custom behavior
 * - Punchthrough
 * - Timeout
 * - ...And possibly more
 * 
 * - Damage
 * ---- Variable damage indicates how much damage the projectile causes.
 * 
 * - Speed
 * ---- Variable speed determines how fast the projectile moves.
 * 
 * - Gravity
 * ---- Variable gravity changes how the projectile is affected in Gs.
 * 
 * - Punchthrough
 * ---- Variable canPunch determines if the projectile can go through
 * 		objects and damage them until it hits a wall.
 * 
 * - Timeout
 * ---- Variable timeout determines how many seconds the projectile can
 * 		exist for.
 * 
 **/


public class CustomProjectile : MonoBehaviour {

	protected float damage = 10f,
					speed = 2.5f,
					gravity = 0f,
					timeout = 10f,
					direction = 1f;

	protected bool canPunch = false,
				   directionRight = true;

	// For the timer
	float startTime;

	// Layer to determine what can be hit
	public LayerMask wallLayer;

	// Reference to the instance note
	public Rigidbody2D thisNote;

	public float getDamage() {return this.damage;}
	public void setDamage(float value) {this.damage = value;}
	public float getSpeed() {return this.speed;}
	public void setSpeed(float value) {this.speed = value;}
	public float getGravity() {return this.gravity;}
	public void setGravity(float value) {this.gravity = value;}
	public bool getCanPunch() {return this.canPunch;}
	public void setCanPunch(bool value) {this.canPunch = value;}
	public float getTimeout() {return this.timeout;}
	public void setTimeout(float value) {this.timeout = value;}
	public void setDirection(float value) {
		if (value < 0) this.direction = -1;
		if (value > 0) this.direction = 1;
	}

	// Manipulation methods

	/**
	 * Destroys the object if the projectile hits a damageable object, and
	 * the projectile does not have punchtrough.
	 **/
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("-Projectile collision-");
	}

	/**
	 * Destroys the object. This method should be overwritten.
	 **/
	public void die() {
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}

	void Update() {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody2D.velocity = new Vector2(direction * speed,0);
		bool noteHit = Physics2D.OverlapCircle (thisNote.position , .02f, wallLayer);
		if (Time.time - startTime >= timeout || noteHit)
			die ();
	}
}
