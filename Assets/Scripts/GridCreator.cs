using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private int gridWidth, gridHeight;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Destructible destructiblePrefab;
    [SerializeField] private Transform cam;
    [SerializeField] private List<PlayerController> players;

    private List<Tile> list_tiles;

    private List<int> list_protected_tiles;


    void Start()
    {
        list_tiles = new List<Tile>(); 
        grid = new Grid(gridWidth, gridHeight);
        //Set les tiles à ne pas utiliser pour placer des Destructibles. "L" aux coins de spawns des joueurs. 
        list_protected_tiles = new List<int>{0, 1, gridHeight, (gridWidth * gridHeight) - 1 - gridHeight , (gridWidth * gridHeight) - 2, (gridWidth * gridHeight) - 1};
        CreateGrid();
        CreateObstacles();
        CreateDestructibles();
        PlacePlayers(); 

    }

    public void CreateGrid()
    {
        var tilePrefabWidth = tilePrefab.GetComponent<Renderer>().bounds.size.x;
        var tilePrefabHeight = tilePrefab.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                var tile = Instantiate(tilePrefab, new Vector3(i * tilePrefabWidth, 0, j * tilePrefabHeight), Quaternion.identity);
                tile.SetPositionXY(i, j); 
                tile.name = $"Tile {i} {j}";
                list_tiles.Add(tile); 
            }
        }
    }

    public void CreateObstacles()
    {
        for (int i = 1; i < gridWidth - 1; i++)
        {
            for (int j = 1; j < gridHeight - 1; j++)
            {
                if(i%6 == 0 && j%4 == 0)
                {
                    var indexTile = i * gridHeight + j;
                    var obstaclePosition = new Vector3(list_tiles[indexTile].transform.position.x, list_tiles[indexTile].transform.position.y + 0.7f, list_tiles[indexTile].transform.position.z); 
                    var obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
                    list_tiles[indexTile].SetAvailable(false); 
                }
            }
        }

    }

    public void CreateDestructibles()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                var indexTile = i * gridHeight + j;
                var shouldPlaceDestructible = Random.Range(0, 5) == 1; //Return true or false 1 fois sur {Range}
                if (list_tiles[indexTile].GetIsAvailable() && shouldPlaceDestructible && !list_protected_tiles.Contains(indexTile))
                {
                    var destructiblePosition = new Vector3(list_tiles[indexTile].transform.position.x, list_tiles[indexTile].transform.position.y + 0.7f, list_tiles[indexTile].transform.position.z);
                    var destructible = Instantiate(destructiblePrefab, destructiblePosition, Quaternion.identity);
                    list_tiles[indexTile].SetAvailable(false);
                    destructible.SetTileOn(list_tiles[indexTile]); // Set la tile comme paramètre de l'objet Destructible 
                }
            }
        }   
    }


    public void PlacePlayers()
    {
        players[0].transform.position = list_tiles[0].transform.position + Vector3.up;
        players[0].originalPosition = players[0].transform.position; 
        players[1].transform.position = list_tiles[list_tiles.Count - 1].transform.position + Vector3.up;
        players[1].originalPosition = players[1].transform.position;
    }
}
