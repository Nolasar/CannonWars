using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera _cam;
    private CircleCollider2D _collider;
    [SerializeField] private GameObject trailPrefab;
    private GameObject currentTrail;
    private bool isCutting;
    private Vector2 previousPosition;
    [SerializeField] private float velocityThreshold = 0.001f;
    private float zPosTheshold = -1.0f;

    void Start()
    {      
        _cam = Camera.main;
        _collider= GetComponent<CircleCollider2D>();
        _collider.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { StartCutting(); }

        if (Input.GetMouseButtonUp(0)) { StopCutting(); }

        if (isCutting) { UpdateCutting(); }     
    }

    private void StartCutting()
    {
        isCutting = true;
        // set blade position equal mouse position
        previousPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        // create instance of trail
        currentTrail = Instantiate(trailPrefab, previousPosition, Quaternion.identity);

        _collider.enabled = false;
    }

    private void StopCutting()
    {
        isCutting = false;
        // delete trail
        Destroy(currentTrail, .1f);
        _collider.enabled = false;
    }

    private void UpdateCutting()
    {
        // set blade position equal mouse position
        transform.position = _cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPosition = transform.position;
        // velocity of blade
        float cuttingVelocity = (currentPosition - previousPosition).magnitude / Time.deltaTime;
        // set traul pos equal mouse pos
        currentTrail.transform.position = new Vector3(currentPosition.x,currentPosition.y, zPosTheshold);
        // check if velocity is small - disable collider
        if (cuttingVelocity > velocityThreshold) { _collider.enabled = true; }
        else { _collider.enabled = false; }
        
        previousPosition = currentPosition;
    }
}
