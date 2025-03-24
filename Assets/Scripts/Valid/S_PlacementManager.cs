using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject _mouseIndicator;
    [SerializeField] private S_InputManager _inputManager;
    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _defaultCity;


    [SerializeField] private SO_ObjDataBase _dataBase;
    private int _selectedObject = -1;
    private int _uniqueId = 0;

    [SerializeField] private GameObject _gridShader;
    private S_GridData _objDataGrid, _roadDataGrid;
    private List<GameObject> _placedGameObjs = new();

    [SerializeField] private S_PlacementPreview _preview;

    private Vector3Int lastDetectedPos = Vector3Int.zero;

    void Start()
    {
        StopPlacement();
        _objDataGrid = new();
        _roadDataGrid = new();
        PlaceStartingCity();
    }

    //Starts everything related to the placement moment
    public void StartPlacement(int ID)
    {

        StopPlacement();    // to ignore last placement if other obj selected
        for (int i = 0; i< _dataBase.objData.Capacity; i++)
        {
            if(_dataBase.objData[i].ID == ID)
                _selectedObject = i;
        }

        if(_selectedObject < 0)
        {
            Debug.Log($"No ID found{ID}");
            return;
        }

        _gridShader.SetActive(true);
        _preview.ShowPreview(_dataBase.objData[_selectedObject].Prefab, _dataBase.objData[_selectedObject].Size);
        _inputManager.OnMouseClick += PlaceObject; // calls the event
        _inputManager.OnMouseUp += StopPlacement;
    }
    //Stops eveything related to the placement moment
    private void StopPlacement()
    {
        _selectedObject = -1;
        _gridShader.SetActive(false);
        _preview.HidePreview();

        _inputManager.OnMouseClick -= PlaceObject; // calls the event
        _inputManager.OnMouseUp -= StopPlacement;

        lastDetectedPos = Vector3Int.zero;
    }

    //Place the obj on cell
    private void PlaceObject(Vector3Int position)
    {
        if (_inputManager.IsPointerOverUI())
        {
            return;
        }

        var mousePosition = (Vector3)position;
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);//convert mouse position into grid space

        bool placementValid = CheckPlacementValid(gridPosition, _selectedObject);
        if (placementValid == true)
        {
            GameObject newObject = Instantiate(_dataBase.objData[_selectedObject].Prefab);
            newObject.transform.position = _grid.CellToWorld(gridPosition);
            if(_dataBase.objData[_selectedObject].ID == 4)
            {
                var gravityPoint = newObject.GetComponent<S_GravityPoint>();
                // activates the black hole physics
                gravityPoint.enabled = true;
                gravityPoint.SetPlacementeSystem(this);
                newObject.GetComponent<SphereCollider>().enabled = true;
            }
            else
            {
                AddGravityLayer(newObject);
            }

            _placedGameObjs.Add(newObject);
            newObject.GetComponent<S_PlaceableInfo>().uniqueId = _uniqueId;
            _uniqueId++;

            var selectedData = _dataBase.objData[_selectedObject].ID == 0 ? _roadDataGrid : _objDataGrid;
            selectedData.AddObjectAtCell(gridPosition,_dataBase.objData[_selectedObject].Size, _dataBase.objData[_selectedObject].ID, _placedGameObjs.Count-1);
            _preview.UpdatePosition(_grid.CellToWorld(gridPosition), false);
        }
    }

    //Check if can put the obj on cell
    public bool CheckPlacementValid(Vector3Int gridPosition, int slectedObjectType)
    {
        var selectedData = _dataBase.objData[slectedObjectType].ID == 0 ? _roadDataGrid : _objDataGrid;

        // buildings just can be placed on empty spaces
        if (slectedObjectType != 4)
            return _roadDataGrid.VerifyIfCanPlaceAtCell(gridPosition, _dataBase.objData[slectedObjectType].Size)
                && _objDataGrid.VerifyIfCanPlaceAtCell(gridPosition, _dataBase.objData[slectedObjectType].Size);


        return selectedData.VerifyIfCanPlaceAtCell(gridPosition, _dataBase.objData[slectedObjectType].Size);
    }

    //Add the gravity layer to all the objects attached to the gameObj
    private void AddGravityLayer(GameObject gameObj)
    {
        gameObj.layer = LayerMask.NameToLayer("Object");
        for (int i = 0; i<gameObj.transform.childCount; i++) {
            gameObj.transform.GetChild(i)
                .gameObject.layer = LayerMask.NameToLayer("Object");
        }
    }

    // Places all the objects already in the city in the grid cell 
    private void PlaceStartingCity() {
        var placeablesInfo =  _defaultCity.GetComponentsInChildren<S_PlaceableInfo>();

        foreach (var placeableInfo in placeablesInfo)
        {
            var gameObject = placeableInfo.gameObject;
            var gridPosition = _grid.WorldToCell(placeableInfo.transform.position);//convert mouse position into grid space
            _placedGameObjs.Add(gameObject);
            _uniqueId++;
            gameObject.GetComponent<S_PlaceableInfo>().uniqueId = _placedGameObjs.IndexOf(gameObject);

            var selectedData = placeableInfo.iD == 0 ? _roadDataGrid : _objDataGrid;
            selectedData.AddObjectAtCell(gridPosition, placeableInfo.size, placeableInfo.iD, _placedGameObjs.Count - 1);
            
            AddGravityLayer(gameObject);
            
        }
    }

    public void DeleteObject(Vector3 position, Vector2Int objSize, int id, int uniqueId)
    {
        Vector3Int gridPosition = _grid.WorldToCell(position);//convert mouse position into grid space
        var selectedData = id == 0 ? _roadDataGrid : _objDataGrid;
        selectedData.RemoveObjectAtCell(Vector3Int.RoundToInt(position), objSize);
        _placedGameObjs.RemoveAt(uniqueId);
    }

    private void Update()
    {
        // controls preview control
        if (_selectedObject == -1) return; //optimization dont want to move anything if object not clicked

        var mousePosition = _inputManager.RaycastBoard();
        if (mousePosition != null)
        {
            Vector3Int gridPosition = _grid.WorldToCell((Vector3)mousePosition);//convert mouse position into grid space
            if (lastDetectedPos != gridPosition) //optimization, ignore if mouse dont move
            {
                if (_selectedObject != -1)
                {
                    bool placementValid = CheckPlacementValid(gridPosition, _selectedObject);

                    _preview.UpdatePosition(_grid.CellToWorld(gridPosition), placementValid);
                }
                //Moves the mouse indicator
                _mouseIndicator.transform.position = (Vector3)mousePosition + new Vector3(0, 0, 0);
                lastDetectedPos = gridPosition;
            }
        }
    }
}
