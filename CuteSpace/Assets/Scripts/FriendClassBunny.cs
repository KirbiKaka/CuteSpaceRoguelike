using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendClassBunny : FriendClass
{
    [HideInInspector]
    public bool freeWildLifeEnergy;

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
        freeWildLifeEnergy = true;
    }

    public override void RemoveBenefit()
    {
        freeWildLifeEnergy = false;
    }
}
