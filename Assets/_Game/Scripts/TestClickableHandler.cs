
using UnityEngine;
using UnityEngine.EventSystems;

namespace  MH
{
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
}


