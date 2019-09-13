using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendClassResearchBoost : FriendClass
{
    public float researchBoost;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ApplyBenefit()
    {
        GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
        if (tempManager != null)
        {
            tempManager.GetComponent<GameManager>().AlterResearchModifier(researchBoost);
        }
        else
        {
            Debug.Log("There is no GameManager in this scene. Please add one.");
        }

    }

    public override void RemoveBenefit()
    {
        GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
        if (tempManager != null)
        {
            tempManager.GetComponent<GameManager>().AlterResearchModifier(-researchBoost);
        }
        else
        {
            Debug.Log("There is no GameManager in this scene. Please add one.");

        }


    }
}
