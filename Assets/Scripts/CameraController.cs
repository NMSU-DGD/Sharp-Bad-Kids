using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Camera Movement Speed
    public float speed;

    // Stop When it hits the Max number
    private float maxIn    = 2.0f;
    private float maxOut   = 11.0f;
    private float maxUp    = 8.0f;
    private float maxDown  = -6.0f;
    private float maxLeft  = -8.5f;
    private float maxRight = 10.0f;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W) && transform.position.z < maxUp)
        {
            transform.position += new Vector3(0, 0, 1 * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.z > maxDown)
        {
            transform.position += new Vector3(0, 0, -1 * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x > maxLeft)
        {
            transform.position += new Vector3(-1 * Time.deltaTime * speed, 0, 0);
        } 
        if (Input.GetKey(KeyCode.D) && transform.position.x < maxRight)
        {
            transform.position += new Vector3(1 * Time.deltaTime * speed, 0, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < maxOut) // forward
 {
            transform.position += new Vector3(0, 5 * Time.deltaTime * speed, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > maxIn) // back
 {
            transform.position += new Vector3(0, -5 * Time.deltaTime * speed, 0);
        }
    }
}
