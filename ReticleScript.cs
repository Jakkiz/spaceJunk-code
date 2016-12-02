using UnityEngine;
using System.Collections;

public class ReticleScript : MonoBehaviour
{
    public GameObject Player;
    float distance;
    void Update()
    {
        distance = Vector2.Distance(Player.transform.position, transform.position);
        //setting ship turning rate, higher the number, faster the turn
        float shipAgility = 5f - (2.3f - distance);
        //getting mouse position

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        //making the reticle move towards the mouse at a set speed
        if(!Camera.main.GetComponent<CameraShakeController>().shake)
        {
            transform.position = Vector2.MoveTowards(transform.position, mousePos, ((shipAgility * Time.deltaTime)));
        }
    }
}
