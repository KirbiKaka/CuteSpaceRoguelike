﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchClass : MonoBehaviour
{
    public int researchCost;
    [HideInInspector]
    public bool isPurchased;
    public GameObject[] researchPrerequisites;

    public Texture defaultImage;
    public Texture unavailableImage;
    public RawImage currentImage;
    private Text displayText;

    public string researchName;

    // Start is called before the first frame update
    void Start()
    {
        isPurchased = false;
        InitializeResearchImage();
        InitializeResearchLabels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeResearchLabels()
    {
        displayText = GetComponentInChildren<Text>();
        displayText.text = "Research: " + researchCost;
    }

    void InitializeResearchImage()
    {
        currentImage = gameObject.GetComponent<RawImage>();
        currentImage.texture = defaultImage;
    }

    public bool CheckIfCanAfford()
    {
  
        GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");

        //   ResearchClass[] compareResearchList = tempManager.GetComponent<GameManager>().acquiredResearchList;

        if (tempManager.GetComponent<GameManager>().researchPoints < researchCost)
        {
            return false;
        }
        else
        {
            return true;
        }
      //  else if (tempManager.GetComponent<GameManager>().acquiredResearch.Count == 0)
      //  {
       //     return true;
       // }
       // else if (tempManager.GetComponent<GameManager>().acquiredResearch.)
    }
    

    public void PurchaseResearch()
    {
        if (!isPurchased)
        {
            GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
            tempManager.GetComponent<GameManager>().AlterResearchPoints(-researchCost);
            isPurchased = true;
            SetUnavailable();
            displayText.text = "Purchased!";
            ApplyBenefit();
        }

    }

    public void SetUnavailable()
    {
        if (currentImage == null)
        {
            Debug.Log("problem with curent image");
        }
        currentImage.texture = unavailableImage;
    }

    public void SetAvailable()
    {
        if (currentImage == null)
        {
            Debug.Log("problem with curent image");
        }
        currentImage.texture = defaultImage;
    }

    public virtual void ApplyBenefit()

    {

    }
}
