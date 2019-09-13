using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendManager : MonoBehaviour
{
    public List<FriendClass> potentialFriendList;

    private bool displayingDescription;

    //[HideInInspector]
    public List<FriendClass> friendList;
    public List<GameObject> friendUIPositionList;
    public List<RawImage> friendUIImageList;
    public List<Text> friendUITooltipList;

    public FriendClass testFriendRequirement;
    // Start is called before the first frame update
    void Start()
    {
        InitializeLists();
        displayingDescription = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AddFriend("Energizer Bunny");
        }

        if (Input.GetKeyDown("up"))
        {
            AddFriend("Efficient Memory");
        }
        /*

        if (Input.GetKeyDown("down"))
        {
            //Debug.Log("Friend you're looking for " + testFriendRequirement.friendName);
            if (CheckForFriend(testFriendRequirement))
            {
                Debug.Log("You have Bunny");
            }
            else
            {
                Debug.Log("You don't have Bunny");
            }
        }
        */
    }

    private void InitializeLists()
    {
        friendList = new List<FriendClass>();
        /*
        foreach (FriendClass friend in potentialFriendList)
        {
            friend.friendImage.enabled = false;
        }
        */

        //friendUIPositionList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            friendUIPositionList.Add(child.gameObject);
        }

        foreach (Text text in friendUITooltipList)
        {
            text.text = "";
            text.enabled = false;
        }

        //potentialFriendList = new List<FriendClass>();

        friendUIImageList = new List<RawImage>();

        foreach (GameObject friend in friendUIPositionList)
        {
            RawImage tempTexture = friend.GetComponent<RawImage>();
            tempTexture.enabled = false;
            friendUIImageList.Add(tempTexture);
        }


    }

    public void AddFriend(string name)
    {
        Debug.Log("Entered Add Friend Function");
        List<FriendClass> tempFriendList = new List<FriendClass>();
        foreach (FriendClass friend in potentialFriendList)
        {
            tempFriendList.Add(friend);
        }

        foreach (FriendClass friend in tempFriendList)
        {
            Debug.Log("Entered Foreach loop");
            if (friend.friendName == name)
            {
                
                friendList.Add(friend);
                friend.ApplyBenefit();
                potentialFriendList.Remove(friend);
                Debug.Log(name);
            }
        }

        Debug.Log("Added Friend Successfully");
        UpdateFriendDisplay();
        
        
    }

    public void UpdateFriendDisplay()
    {
        int temp = 0;
        foreach (FriendClass friend in friendList)
        {
            Debug.Log("Friend Count:" + temp);

            //friendUIPositionList[temp]

            friendUIImageList[temp].texture = friend.friendImage;
            friendUIImageList[temp].enabled = true;
            friendUITooltipList[temp].text = friend.friendDescription;

            //Transform holderObject = friend.GetComponent<Transform>();
           // holderObject.transform.position = friendUIPositionList[temp].transform.position;

            temp++;
        }
    }
    /*
    public bool CheckForEnergizer()
    {
        foreach (FriendClass friend in friendList)
        {
            if (friend.GetComponent<FriendClassBunny>() != null)
            {
                if (friend.GetComponent<FriendClassBunny>().freeWildLifeEnergy)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        return false;
    }
    */

    public bool CheckForFriend(FriendClass friend)
    {
        foreach (FriendClass myFriend in friendList)
        {
            if (friend.friendID == myFriend.friendID)
            {
                if (friend.ReturnFriendStatus())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        return false;

    }


    public void RemoveAllFriends()
    {
        int temp = 0;
        foreach (FriendClass friend in friendList)
        {
            friendUIImageList[temp].enabled = false;
            friend.RemoveBenefit();
            friendList.Remove(friend);
            potentialFriendList.Add(friend);
            friendUITooltipList[temp].text = "";
            temp++;
        }
    }

    public void DisplayDescription(RawImage friendUIImage)
    {

        if (friendUIImage.enabled)
        {
            Text tempText = friendUIImage.GetComponentInChildren<Text>();
            if (friendUITooltipList.Contains(tempText))
            {
                tempText.enabled = true;
                displayingDescription = true;
            }
        }


    }

    public void HideDescription(RawImage friendUIImage)
    {
        Text tempText = friendUIImage.GetComponentInChildren<Text>();
        tempText.enabled = false;
    }
}
