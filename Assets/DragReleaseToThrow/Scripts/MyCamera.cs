using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simsoft.DragReleaseToThrow
{
    public class MyCamera : MonoBehaviour
    {
        private Transform target;

        [SerializeField] private Vector3 offset;
        [SerializeField] private float lerpValue=10f;

        [SerializeField] private float distance = 10f;
        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            offset =  transform.position - target.position+ new Vector3(0,0,-distance);

        }
        private void LateUpdate()
        {
            
            if(Mathf.Abs(transform.position.z-target.position.z) > 7)
            {
                MoveCamera();
            }

        }

        private void MoveCamera()
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0,0,target.position.z) + offset, lerpValue *Time.deltaTime);
        }
    }
}
