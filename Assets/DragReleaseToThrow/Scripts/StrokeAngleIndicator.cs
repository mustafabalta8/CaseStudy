﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simsoft.DragReleaseToThrow
{
    public class StrokeAngleIndicator : MonoBehaviour
    {
        StrokeManager StrokeManager;
        Transform playerBallTransform;
        void Start()
        {
            StrokeManager = GameObject.FindObjectOfType<StrokeManager>();
            playerBallTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            transform.position = playerBallTransform.position;
            transform.rotation = Quaternion.Euler(0, StrokeManager.StrokeAngle, 0);
        }
    }
}

