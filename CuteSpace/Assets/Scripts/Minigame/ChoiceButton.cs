using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceButton : MonoBehaviour
{
    // Define this in the Unity inspector
    public int choiceNumber;

    public Sprite defaultButton;
    public Sprite hoverButton;
    private SpriteRenderer currentButton;

    MakeChoiceEvent makeChoiceEvent = new MakeChoiceEvent();

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddInvokerForMakeChoiceEvent(this);
        currentButton = gameObject.GetComponent<SpriteRenderer>();
        currentButton.sprite = defaultButton;
    }

    public void MakeChoiceEventAddedEventListener(UnityAction<int> listener)
    {
        makeChoiceEvent.AddListener(listener);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void MouseDown()
    {
        currentButton.sprite = hoverButton;
    }

    public void MouseUp()
    {
        currentButton.sprite = defaultButton;
    }

    public void ChooseOption()
    {
        makeChoiceEvent.Invoke(choiceNumber);
    }
}
