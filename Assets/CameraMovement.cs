using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public Transform Player;
	// Use this for initialization
	void Wake () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (Player.position.x, 46.5f, -10);

        if (Player.position.y < 2f && Player.position.x > 520f)
        {
            transform.position = new Vector3(Player.position.x, 10f, -10);
        } else if (Player.position.x > 550f) {
            transform.position = new Vector3(Player.position.x, 10f, -10);
        }

        if (Player.position.x > 652f)
        {
            Application.LoadLevel("Level 2"); 
            
        }
	} 
}
