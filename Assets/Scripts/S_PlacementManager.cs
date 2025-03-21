using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject _mouseIndicator, _cellIndicator;
    [SerializeField] private S_InputManager _inputManager;
    [SerializeField] private BoardControl _board;
    [SerializeField] private Grid _grid;

    private Rigidbody _rigidbody;

    [SerializeField] private SO_ObjDataBase _dataBase;
    private int _selectedObject = -1;

    [SerializeField] private GameObject _gridShader;

    void Start()
    {
        StopPlacement();
    }

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
        _cellIndicator.SetActive(true);
        _inputManager.OnMouseClick += PlaceObject; // calls the event
        _inputManager.OnMouseUp += StopPlacement;
    }

    

    private void StopPlacement()
    {
        _selectedObject = -1;
        _gridShader.SetActive(false);
        _cellIndicator.SetActive(false);
        _inputManager.OnMouseClick -= PlaceObject; // calls the event
        _inputManager.OnMouseUp -= StopPlacement;
    }

    private void PlaceObject(Vector3Int obj)
    {
        if (_inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = _board.CurrMousePos;
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);//convert mouse position into grid space
        GameObject newObject = Instantiate(_dataBase.objData[_selectedObject].Prefab);
        newObject.transform.position = _grid.CellToWorld(gridPosition);
    }


    private void Update()
    {
     //   if (_selectedObject == -1) return; // dont want to move anything if object not clicked

        Vector3 mousePosition = _board.CurrMousePos;//new Vector3(_board.CurrMousePos.x,  10, _board.CurrMousePos.z);

        //   Vector3 diff = mousePosition - transform.position;
        //
        //   _rigidbody.velocity = 10 * diff;
        //   _rigidbody.rotation = Quaternion.Euler(new Vector3(_rigidbody.velocity.z / 5, 0, -_rigidbody.velocity.x / 5));
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);//convert mouse position into grid space

        _mouseIndicator.transform.position = mousePosition + new Vector3(0, 0, 0);
        _cellIndicator.transform.position = _grid.CellToWorld(gridPosition);

    }
}
