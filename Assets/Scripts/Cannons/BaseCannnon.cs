using UnityEngine;

public class BaseCannnon : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    protected int movemantDir = 1;
    protected int rotationDir = 1;

    protected float leftAngleDiff;
    protected float rightAngleDiff;

    public Transform leftMovementLimit;
    public Transform rightMovementLimit;

    public Transform leftBoxBorder;
    public Transform rightBoxBorder;
    public virtual void Rotate(Transform cannon)
    {

    }
    public virtual void Move(Transform cannon)
    {

    }
    public virtual void CalculateRotationLimits(Transform cannon)
    {

    }
}
