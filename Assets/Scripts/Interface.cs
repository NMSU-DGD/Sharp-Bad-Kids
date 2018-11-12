using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Interface : MonoBehaviour {

    GameObject GameContoller;
    Text ActionPoints;
    Text Turn;
    GameObject[] Inventory;

	// Use this for initialization
	void Start () {
        GameContoller = GameObject.FindGameObjectWithTag("GameController");
        ActionPoints = GameObject.Find("Action Points").GetComponentInChildren<Text>();
        Turn = GameObject.Find("Turn").GetComponentInChildren<Text>();
        Inventory = GameObject.FindGameObjectsWithTag("Inventory").OrderBy(go => go.name).ToArray();
    }

    // Update is called once per frame
    void Update() {
        ActionPoints.text = "AP : " + GameContoller.GetComponent<Turn>().actionPoint;
        Turn.text = "Turn : " + (GameContoller.GetComponent<Turn>().turn+1);

        GameObject[] players = GameContoller.GetComponent<Turn>().players;
        for (int i = 0; i < Inventory.Length; i++)
        {
            int[] resource = players[i].GetComponent<Player>().resource;
            Inventory[i].GetComponentInChildren<Text>().text = "Player " + (i+1) + 
                "\nChicken : " + resource[0] + "\nEgg : " + resource[1] + "\nMushroom : " +
                resource[2] + "\nOrange : " + resource[3];
        }
	}
}
