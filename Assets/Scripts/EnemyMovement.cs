using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	
	public float MovementSpeed;
	public Vector2 vel;
	public float rayHeight;
	public float jumpHeight;
	Animator anim;
	private bool facingRight;
	private bool canFlip;
	public bool test;
	public bool HorizontalColition;
	bool isGrounded(){
		RaycastHit2D ray = Physics2D.Raycast(transform.position, -Vector2.up, rayHeight);
		//if (ray != null)
		//transform.rotation = ray.collider.transform.rotation;
		
		
		return (ray.collider != null) ? true : false;
	}
	bool isWalled() {
		Vector2 direction = transform.TransformDirection(Vector2.right) * rayHeight;
		Debug.DrawRay(transform.position, direction, Color.red);
		RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.right, rayHeight);
		
		return (ray.collider != null) ? true : false;
	}
	
	void Start () {
		anim = GetComponent <Animator>();
	}
	void Flip()
	{
		
		// Switch the way the player is labelled as facing
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		canFlip = false;
	}
	
	// Update is called once per frame
	void Update () {
		HorizontalColition = isWalled();
		test = isGrounded();
		float isMoving = Input.GetAxis ("Horizontal");
		if (isMoving != 0 && isGrounded()) {
			anim.SetBool ("EnemyWalking", true);
		}
		else {
			anim.SetBool ("EnemyWalking", false);
		}
		if (isMoving != 0 && canFlip ) {
			if (isMoving > 0 && facingRight)
				Flip();
			else if (isMoving < 0 && !facingRight)
				Flip ();
			canFlip = false;
		}else {
			canFlip = true;
		}
		
		
		vel = GetComponent<Rigidbody2D>().velocity;
		GetComponent<Rigidbody2D>().velocity = new Vector2
			(
				/*X	*/	Input.GetAxis("Horizontal") * MovementSpeed,
				/*Y*/	 (isGrounded() && Input.GetKey(KeyCode.Space)) ? jumpHeight : GetComponent<Rigidbody2D>().velocity.y
				);
		
	}
}