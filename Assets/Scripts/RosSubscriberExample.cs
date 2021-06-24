using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosColor = RosMessageTypes.RoboticsDemo.MUnityColor;
using System;
using Unity.Robotics;

public class RosSubscriberExample : MonoBehaviour
{
    public GameObject cube;
    public GameObject target;
    public float scaler;

    void Start()
    {
        ROSConnection.instance.Subscribe<RosColor>("color", changeRotation);
        Debug.Log("Got Here");
    }

    void changeRotation(RosColor colorMessage)
    {
        Debug.Log("Got Here Too");
        //ArticulationJointController JC = cube.GetComponent<ArticulationJointController>();
        //JC.rotationState = RotationDirection.Positive;
        doTheRotation(cube, target);

    }
    void doTheRotation(GameObject oldPoint, GameObject newPoint)
    {
        //Shout out hellcats on Unity Forum 
        //https://answers.unity.com/questions/48836/determining-the-torque-needed-to-rotate-an-object.html

        Vector3 x = Vector3.Cross(oldPoint.transform.position.normalized, newPoint.transform.position.normalized);
        float theta = Mathf.Asin(x.magnitude);
        Vector3 w = x.normalized * theta / Time.fixedDeltaTime;
        Quaternion q = transform.rotation * oldPoint.GetComponent<ArticulationBody>().inertiaTensorRotation;
        Vector3 T = q * Vector3.Scale(oldPoint.GetComponent<ArticulationBody>().inertiaTensor, (Quaternion.Inverse(q) * w));
        oldPoint.GetComponent<ArticulationBody>().AddRelativeTorque(T*scaler); // should be ,ForceMode.Impulse);
    }
}