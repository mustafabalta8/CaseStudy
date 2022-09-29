using UnityEngine;
using System.Collections;

namespace Simsoft.CaseStudyTabDragToSort
{
    public class Draggable : MonoBehaviour
    {
        [SerializeField] private Fruit objectType;
        [SerializeField] private Place currentPlace;
        private Vector3 mOffset;
        private float mZCoord;

        private bool canDrop = false;
        [SerializeField] private DropPlace dropPlace;

        private Vector3 defaultPosition;

        public Fruit ObjectType { get => objectType; }
        public Place CurrentPlace { get => currentPlace; }

        private void Start()
        {
            defaultPosition = transform.position;

            StartCoroutine(LateStart());
        }

        IEnumerator LateStart()
        {
            yield return new WaitForSeconds(0.3f);
            currentPlace = dropPlace.Place;
            ObjectOrderManager.instance.UpdateObjectCount(dropPlace.Place, this);
        }
        void OnMouseDown()
        {
            mZCoord = Camera.main.WorldToScreenPoint(
                gameObject.transform.position).z;

            // gameobject world pos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
            mOffset.y = 0;

        }
        private void OnMouseDrag()
        {
            Vector3 mousePos = GetMouseAsWorldPoint();
            transform.position = new Vector3(mousePos.x,transform.position.y, mousePos.z) + mOffset;
        }
        private void OnMouseUp()
        {            
            if(canDrop)
            {
                transform.position = dropPlace.transform.position;
                defaultPosition = transform.position;

                ObjectOrderManager.instance.UpdateObjectCount(dropPlace.Place, currentPlace, this);
                currentPlace = dropPlace.Place;
                
            }
            else
            {
                transform.position = defaultPosition;
            }
        }
        
        private void OnTriggerStay(Collider other)
        {
            //print("OnTriggerStay  "+canDrop);
            if (other.GetComponent<DropPlace>())
            {
                canDrop = true;
                dropPlace = other.GetComponent<DropPlace>();

            }
            else
            {
                canDrop = false;
            }
        }
        private Vector3 GetMouseAsWorldPoint()
        {
            // Pixel coordinates of mouse (x,y)
            Vector3 mousePoint = Input.mousePosition;
            // z coordinate of game object on screen
            mousePoint.z = mZCoord;
            // Convert it to world points
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<DropPlace>())
            {
                canDrop = true;
                dropPosition = other.GetComponent<DropPlace>().transform.position;
            }
            print("OnTriggerenter  " + canDrop);
        }
        private void OnTriggerExit(Collider other)
        {          
            if (other.GetComponent<DropPlace>())
            {
                //canDrop = false;
            }
            
        }*/
    }
}
