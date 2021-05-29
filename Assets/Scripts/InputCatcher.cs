using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputCatcher : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
{
    public bool IsMoving => _isPlayerHoldingMove;

    public float SideShift => _sideShift;

    private float _sideShift;
    private bool _isPlayerHoldingMove;
    private Vector3 _startPointerPosition;
    private Vector3 _currentPointerPosition;

    private RectTransform _rect;
    private float _screenWidth;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _screenWidth = _rect.rect.width;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPlayerHoldingMove = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPlayerHoldingMove = false;
        RenewData();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPointerPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _currentPointerPosition = eventData.position;
        _sideShift = (_currentPointerPosition.x - 540f) / _screenWidth * .5f;
        Debug.Log(_sideShift);
    }

    private void RenewData()
    {
        _startPointerPosition = Vector3.zero;
        _currentPointerPosition = Vector3.zero;
        _sideShift = 0;
    }
}
