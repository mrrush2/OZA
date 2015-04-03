using UnityEngine;
using System.Collections;

public class ZombieFollow : MonoBehaviour {
	public Transform target;//set target from inspector instead of looking in Update
	public float speed = .5f;
	bool facingRight = true;
	Animator anim;
	public Transform sightStart, sightEnd;
	public LayerMask rayCanHit;
	public bool edge;
	//bool chase = false;
	private float distance;
	float startTime;

	void Start()
	{
		anim = GetComponent < Animator > ();
	}
	void Update() {

		Debug.DrawLine(sightStart.position, sightEnd.position, Color.blue); //Visual Representation of Ray Jesus
		edge = Physics2D.Linecast(sightStart.position, sightEnd.position, rayCanHit);
		if (!edge) {
			Flip ();
		}
		/*while (!chase) {
			//pace
			for(int pace = 0; pace<5; pace++){
			rigidbody2D.velocity = new Vector2(-1 * speed, rigidbody2D.velocity.y);
			}
			for(int paceA = 0; paceA<5; paceA++){
			rigidbody2D.velocity = new Vector2(-1 * speed, rigidbody2D.velocity.y);
			}
		}
		*/
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
		if (Vector2.Distance (target.position, transform.position) < 3f) {
			
				//chase = true;

			//transform.position += (target.transform.position - transform.position) * speed * Time.deltaTime;
			if(distance == 0)
			{
				rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
			}
			else if(facingRight){
				rigidbody2D.velocity = new Vector2(-1 * speed, rigidbody2D.velocity.y);
			}
			else
			{
				rigidbody2D.velocity = new Vector2(1 * speed, rigidbody2D.velocity.y);
			}
		}
	}

	void Flip(){
		if((Time.time - startTime) > 1) 
		{
			startTime = Time.time;
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		}
}
