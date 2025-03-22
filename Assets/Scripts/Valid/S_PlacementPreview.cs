using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlacementPreview : MonoBehaviour
{
    [SerializeField]private float _previewYOffSet = 0.06f;

    [SerializeField] private GameObject _cellIndicator;
    private GameObject _previewObj;

    [SerializeField] private Material _previewMaterialPrefab;
    private Material _previewMaterialInstance; // created in order to edit just the instance and not the original

    private Renderer _cellIndicatorRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _previewMaterialInstance = new Material(_previewMaterialPrefab);
        _cellIndicator.SetActive(false);
        _cellIndicatorRenderer = _cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void ShowPreview(GameObject prefab, Vector2Int size)
    {
        //obj preview
        _previewObj = Instantiate(prefab);
        Rigidbody[] _previewRBs = _previewObj.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in _previewRBs)
        {
            rb.isKinematic = false;
            rb.detectCollisions = false;
            rb.useGravity = false;
        }
        PreparePreview(_previewObj);
        //cell indicator preview
        PrepareIndicator(size);
        _cellIndicator.SetActive(true);
    }

    //Adjust the indicator size to the obj size
    private void PrepareIndicator(Vector2Int size)
    {
        if(size.x > 0 || size.y > 0)
        {
            _cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            _cellIndicatorRenderer.material.mainTextureScale = size ;
        }
    }

    //Set obj mats to the tranparency shader
    private void PreparePreview(GameObject previewObj)
    {
        Renderer[] renderers = previewObj.GetComponentsInChildren<Renderer>();
        for(int i = 0; i< renderers.Length; i++)
        {
            Material[] materials = renderers[i].materials;
            for(int j = 0; j<materials.Length; j++)
            {
                materials[i] = _previewMaterialInstance;
            }
            renderers[i].materials = materials;
        }
    }
    public void HidePreview()
    {
        _cellIndicator.SetActive(false);
        Destroy(_previewObj);
    }

    public void UpdatePosition(Vector3 pos, bool valid)
    {
        MovePreview(pos);
        ApplyState(valid);
    }

    private void ApplyState(bool valid)
    {
        Color color = valid ? Color.white : Color.red;
        color.a = 0.5f;

        _cellIndicatorRenderer.material.color = color;
        _previewMaterialInstance.color = color;
    }

    private void MovePreview(Vector3 pos)
    {
        _cellIndicator.transform.position = pos;
        _previewObj.transform.position = new Vector3(pos.x, pos.y + _previewYOffSet, pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
