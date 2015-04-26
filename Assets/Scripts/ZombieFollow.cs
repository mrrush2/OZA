using UnityEngine;
using System.Collections;

public class ZombieFollow : MonoBehaviour {
	public Transform target;//set target from inspector instead of looking in Update
	public float followSpeed = .8f;
	public float patrolSpeed = .65f;
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
	float startTime;
	Vector3 theScale;
	float timer;

	public bool canMove = true;
	public float reEnableMovementTimer = 0f;

	void Start()
	{
		anim = GetComponent < Animator > ();
		patrolTimer = initialPatrolTime;
		theScale = transform.localScale;
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
	
		reEnableMovementTimer += Time.deltaTime;
		if(!canMove && reEnableMovementTimer > .5f)
			canMove = true;
		
		
	}
	
	void Flip(){
		if((Time.time - startTime) > 1) 
		{
			startTime = Time.time;
			facingRight = !facingRight;
			theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
	
	void Chase () {
		//if (distance == 0) {
			//rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		 if (facingRight) {
			if (canMove) rigidbody2D.velocity = new Vector2 (theScale.x * followSpeed, rigidbody2D.velocity.y);
		} else {
			if (canMove) rigidbody2D.velocity = new Vector2 (theScale.x * followSpeed, rigidbody2D.velocity.y);
		}
	}
	
	void Patrol(){
		if (patrolTimer > initialPatrolTime/timer) {
			if (canMove) rigidbody2D.velocity = new Vector2 (theScale.x * patrolSpeed, rigidbody2D.velocity.y);
			patrolTimer -= Time.deltaTime;
		}
		else if (patrolTimer > 0) {
			Flip();
			if (canMove) rigidbody2D.velocity = new Vector2 (theScale.x * patrolSpeed, rigidbody2D.velocity.y);
			patrolTimer -= Time.deltaTime;
		}
		else {
			Flip();
			timer = (Random.Range (1,5));
			Debug.Log(timer);
			patrolTimer = initialPatrolTime;
		}
	}
}
