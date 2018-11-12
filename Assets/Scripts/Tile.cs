using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler {

    // Tile Position
    public Vector2 pos;
    public GameObject turn;
    public bool hasResource;
    public bool occupied;

    private void Start()
    {
        turn = GameObject.FindGameObjectWithTag("GameController");
        hasResource = true;
        occupied = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!occupied)
        {
            turn.GetComponent<Turn>().Move(this);
        }
    }
}