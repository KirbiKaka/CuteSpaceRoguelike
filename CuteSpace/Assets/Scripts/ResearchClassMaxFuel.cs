﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchClassMaxFuel : ResearchClass
{

    public int maxFuelIncrease;

    public override void ApplyBenefit()
    {

        GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
        tempManager.GetComponent<GameManager>().AlterMaxFuel(maxFuelIncrease);
    }
}