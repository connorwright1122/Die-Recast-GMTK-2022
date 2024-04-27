using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Enemy enemy;
    public float currentEnemyHealth;
    public float maxEnemyHealth;

    public CombatController pc;
    public float currentPlayerHealth;
    public float maxPlayerHealth;

    public Stats st;

    public Image enemyHealthbar;
    public Image playerHealthbar;

    public Text enemyHealthText;
    public Text playerHealthText;
    public Text goldText;

    private void Start()
    {
        pc = FindObjectOfType<CombatController>();
        enemy = FindObjectOfType<Enemy>();
        st = FindObjectOfType<Stats>();
    }

    private void Update()
    {
        currentEnemyHealth = enemy.health;
        maxEnemyHealth = enemy.maxHealth;
        enemyHealthbar.fillAmount = currentEnemyHealth / maxEnemyHealth;
        enemyHealthText.text = currentEnemyHealth.ToString() + "/" + maxEnemyHealth.ToString();
        

        currentPlayerHealth = pc.health;
        maxPlayerHealth = st.maxHealth;
        playerHealthbar.fillAmount = currentPlayerHealth / maxPlayerHealth;
        playerHealthText.text = currentPlayerHealth.ToString() + "/" + maxPlayerHealth.ToString();

        goldText.text = st.goldCount.ToString() + " GP";
    }
}
