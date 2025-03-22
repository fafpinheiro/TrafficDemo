using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject _mouseIndicator/*, _cellIndicator*/;
    [SerializeField] private S_InputManager _inputManager;
    [SerializeField] private BoardControl _board;
    [SerializeField] private Grid _grid;

    private Rigidbody _rigidbody;

    [SerializeField] private SO_ObjDataBase _dataBase;
    private int _selectedObject = -1;

    [SerializeField] private GameObject _gridShader;


    private S_GridData _objDataGrid, _roadDataGrid;

    //private Renderer _previewRenderer;

    private List<GameObject> placedGameObjs = new();

    [SerializeField] private S_PlacementPreview preview;

    private Vector3Int lastDetectedPos = Vector3Int.zero; //talvez apagar
    void Start()
    {
        StopPlacement();
        _objDataGrid = new();
        _roadDataGrid = new();
       // _previewRenderer = _cellIndicator.GetComponentInChildren<Renderer>();
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
      ///  _cellIndicator.SetActive(true);
        preview.ShowPreview(_dataBase.objData[_selectedObject].Prefab, _dataBase.objData[_selectedObject].Size);
        _inputManager.OnMouseClick += PlaceObject; // calls the event
        //_inputManager.OnMouseUpHold += PlaceObject;
        _inputManager.OnMouseUp += StopPlacement;
    }

    
    //Stops eveything related to the placement moment
    private void StopPlacement()
    {
        _selectedObject = -1;
        _gridShader.SetActive(false);
        //_cellIndicator.SetActive(false);
        preview.HidePreview();

        _inputManager.OnMouseClick -= PlaceObject; // calls the event
        //_inputManager.OnMouseUpHold -= PlaceObject;
        _inputManager.OnMouseUp -= StopPlacement;

        lastDetectedPos = Vector3Int.zero;
    }

    //Place the obj on cell
    private void PlaceObject(Vector3Int obj)
    {
        if (_inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = _board.CurrMousePos;
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);//convert mouse position into grid space

        bool placementValid = CheckPlacementValid(gridPosition, _selectedObject);
        if (placementValid == true)
        {
            GameObject newObject = Instantiate(_dataBase.objData[_selectedObject].Prefab);
            newObject.transform.position = _grid.CellToWorld(gridPosition);
            placedGameObjs.Add(newObject);

            S_GridData selectedData = _dataBase.objData[_selectedObject].ID == 0 ? _roadDataGrid : _objDataGrid;
            selectedData.AddObjectAtCell(gridPosition,_dataBase.objData[_selectedObject].Size, _dataBase.objData[_selectedObject].ID, placedGameObjs.Count-1);
            preview.UpdatePosition(_grid.CellToWorld(gridPosition), false);
        }
    }

    //Check if can put the obj on cell
    private bool CheckPlacementValid(Vector3Int gridPosition, int slectedObjectType)
    {
        S_GridData selectedData = _dataBase.objData[slectedObjectType].ID == 0 ? _roadDataGrid : _objDataGrid;

        return selectedData.VerifyIfCanPlaceAtCell(gridPosition, _dataBase.objData[slectedObjectType].Size);
    }

    private void Update()
    {
        //if (_selectedObject == -1) return; // dont want to move anything if object not clicked

        Vector3 mousePosition = _board.CurrMousePos;//new Vector3(_board.CurrMousePos.x,  10, _board.CurrMousePos.z);

        //   Vector3 diff = mousePosition - transform.position;
        //
        //   _rigidbody.velocity = 10 * diff;
        //   _rigidbody.rotation = Quaternion.Euler(new Vector3(_rigidbody.velocity.z / 5, 0, -_rigidbody.velocity.x / 5));
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);//convert mouse position into grid space
        if (lastDetectedPos != gridPosition)
        {
            if (_selectedObject != -1)
            {
                bool placementValid = CheckPlacementValid(gridPosition, _selectedObject);
                //_previewRenderer.material.color = placementValid ? Color.white : Color.red;

                preview.UpdatePosition(_grid.CellToWorld(gridPosition), placementValid);
            }
            _mouseIndicator.transform.position = mousePosition + new Vector3(0, 0, 0);
            //_cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
            lastDetectedPos = gridPosition;
        }
    }
}
