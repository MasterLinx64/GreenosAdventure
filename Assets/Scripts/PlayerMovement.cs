using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public Transform Player;
	public float MovementSpeed;
	public Vector2 vel;
	public float rayHeight;
	public float jumpHeight;
	Animator anim;
    public AudioClip JumpClip;
 
	private bool facingRight;
	private bool canFlip;
	public bool test;
	public bool HorizontalColition;
    AudioSource playerAudio;
        bool isGrounded(){
		RaycastHit2D ray = Physics2D.Raycast(transform.position, -Vector2.up, rayHeight);
		//if (ray != null)
			//transform.rotation = ray.collider.transform.rotation;
 

		return (ray.collider != null) ? true : false;
	}
    public bool isJumping;
	bool isWalled() {
		Vector2 direction = transform.TransformDirection(Vector2.right) * rayHeight;
		Debug.DrawRay(transform.position, direction, Color.red);
		RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.right, rayHeight);

		return (ray.collider != null) ? true : false;
	}

    public void SetJumpHeight(float j)
    {
        this.jumpHeight = j;
    }
	void Start () {
        playerAudio = GetComponent<AudioSource>();
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
			    anim.SetBool ("PlayerJumping", false);
				anim.SetBool ("PlayerWalking", true);
		} else if (!isGrounded()) {
			anim.SetBool ("PlayerJumping", true);
		} else {
			anim.SetBool ("PlayerJumping", false);
			anim.SetBool ("PlayerWalking", false);
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

        if (Input.GetButtton("Jump") && isGrounded()) {
            playerAudio.Play();
        }
        if (Player.position.y < -25f)
        {
            Application.LoadLevel(0);  
        }
		vel = GetComponent<Rigidbody2D>().velocity;
		GetComponent<Rigidbody2D>().velocity = new Vector2
		(
	/*X	*/	Input.GetAxis("Horizontal") * MovementSpeed,
	/*Y*/	 (isGrounded() && Input.GetButton("Jump")) ? jumpHeight : GetComponent<Rigidbody2D>().velocity.y
		);

	}
}
