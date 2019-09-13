using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void LoadMinigame()
    {
        SceneManager.LoadScene("MiniGame");
    }

}
