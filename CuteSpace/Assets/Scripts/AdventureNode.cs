using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Don't actually make any AdventureNodes. Instead, the encounters should inherit from this.
public class AdventureNode : MonoBehaviour
{
    // To track the outcome of this node. Based on whether you chose choice 1 or 2.
    const int OUTCOME_1_MAIN = 0;
    const int OUTCOME_1_ALT = 1;
    const int OUTCOME_2_MAIN = 2;
    const int OUTCOME_2_ALT = 3;

    RoverMoveEvent roverMoveEvent = new RoverMoveEvent();
    public string mainDialog;
    public string choice1Text;
    public string choice2Text;
    public string resolution1Text;
    public string resolution1AltText;
    public string resolution2Text;
    public string resolution2AltText;

    int choiceMade = -1;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddListenerForTestEvent(handleTestEvent);
    }

    void handleTestEvent(int thing)
    {
        print(thing);
    }

    public void RoverMoveEventAddedEventListener(UnityAction listener)
    {
        roverMoveEvent.AddListener(listener);
    }

    private void Update()
    {
    }

    // EncounterManager will call this first. Use this to calculate everything else
    public void MakeChoice(int choice)
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
            default:
                return resolution2AltText;
        }

    }
}
