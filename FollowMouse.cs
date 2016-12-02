using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour
{
    public GameObject reticle;
    private float rotSpeed = 2f;
    void Update()
    {
        //Angle between reticle and ship
        float angle = AngleBetweenPoints(transform.position, reticle.transform.position);
        float speed = rotSpeed * Time.deltaTime;
        //turning ship
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, reticle.transform.rotation, speed);
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
