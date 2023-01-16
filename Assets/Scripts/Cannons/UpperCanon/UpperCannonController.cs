using UnityEngine;

public class UpperCannonController : MonoBehaviour
{
    private Transform cannon;

    [SerializeField] private Transform leftMovementLimit;
    [SerializeField] private Transform rightMovementLimit;

    [SerializeField] private Transform leftBoxBorder;
    [SerializeField] private Transform rightBoxBorder;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float xSpeed;
    
    UpperCannon upperCannon = new UpperCannon();

    private void Awake()
    {
        cannon = GetComponent<Transform>();

        upperCannon.leftMovementLimit = leftMovementLimit;
        upperCannon.rightMovementLimit = rightMovementLimit;

        upperCannon.leftBoxBorder = leftBoxBorder;
        upperCannon.rightBoxBorder = rightBoxBorder;

        upperCannon.rotationSpeed = rotationSpeed;
        upperCannon.speed = xSpeed;
    }

    private void Update()
    {
        upperCannon.CalculateRotationLimits(cannon);
        upperCannon.Move(cannon);
        upperCannon.Rotate(cannon);
    }
}
