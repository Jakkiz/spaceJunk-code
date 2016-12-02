using UnityEngine;
using System.Collections;

public class HitController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigidBody;
    GameObject projectileOwner;
    public bool instantiated = false;
    public bool playerBullet = false;
    public bool enemyBullet = false;
    //public bool hitSomething = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public bool IsAPlayerBullet
    {
        set
        {
            playerBullet = value;
            instantiated = true;
        }
        get
        {
            return playerBullet;
        }
    }

    public bool IsAEnemyBullet
    {
        set
        {
            enemyBullet = value;
            instantiated = true;
        }
        get
        {
            return enemyBullet;
        }
    }

    public GameObject ProjectileOwner
    {
        set
        {
            projectileOwner = value;
        }
        get
        {
            return projectileOwner;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(instantiated)
        {
            if (playerBullet)
            {
                if (other.tag == "EnemyHitBox")
                {
                    other.GetComponent<ShipCollider>().TakeDamage(18);
                    //ShakeCamera
                    rigidBody.isKinematic = true;
                    anim.Play("projectShipBlast");
                    Destroy(gameObject, 0.4f);
                }
            }
            if (enemyBullet)
            {
                if (other.tag == "Player")
                {
                    other.GetComponent<TakeDamage>().InflictDamage(8);
                    projectileOwner.GetComponent<AIWeapons>().HitSomething();
                    //Knockback
                    other.GetComponentInParent<Rigidbody2D>().AddForce(-transform.right * 50f);
                    rigidBody.isKinematic = true;
                    anim.Play("projectShipBlast");
                    Destroy(gameObject, 0.4f);
                }
                if (other.tag == "Shield")
                {
                    other.GetComponent<Shield>().ShieldTakeDamage(8);
                    rigidBody.isKinematic = true;
                    anim.Play("projectShieldBlast");
                    Destroy(gameObject, 0.4f);
                    projectileOwner.GetComponent<AIWeapons>().HitSomething();
                }
            }
        }
    }
}
