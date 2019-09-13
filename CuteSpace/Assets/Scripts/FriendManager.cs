using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendManager : MonoBehaviour
{
    public List<FriendClass> potentialFriendList;

    //[HideInInspector]
    public List<FriendClass> friendList;
    public List<GameObject> friendUIPositionList;
    public List<RawImage> friendUIImageList;
    // Start is called before the first frame update
    void Start()
    {
        InitializeLists();
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

            //Transform holderObject = friend.GetComponent<Transform>();
           // holderObject.transform.position = friendUIPositionList[temp].transform.position;

            temp++;
        }
    }

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

    public void RemoveAllFriends()
    {
        int temp = 0;
        foreach (FriendClass friend in friendList)
        {
            friendUIImageList[temp].enabled = false;
            friend.RemoveBenefit();
            friendList.Remove(friend);
            potentialFriendList.Add(friend);
            temp++;
        }
    }
}
