using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUI : MonoBehaviour
{
    public GameObject StatsUI;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivateObject(StatsUI);
            other.gameObject.transform.parent.GetComponent<InputHandler>().canAttack = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DeactivateObject(StatsUI);
            other.gameObject.transform.parent.GetComponent<InputHandler>().canAttack = true;
        }
    }

    public void ActivateObject(GameObject go)
    {
        go.SetActive(true);
    }
    public void DeactivateObject(GameObject go)
    {
        go.SetActive(false);
    }
}
