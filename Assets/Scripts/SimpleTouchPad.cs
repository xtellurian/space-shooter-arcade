using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float Smoothing;
    private Vector2 _origin;
    private Vector2 _direction;
    private Vector2 _smoothDirection;
    private bool _touched;
    private int _pointerId;
    void Awake()
    {
        _direction = Vector2.zero;
        _touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_touched) return; // prevent second touch
        _touched = true;
        _pointerId = eventData.pointerId;
        _origin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != _pointerId) return; // match touches

        var currentPosition = eventData.position;
        var directionRaw = currentPosition - _origin;
        _direction = directionRaw.normalized;
        Debug.Log(_direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.pointerId != _pointerId) return; // match touches

        _direction = Vector2.zero;
        _touched = false;
    }

    public Vector2 GetDirection()
    {
        _smoothDirection = Vector2.MoveTowards(_smoothDirection, _direction, Smoothing);
        return _smoothDirection;
    }
}
