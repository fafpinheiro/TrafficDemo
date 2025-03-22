using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControl : MonoBehaviour
{
    [HideInInspector] public Vector3 CurrMousePos;
    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach(var hit in hits)
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Board"))
                continue;

            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 500, Color.green);
            CurrMousePos = hit.point;   // Inpact point with the table

            break;
        }
    }
}
