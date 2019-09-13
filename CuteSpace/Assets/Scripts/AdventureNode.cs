using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Don't actually make any AdventureNodes. Instead, the encounters should inherit from this.
public class AdventureNode : MonoBehaviour
{
    // To track the outcome of this node. Based on whether you chose choice 1 or 2.
    public const int OUTCOME_1_MAIN = 0;
    public const int OUTCOME_1_ALT = 1;
    public const int OUTCOME_2_MAIN = 2;
    public const int OUTCOME_2_ALT = 3;
    public const int OUTCOME_1_ALT_2 = 4;
    public const int OUTCOME_2_ALT_2 = 5;


    public string mainDialog;
    public string choice1Text;
    public string choice2Text;
    public string resolution1Text;
    public string resolution1AltText;
    public string resolution1Alt2Text;
    public string resolution2Text;
    public string resolution2AltText;
    public string resolution2Alt2Text;

    int choiceMade = -1;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
    }

    // EncounterManager will call this first. Use this to calculate everything else
    virtual public void MakeChoice(int choice)
    {
        // Base version assumes one result per choice. Overwrite this if different.
        if (choice == 1)
        {
            choiceMade = OUTCOME_1_MAIN;
        } else
        {
            choiceMade = OUTCOME_2_MAIN;
        }
    }

    public int GetChoiceMade()
    {
        return choiceMade;
    }

    public void SetChoiceMade(int choice)
    {
        choiceMade = choice;
    }

    // Requires MakeChoice to be called first
    public string GetResolutionText()
    {
        switch (choiceMade)
        {
            case OUTCOME_1_MAIN:
                return resolution1Text;
            case OUTCOME_1_ALT:
                return resolution1AltText;
            case OUTCOME_2_MAIN:
                return resolution2Text;
            case OUTCOME_2_ALT:
                return resolution2AltText;
            case OUTCOME_1_ALT_2:
                return resolution1Alt2Text;
            default:
                return resolution2Alt2Text;

        }
    }

    // Requires MakeChoice to be called first
    virtual public int GetFuelOutcome()
    {
        return 0;
    }

    // Requires MakeChoice to be called first
    virtual public int GetDurabilityOutcome()
    {
        return 0;
    }

    // Requires MakeChoice to be called first
    virtual public int GetScrapOutcome()
    {
        return 0;
    }

    // Requires MakeChoice to be called first
    virtual public int GetResearchOutcome()
    {
        return 0;
    }
}
