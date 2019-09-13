using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchClassResearchModifierIncrease : ResearchClass
{
    public float researchModifierIncrease;

    public override void ApplyBenefit()
    {
        GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
        tempManager.GetComponent<GameManager>().AlterResearchModifier(researchModifierIncrease);
    }
}
