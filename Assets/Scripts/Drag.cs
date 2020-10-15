using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public float xMin = -383f;
    public float xMax = 194f;
    public float yMin = -168f;
    public float yMax = 168f;

    public RectTransform testing;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       // Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if(rectTransform.rect.Overlaps(testing.rect))
        {
            Debug.Log("OVERLAPS");
        }
        else
        {
            Debug.Log("NO OVERLAP");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        Vector3 tempPosition =  rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;
        if(tempPosition.x < xMin)
        {
            tempPosition.x = xMin;
        }
        else if(tempPosition.x > xMax)
        {
            tempPosition.x = xMax;
        }

        if(tempPosition.y < yMin)
        {
            tempPosition.y = yMin;
        }
        else if(tempPosition.y > yMax)
        {
            tempPosition.y = yMax;
        }
        rectTransform.anchoredPosition = new Vector3(tempPosition.x, tempPosition.y, .5f);
    }
}
