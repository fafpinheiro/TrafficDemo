using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GridManager : MonoBehaviour
{

    [SerializeField] private int _width, _height;
    [SerializeField] private S_Tile _tilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid()
    {
        for(int x = 0; x < _width; x++)
        {
            for(int z = 0; z < _height; z++)
            {
                Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x*30, 1, z*30), rotation);

                spawnedTile.name = $"Tile {x}{z}";
            }
        }
    }
}
