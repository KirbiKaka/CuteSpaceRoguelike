using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Scripts.Audio;

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
    int playerFuelMax = 3;
    int playerDurability;
    int playerDurabilityMax = 3;
    int playerScrap;
    int playerResearch;

    int encounterCount;
    int prevEncounterCount;

    Timer startGameTimer;
    bool gameStarted;
    AdventureNode currentEncounter;
    AdventureNode previousEncounter;
    bool waitingForResolution;
    Timer resolutionTapTimer;
    bool toggleSpawnMoreGround = true;
    const int SHOW_RESOURCE_CHANGE_DURATION = 1;
    Timer showResourceChangeTimer;
    bool resourceChangeVisible;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddListenerForRoverReachedEncounter(ShowEncounterInfo);
        EventManager.AddListenerForMakeChoiceEvent(EnactChoice);
        EventManager.AddInvokerForRoverMoveEvent(this);

        // TODO: inc playerFuelMax based on supergame upgrades
        GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
        if (tempManager != null)
        {
            playerFuelMax = tempManager.GetComponent<GameManager>().maxFuel;
            playerFuel = playerFuelMax;
        }
        else
        {
            playerFuel = playerFuelMax;
            Debug.Log("There is no GameManager in this scene. Please add one.");
        }

        // TODO: inc playerDurabilityMax based on supergame upgrades
        playerDurability = playerDurabilityMax;
        UpdateReadouts();

        startGameTimer = gameObject.AddComponent<Timer>();
        startGameTimer.Duration = 2;
        startGameTimer.Run();

        resolutionTapTimer = gameObject.AddComponent<Timer>();
        resolutionTapTimer.Duration = 1;

        showResourceChangeTimer = gameObject.AddComponent<Timer>();
        showResourceChangeTimer.Duration = SHOW_RESOURCE_CHANGE_DURATION;
    }

    // Update is called once per frame
    void Update()
    {
        if (startGameTimer.Finished == true && gameStarted == false)
        {
            gameStarted = true;

            Vector3 encounterPosition = Camera.main.transform.position;
            encounterPosition.x += 820;
            encounterPosition.y -= 100;
            encounterPosition.z = 0;
            encounterCount = Random.Range(0, encounterPool1.Count);
            prevEncounterCount = encounterCount;
            GameObject encounter = Instantiate(encounterPool1[encounterCount], encounterPosition, Quaternion.identity);
            currentEncounter = encounter.GetComponent<AdventureNode>();
            roverMoveEvent.Invoke(currentEncounter.gameObject);
        }

        if (Input.GetMouseButtonUp(0))
        {
            PlayerTappedOnScreen();
        }

        if (resourceChangeVisible && showResourceChangeTimer.Finished)
        {
            fuelReadout.transform.GetChild(0).gameObject.SetActive(false);
            durabilityReadout.transform.GetChild(0).gameObject.SetActive(false);
            scrapReadout.transform.GetChild(0).gameObject.SetActive(false);
            researchReadout.transform.GetChild(0).gameObject.SetActive(false);
            Object.Destroy(showResourceChangeTimer);
            showResourceChangeTimer = gameObject.AddComponent<Timer>();
            showResourceChangeTimer.Duration = SHOW_RESOURCE_CHANGE_DURATION;
            resourceChangeVisible = false;
        }
    }

    public void RoverMoveEventAddedEventListener(UnityAction<GameObject> listener)
    {
        roverMoveEvent.AddListener(listener);
    }

    // When the event manager hears that the rover reached an encounter, pop up the dialog and choices
    void ShowEncounterInfo()
    {
        AudioManager.Instance.Play2DSound("PopupAppear");
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
        AudioManager.Instance.Play2DSound("ButtonPress");
        dialogBox.SetActive(true);
        Text dialogText = dialogBox.GetComponentInChildren<Text>();
        dialogText.text = currentEncounter.GetResolutionText();
        choiceButton1.SetActive(false);
        choiceButton2.SetActive(false);

        ChangeFuel(currentEncounter.GetFuelOutcome());
        ChangeDurability(currentEncounter.GetDurabilityOutcome());
        ChangeScrap(currentEncounter.GetScrapOutcome());
        ChangeResearch(currentEncounter.GetResearchOutcome());
        UpdateReadouts();

        // Wait for user to tap away the Resolution screen
        waitingForResolution = true;
        resolutionTapTimer.Run();
    }

    public void PlayerTappedOnScreen()
    {
        // Start a new encounter
        if (waitingForResolution && resolutionTapTimer.Finished)
        {
            //if the player isn't dead
            if (playerFuel <= 0 || playerDurability <= 0)
            {
                GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
                if (tempManager != null)
                {
                    tempManager.GetComponent<GameManager>().AlterResearchPoints(playerResearch);
                    tempManager.GetComponent<GameManager>().LoadMainMenu();
                }
                else
                {
                    Debug.Log("There is no GameManager in this scene. Please add one.");
                }
            }
            else
            {
                AudioManager.Instance.Play2DSound("ButtonPress");
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
                encounterCount = Random.Range(0, encounterPool1.Count);

                //check for dupe encounters 
                if (encounterCount == prevEncounterCount && encounterCount != encounterPool1.Count - 1)
                {
                    if (encounterCount != encounterPool1.Count - 1)
                    {
                        encounterCount += 1;

                    }
                    else
                    {
                        encounterCount = 0;
                    }
                }
                
                GameObject encounter = Instantiate(encounterPool1[encounterCount], encounterPosition, Quaternion.identity);
                currentEncounter = encounter.GetComponent<AdventureNode>();
                roverMoveEvent.Invoke(currentEncounter.gameObject);
                prevEncounterCount = encounterCount;
                // Every event costs 1 fuel
                ChangeFuel(-1);
                UpdateReadouts();

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
    }

    void UpdateReadouts()
    {
        fuelReadout.GetComponent<Text>().text = "Battery: " + playerFuel + "/" + playerFuelMax;
        durabilityReadout.GetComponent<Text>().text = "Durability: " + playerDurability + "/" + playerDurabilityMax;
        scrapReadout.GetComponent<Text>().text = "Scrap: " + playerScrap;
        researchReadout.GetComponent<Text>().text = "Research: " + playerResearch;
    }

    void ChangeFuel(int change)
    {
        if (change != 0)
        {
            playerFuel += change;
            UpdateReadouts();
            GameObject changeReadoutObject = fuelReadout.transform.GetChild(0).gameObject;
            Text changeReadout = changeReadoutObject.GetComponent<Text>();
            changeReadoutObject.SetActive(true);
            if (change > 0)
            {
                changeReadout.color = Color.green;
                changeReadout.text = "+" + change;
            }
            else if (change < 0)
            {
                changeReadout.color = Color.red;
                changeReadout.text = "" + change;
            }
            resourceChangeVisible = true;
            showResourceChangeTimer.Run();
        }
    }

    void ChangeDurability(int change)
    {
        if (change != 0)
        {
            playerDurability += change;
            UpdateReadouts();
            GameObject changeReadoutObject = durabilityReadout.transform.GetChild(0).gameObject;
            Text changeReadout = changeReadoutObject.GetComponent<Text>();
            changeReadoutObject.SetActive(true);
            if (change > 0)
            {
                changeReadout.color = Color.green;
                changeReadout.text = "+" + change;
            }
            else if (change < 0)
            {
                changeReadout.color = Color.red;
                changeReadout.text = "" + change;
            }
            resourceChangeVisible = true;
            showResourceChangeTimer.Run();
        }
    }

    void ChangeScrap(int change)
    {
        if (change != 0)
        {
            playerScrap += change;
            UpdateReadouts();
            GameObject changeReadoutObject = scrapReadout.transform.GetChild(0).gameObject;
            Text changeReadout = changeReadoutObject.GetComponent<Text>();
            changeReadoutObject.SetActive(true);
            if (change > 0)
            {
                changeReadout.color = Color.green;
                changeReadout.text = "+" + change;
            }
            else if (change < 0)
            {
                changeReadout.color = Color.red;
                changeReadout.text = "" + change;
            }
            resourceChangeVisible = true;
            showResourceChangeTimer.Run();
        }
    }

    void ChangeResearch(int change)
    {
        if (change != 0)
        {
            playerResearch += change;
            UpdateReadouts();
            GameObject changeReadoutObject = researchReadout.transform.GetChild(0).gameObject;
            Text changeReadout = changeReadoutObject.GetComponent<Text>();
            changeReadoutObject.SetActive(true);
            if (change > 0)
            {
                changeReadout.color = Color.green;
                changeReadout.text = "+" + change;
            }
            else if (change < 0)
            {
                changeReadout.color = Color.red;
                changeReadout.text = "" + change;
            }
            resourceChangeVisible = true;
            showResourceChangeTimer.Run();
        }
    }
}
