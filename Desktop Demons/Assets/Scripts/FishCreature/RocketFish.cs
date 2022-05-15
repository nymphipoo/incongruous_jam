using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFish : parentDemon
{
    [SerializeField] float startSpeed, speedIncreasePerBump, maxSpeed;
    [SerializeField] Vector3 slightAngleUp;
    [SerializeField] GameObject crack;
    float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = startSpeed;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - Random.Range(-45, 45));

        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        internalSpeed = (transform.right) * currentSpeed * Time.deltaTime;
        base.updateVelocity();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentSpeed > maxSpeed)
        {
            Escaped();
            //play a crack sound
            Instantiate(crack, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
        else
        {
            //Vector2 colPoint = collision.GetContact(0).point;
            transform.right = Vector2.Reflect(transform.right, collision.GetContact(0).normal);
            currentSpeed *= speedIncreasePerBump;
        }
    }
}
