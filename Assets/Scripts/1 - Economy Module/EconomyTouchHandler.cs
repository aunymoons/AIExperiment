using UnityEngine;
using System.Collections;
using Lean.Touch;

namespace TowerDefense
{
    public class EconomyTouchHandler : MonoBehaviour
    {
        public string currentTeamName;

        [Tooltip("This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)")]
        public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;

        [Tooltip("The currently selected GameObject")]
        public GameObject SelectedGameObject;

        [Tooltip("Change the color of the selected GameObject?")]
        public bool ColorSelected = true;

        [Tooltip("The color of the selected GameObject")]
        public Color SelectedColor = Color.green;

        protected virtual void OnEnable()
        {
            // Hook into the events we need
            LeanTouch.OnFingerTap += OnFingerTap;
        }

        protected virtual void OnDisable()
        {
            // Unhook the events
            LeanTouch.OnFingerTap -= OnFingerTap;
        }

        public void OnFingerTap(LeanFinger finger)
        {

            // Make sure the finger isn't over any GUI elements
            //if (finger.IsOverGui == false)
            //{

            // Raycast information
            Ray ray = finger.GetRay();
            RaycastHit hit = default(RaycastHit);

            // Was this finger pressed down on a collider?
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
            {
                // Select the hit GameObjectk
                    Select(hit.collider.gameObject, finger);
            }
            else
            {
                // Nothing was tapped, so deselect
                Deselect(finger);
            }
            //}
        }

        private void Deselect(LeanFinger currentFinger)
        {
            // Is there a selected GameObject?
            if (SelectedGameObject != null)
            {
                SendTapMessage(SelectedGameObject, false, currentFinger.IsOverGui);

                // Mark selected GameObject null
                SelectedGameObject = null;
            }
        }

        private void Select(GameObject newGameObject, LeanFinger currentFinger)
        {
            // Has the selected GameObject changed?
            if (newGameObject != SelectedGameObject)
            {
                // Deselect the old GameObject
                Deselect(currentFinger);
                // Change selection
                SelectedGameObject = newGameObject;

                SendTapMessage(SelectedGameObject, true, currentFinger.IsOverGui);
                
            }
            else
            {
                    SendTapMessage(SelectedGameObject, true, currentFinger.IsOverGui);
                
            }
        }

        private void SendTapMessage(GameObject gameObject, bool message, bool isUI)
        {


            // Make sure the GameObject exists
            if (gameObject != null)
            {
                // Get memslot from this GameObject
                MemorySlot memSlot = gameObject.GetComponent<MemorySlot>();

                // Make sure the memslot exists
                if (memSlot != null)
                {
                    //Make sure its from your team
                    //if (memSlot.currentTeamName == currentTeamName)
                    //{
                    //SendMessage
                    if (message == true)
                    {
                        memSlot.OnTapSelected(isUI);
                    }
                    else
                    {
                        memSlot.OnTapDeselected(isUI);
                    }
                    //}

                }
            }
        }

    }
}


