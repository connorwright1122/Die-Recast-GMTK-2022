using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stable : MonoBehaviour
{
    public Transform enemyLocation;
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;

    public bool useLerp = false;
    public float duration = .1f;

    void Update()
    {
        //position = enemy position
        //transform.position = enemyLocation.position; // new Vector3(enemyLocation.position.x, enemyLocation.position.y, enemyLocation.position.x);
        Vector3 newPos = new Vector3(enemyLocation.position.x + xOffset, enemyLocation.position.y + yOffset, enemyLocation.position.z + zOffset);
        if (useLerp)
        {
            float time = 0;
            while (time < duration)
            {
                transform.position = Vector3.Lerp(transform.position, newPos, time / duration);
                time += Time.deltaTime;
            }
        }
        else
        {
            transform.position = newPos; //new Vector3(enemyLocation.position.x + xOffset, enemyLocation.position.y + yOffset, enemyLocation.position.z + zOffset);
        }
    }
}
