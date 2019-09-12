using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureNode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddListenerForTestEvent(handleTestEvent);
    }

    // Update is called once per frame
    void handleTestEvent(int thing)
    {
        print(thing);
    }
}
