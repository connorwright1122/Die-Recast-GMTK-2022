using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    public int maxHealth = 100;
    public int health;

    public Stats stats;


    public int bounceMax = 5;
    public int bounceCurrent;

    private Rigidbody rb;
    private Vector3 lastVelocity;

    private bool rollActive;

    public float lookSpeed = 5f;
    public Transform target;

    //public float timePassed;
    public float moveSpeed = 5f;

    public bool timerStarted = false;
    public bool timerDone = false;

    public float lookLength = 3f;
    private float lastLook = -999f;
    private float savedTime;


    private Vector3 newVelocity;

    public Transform stable;

    //public GameObject


    Cinemachine.CinemachineImpulseSource impulse;

    public int prevDice;
    public int consecutiveDice = 0;
    public int diceBoost = 3;

    public Text popupText;
    //public Image healthbar;


    public bool isDead = false;



    //STATES
    private enum State
    {
        Searching,
        Rolling,
        Dead
    }

    private State currentState;


    private void Start()
    {
        stats = FindObjectOfType<Stats>();

        rb = GetComponentInChildren<Rigidbody>();

        impulse = GetComponent<Cinemachine.CinemachineImpulseSource>();

        currentState = State.Searching;

        health = maxHealth;

        popupText = transform.parent.GetComponentInChildren<Text>();
    }


    private void Update()
    {
        switch (currentState)
        {
            case State.Searching:
                UpdateSearchingState();
                break;
            case State.Rolling:
                UpdateRollingState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
        //lastVelocity = rb.velocity; //MOVE TO ONLY SET ONCE

        //Debug.Log(currentState);
        //Debug.Log(timerDone);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("d");
        if (currentState == State.Rolling && (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player"))
        {
            bounceCurrent++;
            //bounce off wall
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            //rb.velocity = direction * Mathf.Max(speed, 0f);
            newVelocity = direction * Mathf.Max(speed, 0f);

            //lastVelocity = rb.velocity;
            //Debug.Log("d");

            impulse.GenerateImpulse(Camera.main.transform.forward);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent.GetComponent<CombatController>().TakeDamage(5);
        }
    }


    private void UpdateSearchingState()
    {
        //set vars
        if(rollActive)
        {
            rollActive = false;
            savedTime = Time.time;
            //transform.parent.rotation = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //if (Time.time < Time.time + lookLength)
        //savedTime = Time.time;
        if (Time.time - savedTime < lookLength)
        {
            Look();
            //lastLook = Time.time;
        }
        else
        {
            //Debug.Log("dodo");
            SwitchState(State.Rolling);
        }


        //look for player for certain amount of time 

        /*
        IEnumerator co;
        co = LookForPlayer();

        StartCoroutine(co);

        
        if(!timerStarted)
        {
            StartCoroutine(co);
            timerStarted = true;
        }
        
        if (timerDone)
        {
            StopCoroutine(co);
            SwitchState(State.Rolling);
        }
        */
        

        //SwitchState(State.Rolling);

        //Debug.Log(target.position);
    }

    private void UpdateRollingState()
    {
        if (!rollActive)
        {
            //start initial velocity 
            //rb.velocity = transform.forward * moveSpeed;
            lastVelocity = rb.velocity;
            //newVelocity = transform.parent.forward * moveSpeed;
            newVelocity = stable.forward * moveSpeed;
            bounceCurrent = 0;
            bounceMax = UnityEngine.Random.Range(1, 7);
            rollActive = true;
        }

        

        else
        {
            //rb.velocity = transform.parent.forward * moveSpeed;
            rb.velocity = newVelocity;
            lastVelocity = rb.velocity;
        }
        
        //track number of bounces

        if (bounceCurrent == bounceMax)
        {
            SwitchState(State.Searching);
        }
    }

    private void UpdateDeadState()
    {
        
    }

    private void SwitchState(State state)
    {
        switch (state)
        {
            case State.Searching:
                UpdateSearchingState();
                break;
            case State.Rolling:
                UpdateRollingState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;

        }

        currentState = state;
        //Debug.Log(currentState);
    }

    public void TakeDamage(int damage, int diceType)
    {
        if (diceType == prevDice)
        {
            consecutiveDice++;
            if (consecutiveDice == diceBoost)
            {
                DiceBoost(diceType);
            }
        }
        else
        {
            consecutiveDice = 0;
        }

        popupText.text = damage.ToString();
        
        health -= damage;
        stats.IncreaseGold(1);
        //Debug.Log(stats.goldCount);
        //Debug.Log(health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void DiceBoost(int diceType)
    {
        switch (diceType)
        {
            case 1:
                health -= 10;
                break;
            case 2:
                moveSpeed -= 1;
                break;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }


    
    IEnumerator LookForPlayer()
    {
        //Debug.Log("look");
        float timePassed = 0f;
        while (timePassed < 3f)
        {
            //Loook 
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, rotation, lookSpeed * Time.deltaTime);
            transform.parent.rotation = Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y, 0);

            timePassed += Time.deltaTime;

            Debug.Log("l");

            yield return null;
        }

        Debug.Log("Done");
        timerDone = true;
        //SwitchState(State.Rolling);

        yield return new WaitForSeconds(1);

        //yield return null;
    }


    private void Look()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        //transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, rotation, lookSpeed * Time.deltaTime);
        //transform.parent.rotation = Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y, 0);

        stable.rotation = Quaternion.Lerp(stable.rotation, rotation, lookSpeed * Time.deltaTime);
        stable.rotation = Quaternion.Euler(0, stable.rotation.eulerAngles.y, 0);

        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        //transform.parent.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
        //transform.parent.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        //go forward based on parent orientation
    }



    public void ResetEnemy()
    {
        health = maxHealth;
        moveSpeed = 30f;
    }


    public void KillEnemy()
    {
        isDead = true;
    }

}
