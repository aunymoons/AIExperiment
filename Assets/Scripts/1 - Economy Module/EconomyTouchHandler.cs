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
                Deselect();
            }
            //}
        }

        private void Deselect()
        {
            // Is there a selected GameObject?
            if (SelectedGameObject != null)
            {
                SendTapMessage(SelectedGameObject, false);

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
                Deselect();


                if (currentFinger.IsOverGui == false)
                {
                    // Change selection
                    SelectedGameObject = newGameObject;

                    SendTapMessage(SelectedGameObject, true);
                }
                    


            }
            else
            {
                if (currentFinger.IsOverGui == false)
                {
                    SendTapMessage(SelectedGameObject, true);
                }
            }
        }

        private void SendTapMessage(GameObject gameObject, bool message)
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
                        memSlot.OnTapSelected();
                    }
                    else
                    {
                        memSlot.OnTapDeselected();
                    }
                    //}

                }
            }
        }

    }
}


