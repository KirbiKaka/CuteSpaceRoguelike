﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Audio;

public class ResearchClass : MonoBehaviour
{
    public int researchCost;
    [HideInInspector]
    public bool isPurchased;
    public GameObject[] researchPrerequisites;

    public Texture defaultImage;
    public Texture unavailableImage;
    public RawImage currentImage;

    public Text displayNameText;
    public Text displayCostText;

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
        displayCostText.text = "Research: " + researchCost;
        displayNameText.text = researchName;
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
		if (CheckIfCanAfford())
		{
			if (!isPurchased)
			{
				GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
				tempManager.GetComponent<GameManager>().AlterResearchPoints(-researchCost);

                GameObject currencyTextReference = GameObject.FindGameObjectWithTag("CurrencyTracker");
                currencyTextReference.GetComponent<CurrencyTracker>().UpdateCurrency();

				isPurchased = true;
				SetUnavailable();
				displayCostText.text = "Purchased!";
				AudioManager.Instance.Play2DSound("UpgradeEvent");
				ApplyBenefit();
			}
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
