using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildAnimalEncounter : AdventureNode
{

    public FriendClass helperFriendBunny;
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

    override public int GetFuelOutcome()
    {
        GameObject tempFriendManager = GameObject.FindGameObjectWithTag("EncounterManager");
        if (tempFriendManager.GetComponent<FriendManager>().CheckForFriend(helperFriendBunny))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
