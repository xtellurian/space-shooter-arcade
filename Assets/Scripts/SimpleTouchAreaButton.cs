using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler {

   
    private bool _touched;
    private int _pointerId;
    private bool _canFire;

    void Awake()
    {
        _touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_touched) return; // prevent second touch

        _touched = true;
        _pointerId = eventData.pointerId;
        _canFire = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId != _pointerId) return; // match touches

        _touched = false;
        _canFire = false;
    }

    public bool CanFire()
    {
        return _canFire;
    }

}
