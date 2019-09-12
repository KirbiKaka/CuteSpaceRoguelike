using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rover : MonoBehaviour
{
    //Declare vars and const
    float roverSpeed;
    float eventLocationX = 8;
    TestEvent testEvent = new TestEvent();
    int testNumber = 5;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddInvokerForTestEvent(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.x < eventLocationX - 2)
        {
            transform.Translate(Time.deltaTime, 0, 0);
        }
        else
        {
            testEvent.Invoke(testNumber);
        }

    }

    public void TestEventAddedEventListener(UnityAction<int> listener)
    {
        testEvent.AddListener(listener);
    }
}
