using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float movementSpeed, rayHeight, jumpHeight, rayLength;
	public bool test, grounded;
	private float a = 0.15f;

	bool isGrounded()
	{
		RaycastHit2D ray = Physics2D.BoxCast (transform.position, new Vector2 (0.1f, 0.1f), 0, -transform.up, rayHeight);
		return (ray.collider !=null) ? true:false;
	}

	bool hitsRight()
	{
		RaycastHit2D ray = Physics2D.BoxCast (transform.position, new Vector2 (a, a), 0, transform.right, rayLength);
		return (ray.collider !=null) ? true:false;
	}

	bool hitsLeft()
	{
		RaycastHit2D ray = Physics2D.BoxCast (transform.position, new Vector2 (a, a), 0, -transform.right, rayLength);
		return (ray.collider !=null) ? true:false;
	}

	void Update () 
	{
		float moving = Input.GetAxis("Horizontal");
		test =  hitsRight();
		grounded = isGrounded();
		GetComponent<Rigidbody2D>().velocity = new Vector2 
			( 
			 	(!hitsRight() && !hitsLeft() || isGrounded() ) ? moving * movementSpeed : GetComponent<Rigidbody2D>().velocity.x,
				( isGrounded() && Input.GetKey(KeyCode.Space)) ? jumpHeight : GetComponent<Rigidbody2D>().velocity.y
			);
	}
	
}