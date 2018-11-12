using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public Vector2 current;
    public Tile currTile;
    public int[] resource = new int[4];
    
    private Vector3 destination;
    private GameObject Grid;
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        destination = transform.position;
        Grid = GameObject.FindGameObjectWithTag("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, destination) > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        } else
        {
            anim.SetBool("isMoving", false);
        }

        currTile.occupied = true;
	}

    // Check where the character is located
    void OnTriggerEnter(Collider other)
    {
        currTile = other.gameObject.GetComponent<Tile>();
    }

    void OnTriggerExit(Collider other)
    {
        other.GetComponent<Tile>().occupied = false;
    }

    // Move to the clicked Tile
    public void MoveTo(Vector2 destination)
    {
        this.destination = Grid.GetComponent<GridManager>().calcWorldCoord(destination);
        this.destination.y += 0.1f;
        transform.LookAt(this.destination);
        anim.SetBool("isMoving", true);
    }
}
