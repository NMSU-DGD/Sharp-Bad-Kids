using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Camera Movement Speed
    public float speed;

    // Stop When it hits the Max number
    private float maxIn    = 4.5f;
    private float maxOut   = 11.0f;
    private float maxUp    = 8.0f;
    private float maxDown  = -6.0f;
    private float maxLeft  = -8.5f;
    private float maxRight = 10.0f;
    private bool turnChange = false;
    private Vector3 destination;
	
	// Update is called once per frame
	void Update () {
        // Camera Up
        if (Input.GetKey(KeyCode.W) && transform.position.z < maxUp)
        {
            transform.position += new Vector3(0, 0, 1 * Time.deltaTime * speed);
        }
        // Camera Down
        if (Input.GetKey(KeyCode.S) && transform.position.z > maxDown)
        {
            transform.position += new Vector3(0, 0, -1 * Time.deltaTime * speed);
        }
        // Camera Left
        if (Input.GetKey(KeyCode.A) && transform.position.x > maxLeft)
        {
            transform.position += new Vector3(-1 * Time.deltaTime * speed, 0, 0);
        } 
        // Camera Right
        if (Input.GetKey(KeyCode.D) && transform.position.x < maxRight)
        {
            transform.position += new Vector3(1 * Time.deltaTime * speed, 0, 0);
        }
        // Camera Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < maxOut) // forward
        {
            transform.position += new Vector3(0, 5 * Time.deltaTime * speed, 0);
        }
        // Camera Zoom In
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > maxIn) // back
        {
            transform.position += new Vector3(0, -5 * Time.deltaTime * speed, 0);
        }

        // Camera move
        if (turnChange) {
            transform.position = Vector3.Lerp(transform.position, destination, 0.1f);
        }

        // Toggle turn change once it gets close
        if (Vector3.Distance(transform.position, destination) < 0.1f) {
            turnChange = false;
        }
    }

    // Move Camera to the player
    public void MoveTo (GameObject player) {
        // toggle turnChange
        turnChange = true;
        // Player position
        Vector3 pos = player.transform.position;
        // Set destination
        destination = new Vector3( pos.x, 4.5f, pos.z - 2 );
    }
}
