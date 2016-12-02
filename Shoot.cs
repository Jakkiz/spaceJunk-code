using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    //heat value, from 0 to 100
    public float heat = 0;
    private float bulletSpeed = 500;
    //timer from the last bullet shot
    float timer;
    //rate of fire
    float rof = 0.12f;
    //timer used to lower heat once the player stop shooting for 3s or more
    float heatimer;

    public Slider heatSlider;
    public Image filling;

    void UpdateSlider()
    {
        heatSlider.value = heat;
        if (heat < 50)
        {
            filling.color = Color.green;
        }

        if (heat > 50 && heat < 85)
        {
            filling.color = Color.yellow;
        }
        if (heat > 85)
        {
            filling.color = Color.red;
        }

    }
    void FireProjectile(float heat)
    {
        // Hmod is the Heat Modifier that changes the bullet angle, the greater the number the more inaccurate
        float hmod = (heat / 9);
        //create a random number
        float random = Random.Range(1, 10);
        float rmod = Random.Range(0, hmod);
        // this converts the transform.rotation angle to a vector3 so that I can modify the bullet angle per axis
        Vector3 hr = transform.rotation.eulerAngles;
        //check the random number and decide the deviation of the bullet accordingly
        if (random < 5)
        {
            hr = new Vector3(hr.x, hr.y, hr.z + (hmod - rmod));
        }
        else
        {
            hr = new Vector3(hr.x, hr.y, hr.z - (hmod - rmod));
        }
        //same code as yours, only difference is that instead of using "transform.rotation" i use "Quaternion.Euler(hr)", it bascially take the Vector3 and converts it so we can use it as transform.rotate
        Vector3 weaponPosition = new Vector3(transform.position.x, transform.position.y, (transform.position.z + 0.02f));
        GameObject projClone = (GameObject)Instantiate(Resources.Load("projectile"), weaponPosition, Quaternion.Euler(hr));
        projClone.GetComponent<HitController>().IsAPlayerBullet = true;
        GetComponent<AudioSource>().Play();
        projClone.GetComponent<Rigidbody2D>().AddForce(-projClone.transform.right * bulletSpeed);
        Destroy(projClone, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();
        //start the timer
        timer += Time.deltaTime;
        //the player wont be able to shoot if the heat is higher than 99 or the gun isnt reloaded yet

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && heat < 99 && timer > rof)
        {
            //increase the rof based on heat, higher rof -> lower fire rate
            rof = rof + (heat / 5500);
            //increase heat, reset timer, fire the projectile
            heat = heat + 2;
            timer = 0.0f;
            FireProjectile(heat);
        }
        // if the player hasnt been shooting for the past 3 seconds and heat is higher than 0
        if (timer > 2f && heat > 0)
        {
            //start the heat timer to reduce the heat. basically the heat will be reduced every 0.2 seconds in the while
            heatimer += Time.deltaTime;
            if (heat >= 55)
            {
                GetComponent<AudioSource>().Play();
            }
            while (heat != 0 && heatimer > 0.02f)
            {
                //reduce heat, reset heat timer, restart heat timer. 
                heat = heat - 1;
                heatimer = 0;
                heatimer += Time.deltaTime;
                //I also start lowering the rof (thus increasing the fire rate) i also use an if because sometimes the rof would go below the intended base value
                rof = rof - (heat / 10000);
                if (rof < 0.12f)
                {
                    rof = 0.12f;
                }
                //just checking if the heat is below 0, in that case i increase it back to 0
                while (heat < 0)
                {
                    heat = heat + 1;
                }
            }
            //final check, just in case the rof has some weird value when heat reaches 0
            if (heat == 0)
            {
                rof = 0.12f;
            }
        }
    }

    void OnGUI()
    {
        //GUILayout.Label(" ");
        //GUILayout.Label("Heat:" + heat);
        //GUILayout.Label("Timer:" + timer);
        //GUILayout.Label("Heat Timer:" + heatimer);
        //GUILayout.Label("Rate of Fire" + rof);

    }
}
