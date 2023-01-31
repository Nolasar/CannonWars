using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticCannonController : MonoBehaviour
{
    [SerializeField] private MovementConstants constants;
    private Transform cannon;

    [SerializeField] private Transform leftBoxBorder;
    [SerializeField] private Transform rightBoxBorder;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isleftCannon;
    // [SerializeField] private float projectileSpeed;
    StaticCannon staticCannon = new StaticCannon();
    void Start()
    {
        cannon = GetComponent<Transform>();

        staticCannon.leftBoxBorder = leftBoxBorder;
        staticCannon.rightBoxBorder = rightBoxBorder;

        staticCannon.rotationSpeed = rotationSpeed;
        staticCannon.isLeftCannon = isleftCannon;
        
        staticCannon.speed = constants.BALLISTICAL_SPEED;
        staticCannon.CalculateRotationLimits(attackPoint);

    }

    // Update is called once per frame
    void Update()
    {       
        staticCannon.Rotate(cannon);
    }
}
