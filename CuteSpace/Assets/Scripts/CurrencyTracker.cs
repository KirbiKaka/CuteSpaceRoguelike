using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyTracker : MonoBehaviour
{
    public Text currencyText;

    // Start is called before the first frame update
    void Start()
    {
        //currencyText = GetComponent<Text>();
        UpdateCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCurrency()
    {
        GameObject tempManager = GameObject.FindGameObjectWithTag("GameController");
        if (tempManager != null)
        {
            currencyText.text = tempManager.GetComponent<GameManager>().researchPoints.ToString();
        }
        else
        {
            Debug.Log("There is no GameManager in this scene. Please add one.");
        }
    }
}
