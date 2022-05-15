using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCreature : parentDemon
{
    [SerializeField] float jumpDelay,jumpForce,xRange,gravityAccel,gravityMax;
    [SerializeField] Transform groundCast;
    float timer = 0;
    float gravity=0;
    // Start is called before the first frame update
    

    // Update is called once per frame
    protected virtual void Update()
    {
        if (timer >= jumpDelay)
        {
            timer = 0;
            Jump();
        }
        if (!Physics2D.Linecast(transform.position, groundCast.position, creatureCollisionLayers))
        {
            gravity += gravityAccel * Time.deltaTime;
            gravity = Mathf.Clamp(gravity, 0, gravityMax);
        }
        else
        {

            timer += Time.deltaTime;
        }
    }
    void Jump()
    {
        internalSpeed.y = jumpForce;
        internalSpeed.x = Random.Range(-xRange, xRange);
        gravity = 0;
    }
    protected virtual void FixedUpdate()
    {
        if (!Physics2D.Linecast(transform.position, groundCast.position, creatureCollisionLayers))
        {
            internalSpeed.y -= gravity;
        }
        base.updateVelocity();
    }
}
