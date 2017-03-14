using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public int shieldMax;

    private int currentShield;
    private bool shipGotHit = false;
    private CircleCollider2D collider;
    private float timer;
    private float shieldTimer;

    void Start()
    {
        shieldMax = GetComponentInParent<Characteristics>().shield;
        currentShield = shieldMax;
        collider = GetComponent<CircleCollider2D>();
    }

    public bool ShipGotHit
    {
        set
        {
            shipGotHit = value;
        }
        get
        {
            return shipGotHit;
        }
    }

    public void ShieldTakeDamage(int amount)
    {
        currentShield = GetComponentInParent<Characteristics>().shield;
        if (currentShield > 0)
        {
            currentShield = currentShield - amount;
            GetComponentInParent<Characteristics>().shield = currentShield;
            GetComponent<AudioSource>().Play();
            ShipGotHit = true;
        }
    }

    void CheckIfShieldDown()
    {
        if(currentShield > 0)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
    }

    void Update ()
    {
        currentShield = GetComponentInParent<Characteristics>().shield;
        CheckIfShieldDown();
        timer += Time.deltaTime;
        if(currentShield <= 0)
        {

        }
        if(shipGotHit)
        {
            timer = 0;
            shipGotHit = false;
        }
        if (timer > 3.0f)
        {
            shieldTimer += Time.deltaTime;
            if(shieldTimer > 0.4f)
            {
                if (currentShield < shieldMax)
                {
                    currentShield = currentShield + 1;
                    shieldTimer = 0;
                    shieldTimer += Time.deltaTime;
                    GetComponentInParent<Characteristics>().shield = currentShield;
                }
            }
        }
    }
}
