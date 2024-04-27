using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public CombatController pc;
    public TopDownMovement pm;

    public Enemy enemy;
    
    public Vector2 InputVector { get; private set;  }

    public Vector3 MousePosition { get; private set;  }


    public bool canAttack = true;


    public Animator anim;
    private string currentStateAnim;

    const string P_IDLE = "P_Idle";
    const string P_WALK = "P_Walk";
    const string P_ATTACK = "P_Attack";

    public int numDeaths = 0;

    public GameObject winUI;
    public Text deathText;




    public void Start()
    {
        pc = GetComponent<CombatController>();
        pm = GetComponent<TopDownMovement>();

        enemy = FindObjectOfType<Enemy>();

        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Movement();
        Combat();
        Die();
    }


    public void Movement()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        InputVector = new Vector2(h, v).normalized;

        MousePosition = Input.mousePosition;

        if (h == 0 && v == 0)
        {
            ChangeAnimationState(P_IDLE);
        }
        else
        {
            ChangeAnimationState(P_WALK);
        }
    }


    public void Combat()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            pc.RandomizeDiceStats();
            pm.RandomizeStats();
            pc.RandomizePlayerStats();
        }

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            pc.Attack();
            ChangeAnimationState(P_ATTACK);
        }
    }


    private void ChangeAnimationState(string newState)
    {
        if (currentStateAnim == newState) return;

        anim.Play(newState, 0, 0.0f);
        currentStateAnim = newState;

        //Debug.Log(currentStateAnim);
    }


    public void Die()
    {
        if (pc.isDead)
        {
            numDeaths++;
            enemy.ResetEnemy();
            pm.ResetPlayerMovement();
            pc.ResetPlayerCombat();
        }

        if (enemy.isDead)
        {
            winUI.SetActive(true);
            deathText.text = numDeaths.ToString();
        }
        
    }
}
