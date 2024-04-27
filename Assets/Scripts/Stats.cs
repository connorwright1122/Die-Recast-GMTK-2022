using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int maxDamage, maxRange, maxWeaponRate, maxHealth, maxAbilityCharges, maxWeaponSpeed;
    public float maxMoveSpeed;
    public int goldCount;

    public Text damageText;
    public Text rangeText;
    public Text rateText;
    public Text healthText;
    public Text movementText;
    public Text forceText;

    public Button damageIncrease;
    public Button forceIncrease;
    public Button movementIncrease;
    public Button healthIncrease;

    //public bool goldChecked = false;

    private void Start()
    {
        InitializeMaxStats();
    }

    public void InitializeMaxStats()
    {
        maxDamage = 5;
        maxRange = 5;
        maxWeaponRate = 5;
        maxHealth = 5;
        maxAbilityCharges = 1;
        maxWeaponSpeed = 1;
        maxMoveSpeed = 10;

        goldCount = 0;

        damageText.text = maxDamage.ToString();

        forceText.text = maxWeaponSpeed.ToString();
    }


    private void Update()
    {
        CheckGoldCount();
        UpdateStats();
    }

    private void UpdateStats()
    {
        damageText.text = maxDamage.ToString();
        healthText.text = maxHealth.ToString();
        movementText.text = maxMoveSpeed.ToString();
        forceText.text = maxWeaponSpeed.ToString();
    }

    private void CheckGoldCount()
    {
        if (goldCount <= 0)
        {
            damageIncrease.interactable = false;
            forceIncrease.interactable = false;
            movementIncrease.interactable = false;
            healthIncrease.interactable = false;
        }
        else 
        {
            damageIncrease.interactable = true;
            forceIncrease.interactable = true;
            movementIncrease.interactable = true;
            healthIncrease.interactable = true;
        }
    }

    public void IncreaseMaxStatByOne(string maxType)//, int num)
    {

        if (goldCount > 0)
        {
            switch (maxType)
            {
                case "maxDamage":
                    maxDamage++;
                    break;
                case "maxRange":
                    maxRange++;
                    break;
                case "maxWeaponRate":
                    maxWeaponRate++;
                    break;
                case "maxHealth":
                    maxHealth++;
                    break;
                case "maxAbilityCharges":
                    maxAbilityCharges++;
                    break;
                case "maxWeaponSpeed":
                    maxWeaponSpeed++;
                    break;
                case "maxMoveSpeed":
                    maxMoveSpeed++;
                    break;


            }

            goldCount--;
        }
    }

    /*
    public void DecreaseMaxStatByOne(string maxType)//, int num)
    {
        
        if (goldCount > 0)
        {
            switch (maxType)
            {
                case "maxDamage":
                    maxDamage--;
                    break;
                case "maxRange":
                    maxRange--;
                    break;
                case "maxWeaponRate":
                    maxWeaponRate--;
                    break;
                case "maxHealth":
                    maxHealth--;
                    break;
                case "maxAbilityCharges":
                    maxAbilityCharges--;
                    break;
                case "maxWeaponSpeed":
                    maxWeaponSpeed--;
                    break;
                case "maxMoveSpeed":
                    maxMoveSpeed--;
                    break;


            }

            goldCount++;
        }

        
        

        damageText.text = maxDamage.ToString();
    }
    */
    
    
    public void IncreaseMaxStatByAll(string maxType)//, int num)
    {
        int num = goldCount;
        if (goldCount > 0)
        {
            switch (maxType)
            {
                case "maxDamage":
                    maxDamage += num;
                    break;
                case "maxRange":
                    maxRange += num;
                    break;
                case "maxWeaponRate":
                    maxWeaponRate += num;
                    break;
                case "maxHealth":
                    maxHealth += num;
                    break;
                case "maxAbilityCharges":
                    maxAbilityCharges += num;
                    break;
                case "maxWeaponSpeed":
                    maxWeaponSpeed += num;
                    break;
                case "maxMoveSpeed":
                    maxMoveSpeed += num;
                    break;


            }

            goldCount = 0;
        }
        

        //damageText.text = maxDamage.ToString();
    }

    public void IncreaseGold(int num)
    {
        goldCount += num;
    }


}
