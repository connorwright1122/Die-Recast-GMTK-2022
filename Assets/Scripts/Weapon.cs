using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponSpeed;
    public int weaponDamage;
    public int weaponRange;
    public int weaponRate;
    public int weaponType;

    public Rigidbody rb;

    private Vector3 initialPosition;

    //public Stats stats;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * weaponSpeed;

        initialPosition = transform.position;

        //stats = GameObject.FindObjectOfType<Stats>();
    }

    private void Update()
    {
        CheckDistance();
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.transform.parent.transform.parent.GetComponent<Enemy>().TakeDamage(weaponDamage);
            Destroy(gameObject);
            //stats.goldCount++;
            //Debug.Log(stats.goldCount);
        }

        
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //other.gameObject.transform.parent.transform.parent.GetComponent<Enemy>().TakeDamage(weaponDamage);
            other.gameObject.GetComponent<Enemy>().TakeDamage(weaponDamage, weaponType);
            Destroy(gameObject);
            //stats.goldCount++;
            //Debug.Log(stats.goldCount);
            //Debug.Log("h");
        }
    }


    public void SetWeaponStats(int damage, int range, int rate, int speed, int diceType)
    {
        weaponSpeed = speed;
        weaponDamage = damage;
        weaponRange = range;
        weaponRate = rate;
        weaponType = diceType;

        //Debug.Log(weaponDamage);
    }


    private void CheckDistance()
    {
        if (Vector3.Distance(initialPosition, transform.position) > (float) weaponRange)
        {
            //Destroy(gameObject);
            //Debug.Log("h");
            rb.isKinematic = true;
        }
    }
}
