using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public enum Trajectory { Linear, Ballistic }
    public Trajectory trajectory;

    private Vector3 dir;
    public float speed;
    private float angle;
    private float _time = 0;
    public float gravity = 9.81f;
    private Vector3 startPos;

    void Start()
    { 
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
                transform.position += dir * speed * Time.deltaTime;
                break;
            // Ballistic movement
            case Trajectory.Ballistic:
                float xPos = speed * Mathf.Cos(angle) * _time;
                float yPos = speed * Mathf.Sin(angle) * _time - (1f / 2f) * gravity * Mathf.Pow(_time, 2);
                transform.position = startPos + new Vector3(xPos, yPos, -1);
                _time += Time.deltaTime;
                break;
        }      
    }
}
