using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour
{
    //next two variables can also be instantiated using unity editor
    public int gridWidthInHexes = 10;
    public int gridHeightInHexes = 10;

    //character prefab
    public GameObject Character;
    protected GameObject[] Characters =  new GameObject[4];

    //Hexagon array
    public GameObject[] Hex = new GameObject[4];

    //Hexagon tile width and height in game world
    private float hexWidth;
    private float hexHeight;

    //Method to initialise Hexagon width and height
    void setSizes()
    {
        //renderer component attached to the Hex prefab is used to get the current width and height
        hexWidth = Hex[0].GetComponent<Renderer>().bounds.size.x+0.1f;
        hexHeight = Hex[0].GetComponent<Renderer>().bounds.size.z+0.1f;
    }

    //Method to calculate the position of the first hexagon tile
    //The center of the hex grid is (0,0,0)
    Vector3 calcInitPos()
    {
        Vector3 initPos;
        //the initial position will be in the left upper corner
        initPos = new Vector3(-hexWidth * gridWidthInHexes / 2f + hexWidth / 2, 0,
            gridHeightInHexes / 2f * hexHeight - hexHeight / 2);

        return initPos;
    }

    //method used to convert hex grid coordinates to game world coordinates
    public Vector3 calcWorldCoord(Vector2 gridPos)
    {
        //Position of the first hex tile
        Vector3 initPos = calcInitPos();
        //Every second row is offset by half of the tile width
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = initPos.x + offset + gridPos.x * hexWidth;
        //Every new line is offset in z direction by 3/4 of the hexagon height
        float z = initPos.z - gridPos.y * hexHeight * 0.75f;
        return new Vector3(x, 0, z);
    }

    //Finally the method which initialises and positions all the tiles
    void createGrid()
    {
        //Game object which is the parent of all the hex tiles
        GameObject hexGridGO = new GameObject("HexGrid");

        for (float y = 0; y < gridHeightInHexes; y++)
        {
            for (float x = 0; x < gridWidthInHexes; x++)
            {
                //GameObject assigned to Hex public variable is cloned
                GameObject hex = (GameObject)Instantiate(Hex[Random.Range(0, 4)]);
                //Current position in grid
                Vector2 gridPos = new Vector2(x, y);
                hex.GetComponent<Tile>().pos = gridPos;
                hex.transform.position = calcWorldCoord(gridPos);
                hex.transform.parent = hexGridGO.transform;
            }
        }
    }

    //Character Creation
    void createCharacters()
    {
        for (int i = 0; i < 4; i++)
        {
            // instantiate character
            Characters[i] = (GameObject)Instantiate(Character);
            // coordinates
            if (i == 0)
            {
                Characters[i].transform.position = calcWorldCoord(new Vector2(0, 0)) + new Vector3(0, 0.1f, 0);
                Characters[i].GetComponent<Player>().current = new Vector2(0, 0);
                Characters[i].GetComponent<Player>().name = "Player 1";
            }
            else if (i == 1)
            {
                Characters[i].transform.position = calcWorldCoord(new Vector2(0, 9)) + new Vector3(0, 0.1f, 0);
                Characters[i].GetComponent<Player>().current = new Vector2(0, 9);
            	Characters[i].GetComponent<Player>().name = "Player 2";
            }
            else if (i == 2)
            {
                Characters[i].transform.position = calcWorldCoord(new Vector2(9, 0)) + new Vector3(0, 0.1f, 0);
                Characters[i].GetComponent<Player>().current = new Vector2(9, 0);
            	Characters[i].GetComponent<Player>().name = "Player 3";
            }
            else
            {
                Characters[i].transform.position = calcWorldCoord(new Vector2(9, 9)) + new Vector3(0, 0.1f, 0);
                Characters[i].GetComponent<Player>().current = new Vector2(9, 9);
            	Characters[i].GetComponent<Player>().name = "Player 4";
            }
        }
    }

    //The grid should be generated on game start
    void Awake()
    {
        setSizes();
        createGrid();
        createCharacters();
    }
}