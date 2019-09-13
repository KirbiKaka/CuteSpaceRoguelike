using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureNodeButton : MonoBehaviour
{
    public Texture defaultButton;
    private RawImage currentButton;

    // Start is called before the first frame update
    void Start()
    {
        currentButton = gameObject.GetComponent<RawImage>();
        currentButton.texture = defaultButton;
    }

}
