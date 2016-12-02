using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public GameObject shipSprite;
    public GameObject ship;
    public float currentSpeed;
    public float normalSpeed;
    public float boostSpeed;
    public bool active = false;
    public bool facingUp = false;
    public bool facingDown = false;
    public bool facingLeft = false;
    public bool facingRight = false;

    private Rigidbody2D rigidbodyShip;
    private float moveHorizontal;
    private float moveVertical;
    public bool changeTable = false;
    private float dashForce = 500f;

    // Use this for initialization
    void Start()
    {
        rigidbodyShip = GetComponent<Rigidbody2D>();
    }

    void MovementS()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            rigidbodyShip.AddForce(transform.up * currentSpeed);
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            rigidbodyShip.AddForce(-transform.up * currentSpeed);
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rigidbodyShip.AddForce(transform.right * currentSpeed);
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rigidbodyShip.AddForce(-transform.right * currentSpeed);
        }
    }

    void ChangeTableReference()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            changeTable = true;
        }
        else
        {
            changeTable = false;
        }
    }

    void SideFacing()
    {
        if (changeTable)
        {
            if (shipSprite.transform.eulerAngles.z >= 295 && shipSprite.transform.eulerAngles.z <= 335)
            {
                facingUp = true;
                facingDown = false;
                facingLeft = true;
                facingRight = false;
            }
            if (shipSprite.transform.eulerAngles.z >= 25 && shipSprite.transform.eulerAngles.z <= 65)
            {
                facingUp = false;
                facingDown = true;
                facingLeft = true;
                facingRight = false;
            }
            if (shipSprite.transform.eulerAngles.z >= 115 && shipSprite.transform.eulerAngles.z <= 155)
            {
                facingUp = false;
                facingDown = true;
                facingLeft = false;
                facingRight = true;
            }
            if (shipSprite.transform.eulerAngles.z >= 205 && shipSprite.transform.eulerAngles.z <= 245)
            {
                facingUp = true;
                facingDown = false;
                facingLeft = false;
                facingRight = true;
            }
            if ((shipSprite.transform.eulerAngles.z > 335 || shipSprite.transform.eulerAngles.z < 25) ||
                (shipSprite.transform.eulerAngles.z > 65 && shipSprite.transform.eulerAngles.z < 115) ||
                (shipSprite.transform.eulerAngles.z > 155 && shipSprite.transform.eulerAngles.z < 205) ||
                (shipSprite.transform.eulerAngles.z > 245 && shipSprite.transform.eulerAngles.z < 295))
            {
                facingUp = false;
                facingDown = false;
                facingLeft = false;
                facingRight = false;
            }
        }
        else
        {
            if (shipSprite.transform.eulerAngles.z > 315 || shipSprite.transform.eulerAngles.z <= 45)
            {
                facingUp = false;
                facingDown = false;
                facingLeft = true;
                facingRight = false;
            }
            if (shipSprite.transform.eulerAngles.z > 45 && shipSprite.transform.eulerAngles.z <= 135)
            {
                facingUp = false;
                facingDown = true;
                facingLeft = false;
                facingRight = false;
            }
            if (shipSprite.transform.eulerAngles.z > 135 && shipSprite.transform.eulerAngles.z <= 225)
            {
                facingUp = false;
                facingDown = false;
                facingLeft = false;
                facingRight = true;
            }
            if (shipSprite.transform.eulerAngles.z > 225 && shipSprite.transform.eulerAngles.z <= 315)
            {
                facingUp = true;
                facingDown = false;
                facingLeft = false;
                facingRight = false;
            }
        }
    }

    void AddSpeed()
    {
        if (changeTable)
        {
            if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && facingUp && facingRight) ||
                (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && facingRight && facingDown) ||
                (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && facingDown && facingLeft) ||
                (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && facingLeft && facingUp))
            {
                currentSpeed = boostSpeed;
            }
            else
            {
                currentSpeed = normalSpeed;
            }
        }
        else
        {
            if ((Input.GetKey(KeyCode.W) && facingUp) || (Input.GetKey(KeyCode.S) && facingDown) || (Input.GetKey(KeyCode.A) && facingLeft) || (Input.GetKey(KeyCode.D) && facingRight))
            {
                currentSpeed = boostSpeed;
            }
            else
            {
                currentSpeed = normalSpeed;
            }
        }
    }

    void SettingSideFacingInCharacteristics()
    {
        shipSprite.GetComponent<Characteristics>().SetAllFacingDirection(facingUp, facingDown, facingLeft, facingRight);
    }

    public bool SetPlayerActive
    {
        set
        {
            active = value;
        }
        get
        {
            return active;
        }
    }

    void Dash()
    {
        Vector2 dir = Vector2.right; // standard value;
        if(Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dir = Vector2.up.normalized;
            if (Input.GetKey(KeyCode.D))
            {
                dir = new Vector2(0.5f, 0.5f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir = new Vector2(-0.5f, 0.5f);
            }
            rigidbodyShip.isKinematic = true;
            rigidbodyShip.isKinematic = false;
            rigidbodyShip.AddForce(dir * dashForce);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dir = -Vector2.up.normalized;
            if (Input.GetKey(KeyCode.D))
            {
                dir = new Vector2(0.5f, -0.5f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir = new Vector2(-0.5f, -0.5f);
            }
            rigidbodyShip.isKinematic = true;
            rigidbodyShip.isKinematic = false;
            rigidbodyShip.AddForce(dir * dashForce);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift) && !(Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.S)))
        {
            dir = Vector2.right.normalized;
            rigidbodyShip.isKinematic = true;
            rigidbodyShip.isKinematic = false;
            rigidbodyShip.AddForce(dir * dashForce);
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift) && !(Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.S)))
        {
            dir = -Vector2.right.normalized;
            rigidbodyShip.isKinematic = true;
            rigidbodyShip.isKinematic = false;
            rigidbodyShip.AddForce(dir * dashForce);
        }
    }
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(active)
        {
            Dash();
            MovementS();
            SideFacing();
            AddSpeed();
            ChangeTableReference();
            SettingSideFacingInCharacteristics();
        }
    }
}
