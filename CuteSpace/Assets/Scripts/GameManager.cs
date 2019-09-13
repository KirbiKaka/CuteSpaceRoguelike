using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Audio;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // private GameManager m_Instance;
    //public GameManager Instance { get { return m_Instance; } }

    //public Text currencyDisplayText;

    [HideInInspector]
    public float researchPoints;
    [HideInInspector]
    public int numberOfFriends;

    [HideInInspector]
    public int maxFuel = 3;

    [HideInInspector]
    public int maxDurability = 3;

    [HideInInspector]
    public float researchGainMultiplier;

    [HideInInspector]
    public List<GameObject> acquiredResearch;



    private void Awake()
    {
        //m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeCurrency();
        LoadMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    //Initializej Key Variables/////////////////////

    private void InitializeCurrency()
    {
        researchPoints = 0;
        numberOfFriends = 0;
        researchGainMultiplier = 0;
        maxFuel = 3;
        maxDurability = 3;
    }

    //Alter Key Variables//////////////////////

    public void AlterNumberOfFriends(int amountToAlter)
    {
        numberOfFriends += amountToAlter;
    }

    public void AlterResearchModifier(float amountToAlter)
    {
        researchGainMultiplier += amountToAlter;
        Debug.Log("new research modifier:" + researchGainMultiplier);
    }

    public void AlterResearchPoints(int amountToAlter)
    {
        if (amountToAlter > 0)
        {
            Mathf.RoundToInt(researchPoints += (amountToAlter * (1 + researchGainMultiplier)));
        }
        else
        {
            researchPoints += amountToAlter;
        }
    }

    public void AlterMaxFuel(int amountToAlter)
    {
        maxFuel += amountToAlter;
    }

    public void LoadMinigame()
    {
        SceneManager.LoadScene("MiniGame");
        AudioManager.Instance.CrossfadeMusic("MainMenu", 1);
    }

    public void LoadMainMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "preLoadScene")
        {
            AudioManager.Instance.CrossfadeMusic("Explore", .1f);
        }
        else
        {
            AudioManager.Instance.CrossfadeMusic("Explore", 1);
        }
        SceneManager.LoadScene("MainMenu");

    }
}
