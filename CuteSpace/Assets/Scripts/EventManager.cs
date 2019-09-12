using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class EventManager
{
    #region lists
    static List<Rover> invokersForTestEvent = new List<Rover>();
    static List<UnityAction<int>> listenersForTestEvent = new List<UnityAction<int>>();

    #endregion


    #region test event 
    public static void AddInvokerForTestEvent(Rover invoker)
    {
        invokersForTestEvent.Add(invoker);
        foreach (UnityAction<int> listener in listenersForTestEvent)
        {
            invoker.TestEventAddedEventListener(listener);
        }
    }

    public static void AddListenerForTestEvent(UnityAction<int> handler)
    {
        listenersForTestEvent.Add(handler);
        foreach (Rover rover in invokersForTestEvent)
        {
            rover.TestEventAddedEventListener(handler);
        }
    }
#endregion
}