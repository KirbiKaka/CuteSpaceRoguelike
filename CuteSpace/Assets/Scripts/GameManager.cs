using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private GameManager m_Instance;
    public GameManager Instance { get { return m_Instance; } }

    [HideInInspector]
    public int researchPoints;
    [HideInInspector]
    public int numberOfFriends;

    [HideInInspector]
    public int currentFuel;
    [HideInInspector]
    public int maxFuel;

    public int startingFuel;

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
    }

    //Alter Key Variables//////////////////////

    public void AlterNumberOfFriends(int amountToAlter)
    {
        numberOfFriends += amountToAlter;
    }

    public void AlterResearchPoints(int amountToAlter)
    {
        researchPoints -= amountToAlter;
    }

    public void AlterMaxFuel(int amountToAlter)
    {
        maxFuel += amountToAlter;
    }

    public void AlterCurrentFuel(int amountToAlter)
    {
        currentFuel += amountToAlter;
    }
}
