using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_DraggableWindow : MonoBehaviour, IDragHandler
{
    [SerializeField]private Canvas canvas;

    [SerializeField] private RectTransform _rectTransform;
    // Start is called before the first frame update
    void Start()
    {
       // _rectTransform = GetComponent<RectTransform>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}
