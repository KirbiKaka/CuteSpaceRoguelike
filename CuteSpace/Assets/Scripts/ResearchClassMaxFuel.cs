using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchClassMaxFuel : ResearchClass
{

    public int maxFuelIncrease;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyBenefit()
    {

        GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
        Debug.Log("Max Fuel Before: " + tempManager.GetComponent<GameManager>().maxFuel);
        tempManager.GetComponent<GameManager>().AlterMaxFuel(maxFuelIncrease);
        Debug.Log("Max Fuel After: " + tempManager.GetComponent<GameManager>().maxFuel);
    }
}
