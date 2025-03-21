using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GridManager : MonoBehaviour
{

    [SerializeField] private int _width, _height, _cellInterval;
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
        for(int x = 1; x < _width; x++)
        {
            for(int z = 1; z < _height; z++)
            {
                Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                float size = _tilePrefab.transform.localScale.x;
                var spawnedTile = Instantiate(_tilePrefab, new Vector3((x* (size + _cellInterval))  + (size / 2), 0.01f, (z* (size + _cellInterval))+ (size / 2)), rotation);

                spawnedTile.name = $"Tile {x}{z}";
            }
        }
    }
}
