using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Characteristics : MonoBehaviour
{
    public int health = 100;
    public int shield = 40;
    public int speed = 10;
    public int rotationSpeed = 10;
    public bool dead = false;

    public float currentSpeed;

    public bool facingUp = false;
    public bool facingDown = false;
    public bool facingLeft = false;
    public bool facingRight = false;

    public Slider healthSlider;
    public Slider shieldSlider;

    void Update()
    {
        healthSlider.value = health;
        shieldSlider.value = shield;
    }

    public float GetPlayerCurrentSpeed()
    {
        currentSpeed = GetComponentInParent<ShipMovement>().currentSpeed;
        return currentSpeed;
    }

    public bool FacingUp
    {
        set{ facingUp = value; }
        get{ return facingUp; }
    }

    public bool FacingDown
    {
        set { facingDown = value; }
        get { return facingDown; }
    }

    public bool FacingLeft
    {
        set { facingLeft = value; }
        get { return facingLeft; }
    }

    public bool FacingRight
    {
        set { facingRight = value; }
        get { return facingRight; }
    }

    public void SetAllFacingDirection(bool facingUp, bool facingDown, bool facingLeft, bool facingRight)
    {
        this.facingUp = facingUp;
        this.facingDown = facingDown;
        this.facingLeft = facingLeft;
        this.facingRight = facingRight;
    }

    public float GetRotationZ()
    {
        return transform.eulerAngles.z;
    }
}
