using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildAnimalEncounter : AdventureNode
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public int GetResearchOutcome()
    {
        switch (GetChoiceMade())
        {
            case OUTCOME_1_MAIN:
                return 10;
            default:
                return 30;
        }
    }
}
