using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpperCannonRotation : MonoBehaviour
{
    [SerializeField] private Transform fieldRightBorder;
    [SerializeField] private Transform fieldLeftBorder;

    private Transform cannon;
    public float rotationSpeed = 10f;
    public float initialRotation = 0;
    private float angleThreshold = 0.1f;
    private float dir = 1;

    [SerializeField] private float leftAngle;
    [SerializeField] private float rightAngle;

    private float rotationZ;


    void Start()
    {
        cannon = GetComponent<Transform>();
        rotationZ = initialRotation;
    }

    void Update()
    {
        BoundariesCalculating();
        RotateCannon();
      
    }

    private void BoundariesCalculating()
    {
        Vector3 leftBorder = fieldLeftBorder.position - cannon.position;
        Vector3 rightBorder = fieldRightBorder.position - cannon.position;

        leftAngle = Vector3.Angle(-cannon.up, leftBorder) * (Vector3.Cross(-cannon.up, leftBorder).z < 0 ? 1 : -1);
        rightAngle = Vector3.Angle(-cannon.up, rightBorder) * (Vector3.Cross(-cannon.up, rightBorder).z < 0 ? -1 : 1);
    }
    private void RotateCannon()
    {
        rotationZ += rotationSpeed * Time.deltaTime * dir;
        cannon.rotation = Quaternion.Euler(0, 0, rotationZ);
        if (leftAngle <= angleThreshold)
        {
            rotationZ -= (leftAngle +  angleThreshold * Time.deltaTime * rotationSpeed);
            dir = 1;
        }
        else if (rightAngle <= angleThreshold)
        {
            rotationZ += (rightAngle + angleThreshold * Time.deltaTime * rotationSpeed);
            dir = -1;
        }
    }
}
