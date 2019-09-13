using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class EventManager
{
    #region lists
    //test lists
    static List<Rover> invokersForTestEvent = new List<Rover>();
    static List<UnityAction<int>> listenersForTestEvent = new List<UnityAction<int>>();

	//rover move lists
	static List<AdventureNode> invokersForRoverMoveEvent = new List<AdventureNode>();
	static List<UnityAction> listenersForRoverMoveEvent = new List<UnityAction>();
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
    
	#region rover move event
	public static void AddInvokerForRoverMoveEvent(AdventureNode invoker)
	{
		invokersForRoverMoveEvent.Add(invoker);
		foreach (UnityAction listener in listenersForRoverMoveEvent)
		{
			invoker.RoverMoveEventAddedEventListener(listener);
		}
	}

	public static void AddListenerForRoverMoveEvent(UnityAction handler)
	{
		listenersForRoverMoveEvent.Add(handler);
		foreach (AdventureNode adventureNode in invokersForRoverMoveEvent)
		{
			adventureNode.RoverMoveEventAddedEventListener(handler);
		}
	}

	#endregion
}