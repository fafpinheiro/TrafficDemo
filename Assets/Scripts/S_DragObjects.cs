using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private float _startPosY;
    private BoardControl _board;
    // Start is called before the first frame update
    void Start()
    {
        _board = GetComponentInParent<BoardControl>();
        _rigidbody = GetComponent<Rigidbody>();

        _startPosY = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        Vector3 newPos = new Vector3(_board.CurrMousePos.x, _startPosY + 100, _board.CurrMousePos.z);
        Vector3 diff = newPos - transform.position;

        _rigidbody.velocity = 10 * diff;
        _rigidbody.rotation = Quaternion.Euler(new Vector3(_rigidbody.velocity.z/5, 0, -_rigidbody.velocity.x/5));
    }
}
