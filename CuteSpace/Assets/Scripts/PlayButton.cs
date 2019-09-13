using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayButton : MonoBehaviour
{
    public Texture defaultButton;
    public Texture hoverButton;
    private RawImage currentButton;

    // Start is called before the first frame update
    void Start()
    {
        InitializeImage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void InitializeImage()
    {
        currentButton = gameObject.GetComponent<RawImage>();
        currentButton.texture = defaultButton;
    }

    public void MouseEnter()
    {
        currentButton.texture = hoverButton;
    }

    public void MouseExit()
    {
        currentButton.texture = defaultButton;
    }

    public void PlayGame()
    {
        GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
        if (tempManager != null)
        {
            tempManager.GetComponent<GameManager>().LoadMinigame();
        }
        else
        {
            Debug.Log("There is no GameManager in this scene. Please add one.");
        }
    }



}
