using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    // Define these by selecting them in the Unity inspector
    public GameObject dialogBox;
    public GameObject choiceButton1;
    public GameObject choiceButton2;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddListenerForRoverReachedEncounter(ShowEncounterInfo);
        EventManager.AddListenerForMakeChoiceEvent(EnactChoice);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the event manager hears that the rover reached an encounter, pop up the dialog and choices
    void ShowEncounterInfo() {
        dialogBox.SetActive(true);
        choiceButton1.SetActive(true);
        choiceButton2.SetActive(true);
    }

    // When the event manager hears that a choice has been selected, act on it
    void EnactChoice(int choice)
    {
        dialogBox.SetActive(true);
        choiceButton1.SetActive(false);
        choiceButton2.SetActive(false);
    }
}
