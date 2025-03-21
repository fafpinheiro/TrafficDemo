using CM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameManager : MonoBehaviour
{

    public CameraMovement cameraMovement;

    public S_InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
       // inputManager.OnMouseClick += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int position)
    {
        Debug.Log(position);
    }

    // Update is called once per frame
    void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMoveVec.x, 0 ,inputManager.CameraMoveVec.y));
        
    }
}
