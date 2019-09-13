using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendClass : MonoBehaviour
{
    public Texture friendImage;
    public string friendName;
    public string friendDescription;

    public int friendID;

    private bool hasFriend;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReturnFriendStatus()
    {
        return hasFriend;
    }

    public void EnableFriend()
    {
        hasFriend = true;
    }

    public void DisableFriend()
    {
        hasFriend = false;
    }

    public virtual void ApplyBenefit()
    {

    }

    public virtual void RemoveBenefit()
    {

    }
}
