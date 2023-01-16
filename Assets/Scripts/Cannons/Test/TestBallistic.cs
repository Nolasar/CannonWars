using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallistic : MonoBehaviour
{
    public float angle;
    public float speed;
    private float _time = 0;
    public float gravity = 9.81f;
    private Vector3 startPos;

    public Transform maxTarget;
    public Transform minTarget;
    void Start()
    {
        startPos = transform.position;
        angle = CalculateAngle(minTarget);
        Debug.Log(CalculateAngle(minTarget) * Mathf.Rad2Deg);
        Debug.Log(CalculateAngle(maxTarget) * Mathf.Rad2Deg);

    }

    // Update is called once per frame
    void Update()
    {
        float xPos = speed * Mathf.Cos(angle) * _time;
        float yPos = speed * Mathf.Sin(angle) * _time - (1f / 2f) * gravity * Mathf.Pow(_time, 2);
        transform.position = startPos + new Vector3(xPos, yPos, -1);
        _time += Time.deltaTime;
    }

    float CalculateAngle(Transform target)
    {
        float x = -startPos.x + target.position.x;
        float y = -startPos.y + target.position.y;
        float f = (Mathf.Pow(speed,2) + Mathf.Sqrt(Mathf.Pow(speed,4) - gravity * 
            (gravity * Mathf.Pow(x,2) + 2 * Mathf.Pow(speed,2) * y)))/ (gravity * x);
        float angle = Mathf.Atan(f);
        return angle;
    }
}
