using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ObjUiRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 50f; // Speed of rotation
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates the object
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}
