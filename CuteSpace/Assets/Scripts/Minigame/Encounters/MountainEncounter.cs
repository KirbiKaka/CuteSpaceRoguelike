using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainEncounter : AdventureNode
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void MakeChoice(int choice)
    {
        // Base version assumes one result per choice. Overwrite this if different.
        if (choice == 1)
        {

            SetChoiceMade(OUTCOME_1_MAIN);
        }
        else
        {
            float roll = Random.Range(0f, 1f);
            if (roll >= .75)
            {
                SetChoiceMade(OUTCOME_2_MAIN);
            }
            else if (roll >= .5)
            {
                SetChoiceMade(OUTCOME_2_ALT);
            }
            else
            {
                SetChoiceMade(OUTCOME_2_ALT_2);
            }
        }
    }

    override public int GetFuelOutcome()
    {
        switch (GetChoiceMade())
        {
            case OUTCOME_1_MAIN:
                return -3;
            case OUTCOME_2_ALT:
                return -3;
            default:
                return 0;
        }
    }

    override public int GetResearchOutcome()
    {
        switch (GetChoiceMade())
        {
            case OUTCOME_2_ALT:
                return 20;
            default:
                return 0;
        }
    }

    override public int GetDurabilityOutcome()
    {
        switch (GetChoiceMade())
        {
            case OUTCOME_2_MAIN:
                return -5;
            case OUTCOME_2_ALT_2:
                return -1;
            default:
                return 0;
        }
    }
}
