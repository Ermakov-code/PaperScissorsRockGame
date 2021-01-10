using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerScript : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    
    [SerializeField] private GameObject player;

    [SerializeField] private float moveTime;
    

   // private bool[] _positionControl;
   private float _currentPosition;
   private bool _isMoving;

    public void Start()
    {
        _currentPosition = 2;
        _isMoving = false;
        // _positionControl = new bool[3]{false, true, false};
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isMoving)
        {
            if (eventData.delta.x > 0)
            {
                if (_currentPosition != 3)
                {
                    _currentPosition++;
                    //player.transform.DOMove(player.transform.position + Vector3.right * 5, moveTime).OnStart(() => StartCoroutine(IsMoving()));
                    // player.transform.DOMove(player.transform.position + Vector3.right * 5, moveTime).OnStart(Switcher(_isMoving)).OnComplete(
                    //     Switcher(_isMoving));
                    player.transform.DOMove(player.transform.position + Vector3.right * 4, moveTime).SetEase(Ease.Linear)
                        .OnStart(() => Switcher(_isMoving)).OnComplete(()=> Switcher(_isMoving));
                }
            }
            else
            {
                if (_currentPosition != 1)
                {
                    _currentPosition--;
                    //player.transform.DOMove(player.transform.position + Vector3.left * 5, moveTime).OnStart(() => StartCoroutine(IsMoving()));
                    // player.transform.DOMove(player.transform.position + Vector3.left * 5, moveTime).OnStart(Switcher(_isMoving)).OnComplete(
                    //     Switcher(_isMoving));
                    player.transform.DOMove(player.transform.position + Vector3.left * 4, moveTime).SetEase(Ease.Linear)
                        .OnStart(() => Switcher(_isMoving)).OnComplete(()=> Switcher(_isMoving));

                }
            }
        }
    }

    // public TweenCallback Switcher(bool _isMoving)
    // {
    //     this._isMoving = !_isMoving;
    //     return null;
    // }
    public void Switcher(bool _isMoving)
    {
        this._isMoving = !_isMoving;
    }

    // public IEnumerator IsMoving()
    // {
    //     _isMoving = true;
    //     yield return new WaitForSeconds(moveTime);
    //     _isMoving = false;
    // }
}
