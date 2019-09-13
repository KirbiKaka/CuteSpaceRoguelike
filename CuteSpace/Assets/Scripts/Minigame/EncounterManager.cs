using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    // Define these by selecting them in the Unity inspector
    public GameObject dialogBox;
    public GameObject choiceButton1;
    public GameObject choiceButton2;
    public List<GameObject> encounterPool1;
    public GameObject fuelReadout;
    public GameObject durabilityReadout;
    public GameObject scrapReadout;
    public GameObject researchReadout;
    public GameObject groundPrefab;

    RoverMoveEvent roverMoveEvent = new RoverMoveEvent();

    int playerFuel;
    int playerFuelMax = 15;
    int playerDurability;
    int playerDurabilityMax = 10;
    int playerScrap;
    int playerResearch;

    int encounterCount;

    Timer startGameTimer;
    bool gameStarted;
    AdventureNode currentEncounter;
    AdventureNode previousEncounter;
    bool waitingForResolution;
    Timer resolutionTapTimer;
    bool toggleSpawnMoreGround = true;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddListenerForRoverReachedEncounter(ShowEncounterInfo);
        EventManager.AddListenerForMakeChoiceEvent(EnactChoice);
        EventManager.AddInvokerForRoverMoveEvent(this);

        // TODO: inc playerFuelMax based on supergame upgrades
        playerFuel = playerFuelMax;
        // TODO: inc playerDurabilityMax based on supergame upgrades
        playerDurability = playerDurabilityMax;
        UpdateReadouts();

        startGameTimer = gameObject.AddComponent<Timer>();
        startGameTimer.Duration = 2;
        startGameTimer.Run();

        resolutionTapTimer = gameObject.AddComponent<Timer>();
        resolutionTapTimer.Duration = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (startGameTimer.Finished == true && gameStarted == false)
        {
            gameStarted = true;

            Vector3 encounterPosition = Camera.main.transform.position;
            encounterPosition.x += 850;
            encounterPosition.y -= 100;
            encounterPosition.z = 0;
            GameObject encounter = Instantiate(encounterPool1[encounterCount], encounterPosition, Quaternion.identity);
            currentEncounter = encounter.GetComponent<AdventureNode>();
            encounterCount += 1;
            roverMoveEvent.Invoke(currentEncounter.gameObject);
        }

        if (Input.GetMouseButtonUp(0))
        {
            PlayerTappedOnScreen();
        }
    }

    public void RoverMoveEventAddedEventListener(UnityAction<GameObject> listener)
    {
        roverMoveEvent.AddListener(listener);
    }

    // When the event manager hears that the rover reached an encounter, pop up the dialog and choices
    void ShowEncounterInfo() {
        dialogBox.SetActive(true);
        Text dialogText = dialogBox.GetComponentInChildren<Text>();
        dialogText.text = currentEncounter.mainDialog;
        choiceButton1.SetActive(true);
        Text choice1Text = choiceButton1.GetComponentInChildren<Text>();
        choice1Text.text = currentEncounter.choice1Text;
        choiceButton2.SetActive(true);
        Text choice2Text = choiceButton2.GetComponentInChildren<Text>();
        choice2Text.text = currentEncounter.choice2Text;
    }

    // When the event manager hears that a choice has been selected, act on it
    void EnactChoice(int choice)
    {
        // You have to do this or else the currentEncounter won't return the right stuff
        currentEncounter.MakeChoice(choice);

        dialogBox.SetActive(true);
        Text dialogText = dialogBox.GetComponentInChildren<Text>();
        dialogText.text = currentEncounter.GetResolutionText();
        choiceButton1.SetActive(false);
        choiceButton2.SetActive(false);

        playerFuel += currentEncounter.GetFuelOutcome();
        playerDurability += currentEncounter.GetDurabilityOutcome();
        playerScrap += currentEncounter.GetScrapOutcome();
        playerResearch += currentEncounter.GetResearchOutcome();
        UpdateReadouts();

        // Wait for user to tap away the Resolution screen
        waitingForResolution = true;
        resolutionTapTimer.Run();
    }

    public void PlayerTappedOnScreen()
    {
        if (waitingForResolution && resolutionTapTimer.Finished)
        {
            dialogBox.SetActive(false);
            if (previousEncounter != null)
            {
                Object.Destroy(previousEncounter.gameObject);
            }
            previousEncounter = currentEncounter;
            //TODO RANDOMIZE THE ENCOUNTER INSTEAD OF JUST GOING THRU
            Vector3 encounterPosition = Camera.main.transform.position;
            encounterPosition.x += 850;
            encounterPosition.y -= 100;
            encounterPosition.z = 0;
            GameObject encounter = Instantiate(encounterPool1[encounterCount], encounterPosition, Quaternion.identity);
            currentEncounter = encounter.GetComponent<AdventureNode>();
            encounterCount += 1;
            roverMoveEvent.Invoke(currentEncounter.gameObject);

            if (toggleSpawnMoreGround)
            {
                Vector3 groundPosition = Camera.main.transform.position;
                groundPosition.x += 936;
                groundPosition.z = 0;
                Instantiate(groundPrefab, groundPosition, Quaternion.identity);
            }
            toggleSpawnMoreGround = !toggleSpawnMoreGround;

            Object.Destroy(resolutionTapTimer);
            resolutionTapTimer = gameObject.AddComponent<Timer>();
            resolutionTapTimer.Duration = 1;
            waitingForResolution = false;
        }
    }

    void UpdateReadouts()
    {
        fuelReadout.GetComponent<Text>().text = "Battery: " + playerFuel;
        durabilityReadout.GetComponent<Text>().text = "Durability: " + playerDurability;
        scrapReadout.GetComponent<Text>().text = "Scrap: " + playerScrap;
        researchReadout.GetComponent<Text>().text = "Research: " + playerResearch;
    }
}
