using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendClassBunny : FriendClass
{

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
        EnableFriend();
    }

    public override void RemoveBenefit()
    {
        DisableFriend();
    }
}
