using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Audio;


public class GameManager : MonoBehaviour
{
    private GameManager m_Instance;
    public GameManager Instance { get { return m_Instance; } }

    [HideInInspector]
    public float researchPoints;
    [HideInInspector]
    public int numberOfFriends;

    [HideInInspector]
    public int currentFuel;
    [HideInInspector]
    public int maxFuel;

    public int startingFuel;

    [HideInInspector]
    public float researchGainMultiplier;

    [HideInInspector]
    public List<GameObject> acquiredResearch;



    private void Awake()
    {
        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeCurrency();
        InitializeFuel();
        AudioManager.Instance.Play2DSound("Explore", true, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    //Initializej Key Variables/////////////////////

    private void InitializeFuel()
    {
        maxFuel = startingFuel;
        currentFuel = maxFuel;
    }

    private void InitializeCurrency()
    {
        researchPoints = 0;
        numberOfFriends = 0;
        researchGainMultiplier = 0;
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
            researchPoints += (amountToAlter * (1 + researchGainMultiplier));
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

    public void AlterCurrentFuel(int amountToAlter)
    {
        currentFuel += amountToAlter;
    }

    public void LoadMinigame()
    {
        SceneManager.LoadScene("MiniGame");
        AudioManager.Instance.CrossfadeMusic("MainMenu", 1);
    }
}
