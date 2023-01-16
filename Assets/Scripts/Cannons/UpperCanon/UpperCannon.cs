using UnityEngine;

public class UpperCannon : BaseCannnon
{
    private float xPosThreshold = 0.01f;
    private float angleThreshold = 0.1f;
    private float rotationZ;
    private float xPos;

    public override void Move(Transform cannon)
    {
        // Change cannon position
        xPos += speed * Time.deltaTime * movemantDir;
        cannon.position = new Vector3(xPos, cannon.position.y, cannon.position.z);
        // Out of bounds check
        if (cannon.position.x > rightMovementLimit.position.x)
        {
            cannon.position = new Vector3(rightMovementLimit.position.x - xPosThreshold, cannon.position.y, cannon.position.z);
            movemantDir = -1;
        }
        else if (cannon.position.x < leftMovementLimit.position.x)
        {
            cannon.position = new Vector3(leftMovementLimit.position.x + xPosThreshold, cannon.position.y, cannon.position.z);
            movemantDir = 1;
        }
    }
    public override void Rotate(Transform cannon)
    {
        rotationZ += rotationSpeed * Time.deltaTime * rotationDir;
        cannon.rotation = Quaternion.Euler(0, 0, rotationZ);
        if (leftAngleDiff <= angleThreshold)
        {
            rotationZ -= (leftAngleDiff + angleThreshold * Time.deltaTime * rotationSpeed);
            rotationDir = 1;
        }
        else if (rightAngleDiff <= angleThreshold)
        {
            rotationZ += (rightAngleDiff + angleThreshold * Time.deltaTime * rotationSpeed);
            rotationDir = -1;
        }
    }

    public override void CalculateRotationLimits(Transform cannon)
    {
        Vector3 leftBorder = leftBoxBorder.position - cannon.position;
        Vector3 rightBorder = rightBoxBorder.position - cannon.position;

        leftAngleDiff = Vector3.Angle(-cannon.up, leftBorder) * (Vector3.Cross(-cannon.up, leftBorder).z < 0 ? 1 : -1);
        rightAngleDiff = Vector3.Angle(-cannon.up, rightBorder) * (Vector3.Cross(-cannon.up, rightBorder).z < 0 ? -1 : 1);
    }
}
