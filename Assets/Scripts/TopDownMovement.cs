using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private InputHandler input;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    public float moveSpeed;//, maxMoveSpeed;

    private Rigidbody rb;
    private Transform child;

    public Stats stats;

    public Transform respawnPoint;

    private void Start()
    {
        input = GetComponent<InputHandler>();
        cam = FindObjectOfType<Camera>();

        rb = GetComponentInChildren<Rigidbody>();
        child = this.gameObject.transform.GetChild(0);

        stats = GameObject.FindObjectOfType<Stats>();
    }

    private void Update()
    {
        var targetVector = new Vector3(input.InputVector.x, 0, input.InputVector.y);
        //var targetVector = input.InputVector;

        MoveTowardTarget(targetVector);
        RotateTowardMouseV1();
        //RotateTowardMouseV2();
    }

    private void RotateTowardMouseV1()
    {
        Ray ray = cam.ScreenPointToRay(input.MousePosition);

        int layerMask = 1 << 9;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f, layerMask))
        {
            var target = hitInfo.point;
            //target.y = transform.position.y;
            target.y = child.position.y;
            child.LookAt(target);
        }
    }
    
    private void RotateTowardMouseV2()
    {
        //Ray ray = cam.ScreenPointToRay(input.MousePosition);
        Vector3 mousePosition = input.MousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;

        /*
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            //target.y = transform.position.y;
            target.y = child.position.y;
            child.LookAt(target);
        }
        */

    }

    private void MoveTowardTarget(Vector3 targetVector)
    {
        //var speed = moveSpeed * Time.deltaTime;
        //transform.Translate(targetVector * speed);
        rb.velocity = targetVector * moveSpeed;
    }

    public void RandomizeStats()
    {
        moveSpeed = Mathf.Round(UnityEngine.Random.Range(10, stats.maxMoveSpeed + 1));
    }


    public void ResetPlayerMovement()
    {
        child.position = respawnPoint.position;
        RandomizeStats();
        //transform.position = respawnPoint.position;
        //child.position = new Vector3(0f, 0f, 0f);
    }
}
