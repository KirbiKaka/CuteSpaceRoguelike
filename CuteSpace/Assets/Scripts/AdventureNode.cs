using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdventureNode : MonoBehaviour
{
    RoverMoveEvent roverMoveEvent = new RoverMoveEvent();
    Timer moveWaitTimer;
    bool moveHasStarted;
    GameObject rover;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddListenerForTestEvent(handleTestEvent);
        EventManager.AddInvokerForRoverMoveEvent(this);
        moveWaitTimer = gameObject.AddComponent<Timer>();
        moveWaitTimer.Duration = 2;
        moveWaitTimer.Run();
    }

    // Update is called once per frame
    void handleTestEvent(int thing)
    {
        print(thing);
    }

    public void StartRoverMoveEvent()
    {
        roverMoveEvent.Invoke();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void RoverMoveEventAddedEventListener(UnityAction listener)
    {
        roverMoveEvent.AddListener(listener);
    }

    private void Update()
    {
        if (moveWaitTimer.Finished == true && moveHasStarted == false)
        {
            rover = GameObject.FindGameObjectWithTag("Player");
            rover.GetComponent<Rover>().handleRoverMoveEvent();
            moveHasStarted = true;            
        }
        
    }
}
