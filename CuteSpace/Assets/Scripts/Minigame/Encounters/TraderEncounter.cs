using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderEncounter : AdventureNode
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
                return 40;
            default:
                return 0;
        }
    }

    override public int GetScrapOutcome()
    {
        switch (GetChoiceMade())
        {
            case OUTCOME_1_MAIN:
                return -15;
            default:
                return 0;
        }
        
    }
}
