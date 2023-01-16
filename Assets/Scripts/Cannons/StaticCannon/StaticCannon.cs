using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCannon : BaseCannnon
{
    private float angleThreshold = 0.01f;

    public float rotationZ = 60;
    // check cannon orientation
    public bool isLeftCannon;

    private float minAngle;
    private float maxAngle;

    private float gravity = 9.81f;
 
    public override void CalculateRotationLimits(Transform cannon)
    {
        // Calculate max,min angles for cannon rotation depend on cannon orientation
        if (!isLeftCannon)
        {
            // For right cannon
            maxAngle = CalculateAngle(cannon, rightBoxBorder);
            minAngle = CalculateAngle(cannon, leftBoxBorder);
            Debug.Log($"Max angle :{maxAngle} Min angle {minAngle}");
        }
        else
        {
            // For left
            minAngle = CalculateAngle(cannon, rightBoxBorder);
            maxAngle = CalculateAngle(cannon, leftBoxBorder);
            Debug.Log($"Max angle :{maxAngle} Min angle {minAngle}");
        }
    }

    // Calculate angle at the known range to target and const speed
    private float CalculateAngle(Transform cannon, Transform target) 
    {
        Vector2 startPos = cannon.position;

        float x = -startPos.x + target.position.x;
        float y = -startPos.y + target.position.y;

        float f = (Mathf.Pow(speed, 2) + Mathf.Sqrt(Mathf.Pow(speed, 4) - gravity *
            (gravity * Mathf.Pow(x, 2) + 2 * Mathf.Pow(speed, 2) * y))) / (gravity * x);
        float angle = Mathf.Atan(f);

        // Return angle in degrees
        return angle * Mathf.Rad2Deg;
    }

    public override void Rotate(Transform cannon)
    {
        rotationZ += rotationSpeed * Time.deltaTime * rotationDir;
        cannon.rotation = Quaternion.Euler(0, 0, rotationZ);

        // change diraction if cannon rotation out of bounds(min,max)
        if (rotationZ > maxAngle)
        {
            rotationZ -= (angleThreshold + Time.deltaTime * rotationSpeed);
            rotationDir = -1;
        }
        else if (rotationZ < minAngle)
        {
            rotationZ += (angleThreshold + Time.deltaTime * rotationSpeed);
            rotationDir = 1;
        }
    }
}
