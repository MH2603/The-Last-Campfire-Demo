
using UnityEngine.EventSystems;
using System;

namespace MH.Puzzle.SlidingTile
{
    public class ClickableHandler : EntityComponent, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IDragHandler
    {
        public Action<PointerEventData> OnEnterMouse;
        public Action<PointerEventData> OnDownMouse;
        public Action<PointerEventData> OnUpMouse;
        public Action<PointerEventData> OnClickEvent;
        public Action<PointerEventData> OnExitMouse;
        public Action<PointerEventData> OnDragEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDownMouse?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnterMouse?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExitMouse?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUpMouse?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent?.Invoke(eventData);
        }
    }
}