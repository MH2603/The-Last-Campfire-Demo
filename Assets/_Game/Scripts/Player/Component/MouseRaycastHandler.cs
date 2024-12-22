using System;
using UnityEngine;

namespace MH.Component
{
    public class MouseRaycastHandler : EntityComponent
    {
        #region ---------- Inspectors ------------

        // LayerMask to filter objects that the raycast can hit
        [SerializeField] private LayerMask raycastLayerMask;

        // Maximum distance for the raycast
        [SerializeField] private float maxRaycastDistance = 100f;

        #endregion

        #region ---------- Properties ---------------

        // Event for notifying when a raycast hits an object
        public Action<RaycastHit> OnRaycastHit;

        // Event for notifying when no object is hit by the raycast
        public Action OnRaycastMiss;

        #endregion
        

        

        // Update is called once per frame
        private void Update()
        {
            HandleRaycast();
        }

        /// <summary>
        /// Handles the raycast logic from the mouse position.
        /// </summary>
        private void HandleRaycast()
        {
            // Check if the left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                // Create a ray from the camera through the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Perform the raycast
                if (Physics.Raycast(ray, out hit, maxRaycastDistance, raycastLayerMask))
                {
                    // Trigger the event if an object is hit
                    OnRaycastHit?.Invoke(hit);
                }
                else
                {
                    // Trigger the event if no object is hit
                    OnRaycastMiss?.Invoke();
                }
            }
        }

        /// <summary>
        /// Sets the LayerMask for the raycast.
        /// </summary>
        /// <param name="layerMask">The new LayerMask.</param>
        public void SetRaycastLayerMask(LayerMask layerMask)
        {
            raycastLayerMask = layerMask;
        }

        /// <summary>
        /// Sets the maximum distance for the raycast.
        /// </summary>
        /// <param name="distance">The new maximum distance.</param>
        public void SetMaxRaycastDistance(float distance)
        {
            maxRaycastDistance = distance;
        }
    }
}