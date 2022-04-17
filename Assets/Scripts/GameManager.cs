using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GoalChecker[] goals;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int goalCounter = 0;
        foreach (GoalChecker goal in goals)
        {
            if (goal.HasBox())
            {
                goalCounter++;
            }
        }

        // Debug.Log(goalCounter + " goals filled");

        if (goalCounter == goals.Length)
        {
            Debug.Log("Room completed!!!");
            text.SetActive(true);
        } else
        {
            text.SetActive(false);
        }
    }
}
