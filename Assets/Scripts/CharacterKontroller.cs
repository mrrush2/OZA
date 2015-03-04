using UnityEngine;
using System.Collections;

public class CharacterKontroller : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.001f;
	public LayerMask whatIsGround;
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


		//Horizontal Movement

		if (Input.GetKey (keyRight) && Input.GetKey (keyLeft)) {
			move = 0F;
		} else if (Input.GetKey (keyRight)) {
			move = 1F;
		} else if (Input.GetKey (keyLeft)) {
			move = -1F;
		} else  if (grounded){
			if (move > 0.1 || move < -0.1)
				move += (move > 0 ? -0.1F : 0.1F);
			else
				move = 0F;
		}

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
		notTraversable = Physics2D.Linecast(sightStart.position, sightEnd.position, whatIsGround);


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