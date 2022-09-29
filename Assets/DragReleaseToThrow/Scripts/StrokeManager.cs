using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simsoft.DragReleaseToThrow
{
    public class StrokeManager : MonoBehaviour
    {
        //input
        private float lastFrameFingerPositionX;
        private float moveFactorX;

        [SerializeField] private float strokeForce;

        public float StrokeAngle { get; protected set; }
        public float StrokeForce { get => strokeForce; set => strokeForce = value; }

        //float strokeForceFillSpeed = 5f;
        //int fillDir = 1;

        //float maxStrokeForce = 10f;

        [SerializeField] private GameObject StrokeIndicator;

        public enum StrokeModeEnum
        {
            AIMING,
            FILLING,
            ROLLING
        };

        public StrokeModeEnum StrokeMode { get; protected set; }


        Rigidbody playerBallRB;
        void Start()
        {
            FindPlayerBall();

        }
        private void FindPlayerBall()
        {
            GameObject ball = GameObject.FindGameObjectWithTag("Player");
            if (ball == null)
            {
                Debug.LogError("Couldn't find the ball.");
            }

            playerBallRB = ball.GetComponent<Rigidbody>();

        }
        private void Update()
        {
            if (StrokeMode == StrokeModeEnum.AIMING)
            {
                GetInput();
                StrokeAngle += moveFactorX * 100f * Time.deltaTime;
            }

            //if(StrokeMode == StrokeModeEnum.FILLING)
            //{
            //    StrokeForce += (strokeForceFillSpeed * fillDir) * Time.deltaTime;
            //    if(StrokeForce > maxStrokeForce)
            //    {
            //        StrokeForce = maxStrokeForce;
            //        fillDir = -1;
            //    }
            //    else if (StrokeForce < 0)
            //    {
            //        StrokeForce = 0;
            //        fillDir = 1;
            //    }
            //}


        }

        private void Fire()
        {
            StrokeMode = StrokeModeEnum.ROLLING;

            Vector3 forceVec = new Vector3(0, 0, StrokeForce);

            playerBallRB.AddForce(Quaternion.Euler(0, StrokeAngle, 0) * forceVec, ForceMode.VelocityChange);

            StrokeForce = 0;
            //fillDir = 1;      
        }

        private void GetInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastFrameFingerPositionX = Input.mousePosition.x;
                StrokeIndicator.SetActive(true);
            }
            else if (Input.GetMouseButton(0))
            {
                moveFactorX = Input.mousePosition.x - lastFrameFingerPositionX;
                lastFrameFingerPositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                moveFactorX = 0f;
                StrokeIndicator.SetActive(false);
                Fire();
            }
        }


    }
}

