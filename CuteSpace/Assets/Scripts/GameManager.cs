using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int researchPoints;
    [HideInInspector]
    public int numberOfFriends;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeCurrency()
    {
        researchPoints = 0;
        numberOfFriends = 0;
    }

    public void addResearchPoints(int amountToAdd)
    {
        researchPoints += amountToAdd;
    }

    public void addNumberOfFriends(int amountToAdd)
    {
        numberOfFriends += amountToAdd;
    }

    public void decreaseResearchPoints(int amountToDecrease)
    {
        researchPoints -= amountToDecrease;
    }

    public void decreaseNumberOfFriends(int amountToDecrease)
    {
        researchPoints -= amountToDecrease;
    }
}
