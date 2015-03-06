using UnityEngine;
using System.Collections;

public class CharacterKontroller : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool facingRight = true;

	Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.001f;
	public LayerMask whatIsGround;
	public LayerMask rayCanHit;
	public float jumpForce = 50f;
	public Transform sightStart, sightEnd;
	public bool notTraversable;

	public bool onLadder = false;

	// Move variable
	public float move = 0F;

	// Custom key bindings
	public KeyCode keyRight = KeyCode.D,
				   keyLeft = KeyCode.A,
				   keyJump = KeyCode.Space;

	// Use this for initialization
	void Start () {
		anim = GetComponent < Animator > ();
	}
	
	//  FixedUpdate is called once per frame
	void FixedUpdate () {
		// Jumping
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
		
		anim.SetFloat ("Speed", Mathf.Abs (move));

		if (notTraversable || onLadder)
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		else
			rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	
	}

	void Update()
	{

		Debug.DrawLine(sightStart.position, sightEnd.position, Color.blue); //Visual Representation of Ray Jesus
		notTraversable = Physics2D.Linecast(sightStart.position, sightEnd.position, rayCanHit);

		//Horizontal Movement
		
		if (Input.GetKey (keyRight) && Input.GetKey (keyLeft)) {
			move = 0F; // Reset velocity if both keys are down
		} else if (Input.GetKey (keyRight)) {
			if (move < 0F) move = 0F; // Reset velocity
			if (move < 1F) move += 0.15F; // Small acceleration
		} else if (Input.GetKey (keyLeft)) {
			if (move > 0F) move = 0F;
			if (move > -1F) move -= 0.15F;
		} else {
			if (Mathf.Abs(move) - 0.15F > 0) // Slow down
				move += (move > 0F ? -0.15F : 0.15F);
			else
				move = 0F; // Reset velocity if sufficiently slow
		}

		if (grounded && Input.GetKeyDown (keyJump) && rigidbody2D.velocity.y < 0) 
		{
			anim.SetBool ("Ground", false);
			rigidbody2D.AddForce (new Vector2 (0, (-50*rigidbody2D.velocity.y)));

		}


		if (grounded && Input.GetKeyDown(keyJump))
		{
			anim.SetBool("Ground",false);
			rigidbody2D.AddForce (new Vector2(0, jumpForce));
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}