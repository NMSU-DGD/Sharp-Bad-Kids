using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour {

    public GameObject[] players;
    public int turn;
    public int actionPoint;

    private GameObject Camera;

	// Use this for initialization
	void Start ()
    {
        turn = 0;
        actionPoint = 10;
        players = GameObject.FindGameObjectsWithTag("Player");
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        Camera.GetComponent<CameraController>().MoveTo(players[0]);
    }
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("Indicator").GetComponent<Transform>().position = players[turn].transform.position + new Vector3(0, 1.8f, 0);
    }

    public void nextTurn()
    {
        turn = (turn + 1) % 4;

        // each round calculate Revenue & cooldown
        if(turn == 0) {
            // Check Cooldowns
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
            foreach (GameObject tile in tiles)
            {
                Tile a = tile.GetComponent<Tile>();

                if(!a.hasResource && a.cooling > 0) {
                    a.cooling--;
                }
            }

            // Update Revenue
            foreach(GameObject player in players)
            {
                Player p = player.GetComponent<Player>();

                p.coins += p.resource[4] * 10;

                if(p.coins >= 100) {
                    gameover(p);
                }
            }
        }

        actionPoint = 10;
        Debug.Log(turn);
        Camera.GetComponent<CameraController>().MoveTo(players[turn]);
    }

    public void Move(Tile dest)
    {
        Vector2 current = players[turn].GetComponent<Player>().current;
        int dist = GetTileDistance((int)current.x, (int)current.y, (int)dest.pos.x, (int)dest.pos.y);
        if ( dist <= actionPoint/2)
        {
            players[turn].GetComponent<Player>().MoveTo(dest.pos);
            players[turn].GetComponent<Player>().current = dest.pos;
            actionPoint -= dist * 2;
        }
    }

    public void Collect()
    {
        Player p = players[turn].GetComponent<Player>();

        if (actionPoint >= 4 && p.currTile.GetComponent<Tile>().hasResource)
        {
            switch (p.currTile.name)
            {
                case "Cooling(Clone)":
                    p.currTile.hasResource = false;
                    p.currTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                    p.resource[0]++;
                    break;

                case "Power(Clone)":
                    p.currTile.hasResource = false;
                    p.currTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                    p.resource[1]++;
                    break;

                case "Graphics(Clone)":
                    p.currTile.hasResource = false;
                    p.currTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                    p.resource[2]++;
                    break;

                case "Monitor(Clone)":
                    p.currTile.hasResource = false;
                    p.currTile.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                    p.resource[3]++;
                    break;
            }

            actionPoint -= 4;
        }
    }

    public void Build()
    {
        int[] resource = players[turn].GetComponent<Player>().resource;

        if(actionPoint >= 6)
        {
            // 3 chickens & 2 eggs & 2 mushrooms & 2 oranges?
            // 1 for each for Demo...
            if(resource[0] > 0 && resource[1] > 0 && resource[2] > 0 && resource[3] > 0)
            {
                for(int i = 0; i < 4; i++)
                {
                    resource[i]--;
                }

                resource[4]++;

                actionPoint -= 6;
            }
        }
    }

    public void gameover(Player p)
    {
        GameObject.FindGameObjectWithTag("Finish").GetComponent<Canvas>().enabled = true;
        GameObject.Find("winner").GetComponent<Text>().text = p.name + " is the winner!";
    }

    int GetTileDistance(int aX1, int aY1, int aX2, int aY2)
    {
        int dx = aX2 - aX1;     // signed deltas
        int dy = aY2 - aY1;
        int x = Mathf.Abs(dx);  // absolute deltas
        int y = Mathf.Abs(dy);
        // special case if we start on an odd row or if we move into negative x direction
        if ((dx < 0) ^ ((aY1 & 1) == 1))
            x = Mathf.Max(0, x - (y + 1) / 2);
        else
            x = Mathf.Max(0, x - (y) / 2);
        return x + y;
    }
}
