using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GridData
{
    Dictionary<Vector3Int, PlacedData> _placedObjects = new();

    public void AddObjectAtCell(Vector3Int gridPosition, Vector2Int objSize, int ID, int placedObjectType)
    {
        List<Vector3Int> positionsToOccupy = CalculateObjOccupiedCells(gridPosition, objSize);

        PlacedData data = new PlacedData(positionsToOccupy, ID, placedObjectType);
        for (int i = 0; i<positionsToOccupy.Count; i++)
        {
            if (!_placedObjects.ContainsKey(positionsToOccupy[i]))
            {
                _placedObjects.Add(positionsToOccupy[i], data);
            }
        }
    }


    //Returns the list of the occupied cells by the added obj in the pos gridPosition(left down corner)
    private List<Vector3Int> CalculateObjOccupiedCells(Vector3Int gridPosition, Vector2Int objSize)
    {
        List<Vector3Int> tmp = new();

        for (int i = 0; i < objSize.x; i++)
        {
            for (int j =0;j< objSize.y; j++)
            {
                tmp.Add(gridPosition + new Vector3Int(i, 0, j));
            }
        }
        return tmp;
    }

    public bool VerifyIfCanPlaceAtCell(Vector3Int gridPosition, Vector2Int cellSize)
    {

        List<Vector3Int> positionsToTest = CalculateObjOccupiedCells(gridPosition, cellSize);
        for(int i =0; i<positionsToTest.Count; i++)
        {
            if (_placedObjects.ContainsKey(positionsToTest[i])) return false;
        }
        return true;
    }

}

public class PlacedData
{
    //All cells occupied by the obj
    public List<Vector3Int> occupiedPositions;

    public int ID { get; private set; }

    public int PlacedObjType { get; private set; }

    public PlacedData(List<Vector3Int> occupiedPositions, int iD, int placedObjType)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjType = placedObjType;
    }
}