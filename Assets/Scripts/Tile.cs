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
    public int cooldown;
    public int cooling;

    private void Start()
    {
        turn = GameObject.FindGameObjectWithTag("GameController");
        hasResource = true;
        occupied = false;
        cooling = cooldown;
    }

    void Update()
    {
        if(cooling == 0) {
            hasResource = true;
            cooling = cooldown;
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!occupied)
        {
            turn.GetComponent<Turn>().Move(this);
        }
    }
}