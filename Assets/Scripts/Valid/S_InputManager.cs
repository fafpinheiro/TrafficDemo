using CM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_InputManager : MonoBehaviour
{

    public Action<Vector3Int> OnMouseClick, OnMouseHold;
    public Action OnMouseUp;
    public Action<Vector3Int> OnMouseUpHold;

    private Vector3 _cameraMoveVec;
    private float _altitude;
    [SerializeField]private Camera _mainCamera;
    [SerializeField]private float _cameraMovingSpeed;
    public CameraMovement cameraMovement;


    public LayerMask boardMask; // to check if the click was legal, dont need to call events otherwise


    public Vector2 CameraMoveVec //Propertie
    {
        get { return _cameraMoveVec; }
    }

    public float Altitude //Propertie
    {
        get { return _altitude; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Capture inputs
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
       // CheckClickUpHoldEvent();
        CheckArrowInput();
    }

    // Raycasts a ray from the cam to detect the pointed cell
    public Vector3Int? RaycastBoard()
    {
        RaycastHit hit;
      
        // Raycast from the camera
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
      
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, boardMask))
        {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }
      
        return null;
    }

    // Check for array keys input (movement)
    private void CheckArrowInput()
    {
        // Increase altitude when Spacebar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            _altitude = 1f;
        }

        // Decrease altitude when Shift is pressed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _altitude = -1f;
        }
        _cameraMoveVec = new Vector2(Input.GetAxis("Horizontal")/*, upDown*/, Input.GetAxis("Vertical")); // used to ad AND <- ->

        cameraMovement.MoveCamera(new Vector3(CameraMoveVec.x, 0, CameraMoveVec.y),Altitude);

        _altitude = 0;
    }

    // Check if we are holding the click
    private void CheckClickHoldEvent()
    {
        if(Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false) // check that we are not pressing over a a object UI
        {
            var position =  RaycastBoard();

            if(position != null)
            {
                OnMouseHold?.Invoke(position.Value); // make everybody that is listenig OnMouseHold event that the mouse was moved  // the ? gives the oportunity to not calling the event if nothing is listening OnMouseHold
            }
        }
    }
    // Check if we are holding the click
    private void CheckClickUpHoldEvent()
    {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false) // check that we are not pressing over a a object UI
        {
            var position = RaycastBoard();

            if (position != null)
            {
                OnMouseUpHold?.Invoke(position.Value); // make everybody that is listenig OnMouseHold event that the mouse was moved  // the ? gives the oportunity to not calling the event if nothing is listening OnMouseHold
            }
        }
    }

    // Check if we let go the clicl
    private void CheckClickUpEvent()
    {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false) // check that we are not pressing over a a object UI
        {
            OnMouseUp?.Invoke();
        }
    }

    // Check if we tried to click
    private void CheckClickDownEvent()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) // check that we are not pressing over a a object UI
        {
            var position = RaycastBoard();

            if (position != null)
            {
                OnMouseClick?.Invoke(position.Value); // make everybody that is listenig OnMouseHold event that the mouse was moved  // the ? gives the oportunity to not calling the event if nothing is listening OnMouseHold
            }
        }
    }

    // Check if mouse is hover ui
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
}
