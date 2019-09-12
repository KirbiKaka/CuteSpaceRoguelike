﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{

    public GameObject shop;
    public GameObject mainMenu;
    public GameObject researchMenu;

    private void Start()
    {
        InitializeShop();
        InitializeMainMenu();
        InitializeResearchMenu();
    }

    private void InitializeMainMenu()
    {
        mainMenu.SetActive(true);
    }

    private void InitializeResearchMenu()
    {
        researchMenu.SetActive(false);
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

    public void DisplayResearch()
    {
        researchMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseResearch()
    {
        researchMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void CheckIfCanBuy()
    {
        GameObject[] tempList = GameObject.FindGameObjectsWithTag("Research");

        foreach (GameObject t in tempList)
        {
            ResearchClass tempResearch = t.GetComponent<ResearchClass>();

            if (tempResearch.CheckIfCanAfford() & !tempResearch.isPurchased)
            {
                tempResearch.SetAvailable();
            }
            else
            {
                tempResearch.SetUnavailable();
            }
        }
    }

}
