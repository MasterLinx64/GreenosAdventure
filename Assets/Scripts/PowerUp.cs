using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public Animator anim;
    public Transform Player;
    [SerializeField]
    private float timer;
    private float reset = 0;
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.tag == "Player")
        {
            anim.SetBool("PlayerCollition",  true);
            
            Player.SendMessage("SetJumpHeight", 500f);
            reset = Time.time + timer;
        }
        else
        {


        }
    }
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (reset < Time.time)
        {

            anim.SetBool("PlayerCollition", false);

            Player.SendMessage("SetJumpHeight", 105f);
        }
	}
}
