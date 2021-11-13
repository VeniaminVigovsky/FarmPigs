using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerClickHandler
{
    public static Action BombInputReceived;

    public void OnPointerClick(PointerEventData eventData)
    {        
        BombInputReceived?.Invoke();
    }
}
