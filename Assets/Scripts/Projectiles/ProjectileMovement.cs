using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private MovementConstants constants;
    public enum Trajectory { Linear, Ballistic }
    public Trajectory trajectory;

    private Vector3 dir;
    private float speed;
    private float angle;
    private float _time = 0;
    private Vector3 startPos;

    void Start()
    { 
        constants = GameObject.Find("MovementParams").GetComponent<MovementConstants>();
        startPos = transform.position;
        // Initial angle for ballistic formula
        angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        // Movement diraction  for linear formula
        dir = transform.up;       
    }

    void Update()
    {
        // Move projectile
        Move();      
    }

    private void Move()
    {
        // In the editor choose projectile trajectory
        switch (trajectory){
            // Linear movement
            case Trajectory.Linear:
                speed = constants.LINEAR_SPEED;
                transform.position += dir * speed * Time.deltaTime;
                break;
            // Ballistic movement
            case Trajectory.Ballistic:
                speed = constants.BALLISTICAL_SPEED;
                float xPos = speed * Mathf.Cos(angle) * _time;
                float yPos = speed * Mathf.Sin(angle) * _time - (1f / 2f) * constants.GRAVITY * Mathf.Pow(_time, 2);
                transform.position = startPos + new Vector3(xPos, yPos, -1);
                _time += Time.deltaTime;
                break;
        }      
    }
}
