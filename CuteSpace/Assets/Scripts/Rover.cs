using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Rover : MonoBehaviour
{
    //Declare vars and const
    float roverSpeed;
    float eventLocationX = 8;
    TestEvent testEvent = new TestEvent();
    int testNumber = 5;
    IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddInvokerForTestEvent(this);
        EventManager.AddListenerForRoverMoveEvent(handleRoverMoveEvent);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }

    public void handleRoverMoveEvent()
    {
        GameObject adventureNode = GameObject.FindGameObjectWithTag("StopHere");
        //Transform currentRoverPosition = gameObject.transform;
        Vector2 roverStartPostion = gameObject.transform.position;
        Vector2 currentAdventureNodePostion = adventureNode.transform.position;
        coroutine = MoveFromTo(gameObject.transform, roverStartPostion, currentAdventureNodePostion,1);
        StartCoroutine(coroutine);
    }

    public void TestEventAddedEventListener(UnityAction<int> listener)
    {
        testEvent.AddListener(listener);
    }


    IEnumerator MoveFromTo(Transform objectToMove, Vector2 a, Vector2 b, float speed)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
        testEvent.Invoke(testNumber);
    }
}
