using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    private bool hasBoxOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Box")
        {
            return;
        }

        hasBoxOver = true;

        Debug.Log("OnTriggerEnter " + other.name + " - " + hasBoxOver);

        // @TODO ¿Avisar al GameManager en vez de hacer polling?
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Box")
        {
            return;
        }

        hasBoxOver = false;

        Debug.Log("OnTriggerExit " + other.name + " - " + hasBoxOver);

        // @TODO ¿Avisar al GameManager en vez de hacer polling?
    }

    public bool HasBox()
    {
        // Debug.Log("Has " + gameObject.name + " box?" + (hasBoxOver ? " Yes, it has" : " No, it has not"));
        return hasBoxOver;
    }
}
