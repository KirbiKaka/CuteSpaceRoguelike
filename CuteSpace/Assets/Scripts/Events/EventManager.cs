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

    static List<Rover> invokersForRoverReachedEncounter = new List<Rover>();
    static List<UnityAction> listenersForRoverReachedEncounter = new List<UnityAction>();

    static List<ChoiceButton> invokersForMakeChoiceEvent = new List<ChoiceButton>();
    static List<UnityAction<int>> listenersForMakeChoiceEvent = new List<UnityAction<int>>();
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

    #region rover reaches an encounter
    public static void AddInvokerForRoverReachedEncounter(Rover invoker)
    {
        invokersForRoverReachedEncounter.Add(invoker);
        foreach (UnityAction listener in listenersForRoverReachedEncounter)
        {
            invoker.RoverReachedEncounterAddedEventListener(listener);
        }
    }

    public static void AddListenerForRoverReachedEncounter(UnityAction handler)
    {
        listenersForRoverReachedEncounter.Add(handler);
        foreach (Rover rover in invokersForRoverReachedEncounter)
        {
            rover.RoverReachedEncounterAddedEventListener(handler);
        }
    }
    #endregion

    #region make choice event 
    public static void AddInvokerForMakeChoiceEvent(ChoiceButton invoker)
    {
        invokersForMakeChoiceEvent.Add(invoker);
        foreach (UnityAction<int> listener in listenersForMakeChoiceEvent)
        {
            invoker.MakeChoiceEventAddedEventListener(listener);
        }
    }

    public static void AddListenerForMakeChoiceEvent(UnityAction<int> handler)
    {
        listenersForMakeChoiceEvent.Add(handler);
        foreach (ChoiceButton button in invokersForMakeChoiceEvent)
        {
            button.MakeChoiceEventAddedEventListener(handler);
        }
    }
    #endregion
}