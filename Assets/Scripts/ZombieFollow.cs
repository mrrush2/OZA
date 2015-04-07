using UnityEngine;
using System.Collections;

public class ZombieFollow : MonoBehaviour {
	public Transform target;//set target from inspector instead of looking in Update
	public float followSpeed = .5f;
	public float patrolSpeed = .2f;
	bool facingRight = true;
	Animator anim;
	public Transform sightStart, sightEnd;
	public LayerMask rayCanHit;
	public bool edge;
	private float distance;
	public float initialPatrolTime = 5.0f;
	public float patrolTimer;
	public float chaseDistance = 2f;
	bool isChasing = false;
	
	void Start()
	{
		anim = GetComponent < Animator > ();
		patrolTimer = initialPatrolTime;
	}
	void Update() {
		
		Debug.DrawLine(sightStart.position, sightEnd.position, Color.blue); //Visual Representation of Ray Jesus
		edge = Physics2D.Linecast(sightStart.position, sightEnd.position, rayCanHit);
		if (!edge) {
			Flip ();
		}
		
		//change to look at the player
		target = GameObject.FindWithTag ("Player").transform;		
		distance = transform.position.x - target.position.x;
		//FACE RIGHT OR LEFT
		anim.SetFloat ("Speed", Mathf.Abs(rigidbody2D.velocity.x));
		if (distance > .1 && !facingRight && edge)
			Flip ();
		else if (distance < -.1 && facingRight && edge)
			Flip ();
		
		//if within range, chase player
		isChasing = (Vector2.Distance (target.position, transform.position) < chaseDistance);
		
		if (isChasing == true) {
			Chase ();
		}
		else {
			Patrol ();
		}
		
	}
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	void Chase () {
		if (distance == 0) {
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		} else if (facingRight) {
			rigidbody2D.velocity = new Vector2 (-1 * followSpeed, rigidbody2D.velocity.y);
		} else {
			rigidbody2D.velocity = new Vector2 (1 * followSpeed, rigidbody2D.velocity.y);
		}
	}
	
	void Patrol(){
		if (patrolTimer > initialPatrolTime/2) {
			rigidbody2D.velocity = new Vector2 (-1 * patrolSpeed, rigidbody2D.velocity.y);
			patrolTimer -= Time.deltaTime;
		}
		else if (patrolTimer > 0) {
			Flip();
			rigidbody2D.velocity = new Vector2 (1 * patrolSpeed, rigidbody2D.velocity.y);
			patrolTimer -= Time.deltaTime;
		}
		else {
			Flip();
			patrolTimer = initialPatrolTime;
		}
	}
}
