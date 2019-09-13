using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Rover : MonoBehaviour
{
    // How far away from the node to stop
    public int ADVENTURE_NODE_OFFSET_DISTANCE = 340;
    public int ROVER_WALK_SPEED = 120;

    //Declare vars and const
    TestEvent testEvent = new TestEvent();
    RoverReachedEncounter roverReachedEncounterEvent = new RoverReachedEncounter();
    IEnumerator coroutine;

    Animator animator;
    const int IDLE_ANIMATION_STATE = 0;
    const int RUN_ANIMATION_STATE = 1;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddInvokerForTestEvent(this);
        EventManager.AddInvokerForRoverReachedEncounter(this);
        EventManager.AddListenerForRoverMoveEvent(handleRoverMoveEvent);
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }

    public void handleRoverMoveEvent(GameObject adventureNode)
    {
        //Transform currentRoverPosition = gameObject.transform;
        Vector2 roverStartPosition = gameObject.transform.position;
        Vector2 currentAdventureNodePosition = adventureNode.transform.position;
        // Don't move the Rover up or down
        currentAdventureNodePosition.y = roverStartPosition.y;
        // Don't walk aaaaall the way up to the adventure node
        currentAdventureNodePosition.x -= ADVENTURE_NODE_OFFSET_DISTANCE;
        coroutine = MoveFromTo(gameObject.transform, roverStartPosition, currentAdventureNodePosition, ROVER_WALK_SPEED);
        StartCoroutine(coroutine);
        animator.SetInteger("state", RUN_ANIMATION_STATE);
    }

    public void TestEventAddedEventListener(UnityAction<int> listener)
    {
        testEvent.AddListener(listener);
    }

    public void RoverReachedEncounterAddedEventListener(UnityAction listener)
    {
        roverReachedEncounterEvent.AddListener(listener);
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
        roverReachedEncounterEvent.Invoke();
        animator.SetInteger("state", IDLE_ANIMATION_STATE);
    }
}
