using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CM
{
    public class CameraMovement : MonoBehaviour
    {
        public Camera gameCamera;
        public float cameraMovementSpeed = 5;

        private void Start()
        {
            gameCamera = GetComponent<Camera>();
        }
        public void MoveCamera(Vector3 inputVector, float altitude = 0)
        {
            var movementVector = Quaternion.Euler(0, 30, 0) * inputVector; // to move diagonal shifts
            
            gameCamera.transform.position += (movementVector + new Vector3(0,altitude,0)) * Time.deltaTime * cameraMovementSpeed;
        }
    }
}