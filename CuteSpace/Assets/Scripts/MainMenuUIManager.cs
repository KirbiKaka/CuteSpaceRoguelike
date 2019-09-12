using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{

    public GameObject shop;
    public GameObject mainMenu;

    private void Start()
    {
        InitializeShop();
        InitializeMainMenu();
    }

    private void InitializeMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void InitializeShop()
    {
        shop.SetActive(false);
    }

    public void DisplayShop()
    {
        shop.SetActive(true);
        mainMenu.SetActive(false);
    }



    public void CloseShop()
    {
        shop.SetActive(false);
        mainMenu.SetActive(true);
    }
}
