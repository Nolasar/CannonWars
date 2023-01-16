using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperCannonMovement : MonoBehaviour
{
    // Movement limits
    [SerializeField] private Transform areaLeftBorder;
    [SerializeField] private Transform areaRightBorder;

    [SerializeField] private float speed = 10.0f;

    private Transform cannon;
    // Position threshold
    private float xPosThreshold = 0.01f;
    // Movement diraction
    private int dir = 1;

    private float xPos;

    void Start()
    {
        cannon = GetComponent<Transform>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Change cannon position
        xPos += speed * Time.deltaTime * dir;
        cannon.position = new Vector3(xPos, cannon.position.y, cannon.position.z);
        // Out of bounds check
        if (cannon.position.x > areaRightBorder.position.x)
        {
            cannon.position = new Vector3(areaRightBorder.position.x - xPosThreshold, cannon.position.y, cannon.position.z);
            dir = -1;
        }
        else if (cannon.position.x < areaLeftBorder.position.x)
        {
            cannon.position = new Vector3(areaLeftBorder.position.x + xPosThreshold, cannon.position.y, cannon.position.z);
            dir = 1;
        }
    }
}
