

using System;
using MH.Puzzle.SlidingTile;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestClickableHandler : MonoBehaviour
{
    public ClickableHandler handler;

    private void Start()
    {
        handler.OnDownMouse += MouseDown;
    }

    private void MouseDown(PointerEventData eventData)
    {
        Debug.Log($" Test : {eventData.button} "  );
    }
        
        
        
}