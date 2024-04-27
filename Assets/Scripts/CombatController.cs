using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatController : MonoBehaviour
{
    public int damage, range, weaponRate, health, abilityCharges, weaponSpeed, diceType;
    //public int maxDamage, maxRange, maxWeaponRate, maxHealth, maxAbilityCharges, maxWeaponSpeed;
    //public Dictionary<int, int> statsDictionary;
    //public List<int> statsList;

    //[SerializeField] public TextMeshPro statsText;
    //[SerializeField] public Text statsText;

    public Transform firePoint;
    public GameObject weaponPrefab;
    public GameObject d1Prefab;
    public GameObject d2Prefab;

    public Stats stats;
    public TopDownMovement pm;

    public bool isDead = false;

    private void Start()
    {
        //statsDictionary = new Dictionary<int, int>();
        //statsList = new List<int>();
        //AddStats();
        stats = GameObject.FindObjectOfType<Stats>();
        pm = GetComponent<TopDownMovement>();
        //pm.RandomizeStats();
        RandomizeDiceStats();
        RandomizePlayerStats();
    }

    

    /*
    public void RandomizeStatsTesting()
    {
        damage = UnityEngine.Random.Range(1, stats.maxDamage + 1);
        range = UnityEngine.Random.Range(1, stats.maxRange + 1);
        weaponRate = UnityEngine.Random.Range(1, stats.maxWeaponRate + 1);
        health = UnityEngine.Random.Range(1, stats.maxHealth + 1);
        abilityCharges = UnityEngine.Random.Range(1, stats.maxAbilityCharges + 1);
        weaponSpeed = UnityEngine.Random.Range(1, stats.maxWeaponSpeed + 1);

        statsText.text = damage.ToString();

        //dice stats
        //damage (min-max)
        //range
        //attack speed

        //player stats
        //health
        //movement speed
    }
    */
    
    public void RandomizeDiceStats()
    {
        
        //statsText.text = damage.ToString();

        //dice stats
        //damage (min-max)
        damage = UnityEngine.Random.Range(1, stats.maxDamage + 1);
        //range
        range = UnityEngine.Random.Range(1, stats.maxRange + 1);
        //attack rate
        weaponRate = UnityEngine.Random.Range(1, stats.maxWeaponRate + 1);
        //dice speed
        weaponSpeed = UnityEngine.Random.Range(1, stats.maxWeaponSpeed + 1);

        diceType = UnityEngine.Random.Range(1, 3);

        //player stats
        //health
        //movement speed
    }

    public void RandomizePlayerStats()
    {
        //pm.RandomizeStats();
        health = UnityEngine.Random.Range(1, stats.maxHealth + 1);
    }


    public void Attack()
    {
        RandomizeDiceStats();

        switch (diceType)
        {
            case 1:
                //GameObject newWeapon1 = Instantiate(d1Prefab, firePoint.position, firePoint.rotation);
                //newWeapon1.GetComponentInChildren<Weapon>().SetWeaponStats(damage, range, weaponRate, weaponSpeed, diceType);
                CreateD1();
                break;
            case 2:
                GameObject newWeapon2 = Instantiate(d2Prefab, firePoint.position, firePoint.rotation);
                newWeapon2.GetComponentInChildren<Weapon>().SetWeaponStats(damage, range, weaponRate, weaponSpeed, diceType);
                break;
        }
        //var newWeapon = Instantiate(weaponPrefab, firePoint.position, firePoint.rotation);
        //newWeapon.GetComponent<Weapon>().SetWeaponStats(damage, range, weaponRate, weaponSpeed, diceType);
    }

    public void CreateD1()
    {
        var newWeapon = Instantiate(d1Prefab, firePoint.position, firePoint.rotation);
        newWeapon.GetComponent<Weapon>().SetWeaponStats(damage, range, weaponRate, weaponSpeed, diceType);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
        }
    }

    public void ResetPlayerCombat()
    {
        //
        RandomizePlayerStats();
        isDead = false;
        //Debug.Log("dead");
    }
}
