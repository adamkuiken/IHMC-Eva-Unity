using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Robotics;

namespace RosSharp.Control
{
    public enum RotationDirection { None = 0, Positive = 1, Negative = -1 };
    public enum ControlType { PositionControl };

    public class MyMotorController : MonoBehaviour
    {
        public float stiffness;
        public float damping = 150f;
        public float forceLimit;
        public float speed; // Units: degree/s
        public float torque; // Units: Nm or N
        public float acceleration;// Units: m/s^2 / degree/s^2
        // Start is called before the first frame update

        public GameObject LeftSide;
        public GameObject RightSide;
        public Vector3 torquee;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateDirection();
        }

        private void UpdateDirection()
        {

            bool SelectionInput1 = Input.GetKeyDown("l");
            bool SelectionInput2 = Input.GetKeyDown("j");
            float moveDirection = 0;
            float sideCount = 0;
            if (SelectionInput1 == true)
            {
                moveDirection = 1;
                sideCount = 0;
            }
            else if (SelectionInput2 == true)
            {
                moveDirection = -1;
                sideCount = 0;
            }
            bool SelectionInput3 = Input.GetKeyDown("d");
            bool SelectionInput4 = Input.GetKeyDown("a");
            if (SelectionInput3 == true)
            {
                moveDirection = 1;
                sideCount = 1;
            }
            else if (SelectionInput4 == true)
            {
                moveDirection = -1;
                sideCount = 1;
            }
            GameObject sideToMove;
            if (sideCount == 1)
            {
                sideToMove = LeftSide;

            }
            else
            {
                sideToMove = RightSide;
            }
            ArticulationBody current = sideToMove.GetComponent<ArticulationBody>();


            //if (current.controltype != control)
            //{
            //   UpdateControlType(current);
            //}

            if (moveDirection > 0)
            {
                current.AddTorque(torquee);// RotationDirection.Positive;
            }
            else if (moveDirection < 0)
            {
                current.AddTorque(-torquee);//RotationDirection.Negative;
            }
            else
            {
                current.AddTorque(Vector3.zero);
            }


        }

    }



}