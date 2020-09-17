using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    public float smoothing;
    public Transform player;
    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;

    void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        //Set your start point
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            origin = data.position;
        }
        
    }
    public void OnDrag(PointerEventData data)
    {
        if(data.pointerId == pointerID)
        {
            //Compare the different betwen out start point and current pointer pos
            Vector2 currentPosition = data.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
        }
        
    }
    public void OnPointerUp(PointerEventData data)
    {
        if(data.pointerId == pointerID)
        {
            //Reset Everything
            direction = Vector2.zero;
            touched = false;
        }
        
    }
    public Vector2 GetDirecion()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
        return smoothDirection;
    }
}
