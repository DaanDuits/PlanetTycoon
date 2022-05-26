using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public bool cantBuildDestroy = false;

    public bool[] buildings;
    public bool[] specials;
    public bool[] produce;

    public GameObject[] buttonHolders;

    private void Start()
    {
        for (int i = 0; i < buttonHolders.Length; i++)
        {
            buttonHolders[i].SetActive(false);
        }
    }

    public void SetSpecial(int i)
    {
        for (int j = 0; j < buildings.Length; j++)
        {
            buildings[j] = false;
        }
        for (int j = 0; j < buttonHolders.Length; j++)
        {
            buttonHolders[j].SetActive(false);
        }
        for (int j = 0; j < specials.Length; j++)
        {
           specials[j] = false;
        }
        specials[i] = true;
    }
    public void SetSlotHolder(int i)
    {
        if (!buttonHolders[i].activeSelf)
        {
            for (int j = 0; j < buttonHolders.Length; j++)
            {
                buttonHolders[j].SetActive(false);
            }
            buttonHolders[i].SetActive(true);
        }
        else
        {
            buttonHolders[i] .SetActive(false);
        }
    }

    public void SetBuilding(int i)
    {
        for (int j = 0; j < buttonHolders.Length; j++)
        {
            buttonHolders[j].SetActive(false);
        }
        cantBuildDestroy = false;
        for (int j = 0; j < buildings.Length; j++)
        {
            buildings[j] = false;
        }
        for (int j = 0; j < specials.Length; j++)
        {
            specials[j] = false;
        }
        buildings[i] = true;
    }
    public void SetProduce(int i)
    {
        for (int j = 0; j < buttonHolders.Length; j++)
        {
            buttonHolders[j].SetActive(false);
        }
        cantBuildDestroy = false;
        for (int j = 0; j < produce.Length; j++)
        {
            produce[j] = false;
        }
        for (int j = 0; j < specials.Length; j++)
        {
            specials[j] = false;
        }
        produce[i] = true;
    }
}
