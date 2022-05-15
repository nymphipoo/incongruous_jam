using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegDuckie : parentDemon
{
    [SerializeField] bool startRight = true;
    [SerializeField] bool flipChild = false;
    [SerializeField] protected bool onGround = true;
    [SerializeField] protected float gravityStrength = 5;

    [SerializeField] protected Transform up;
    [SerializeField] protected Transform down;
    [SerializeField] protected Transform right;
    [SerializeField] protected Transform left;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (startRight)
        {
            internalSpeed = new Vector2(1, 0);
        }
        else
        {
            internalSpeed = new Vector2(-1, 0);
        }
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 fwd = transform.TransformDirection(internalSpeed);
        Vector2 length = rb2d.velocity;

        RaycastHit2D hit = Physics2D.Raycast(transform.position,new Vector2(rb2d.velocity.normalized.x,0), .5f, creatureCollisionLayers);
        Debug.DrawRay(transform.position, new Vector2(rb2d.velocity.normalized.x, 0));
        if (hit)
        {
            internalSpeed.x = -internalSpeed.x;
            spriteRef.flipX = !spriteRef.flipX;
        }

        applyGravity();

        updateVelocity();
    }

    virtual protected void applyGravity()
    {
        onGround = isOnGround();
        if (!onGround)
        {
            internalSpeed.y -= gravityStrength * Time.fixedDeltaTime;
        }
        else if (onGround && internalSpeed.y < 0)
        {
            internalSpeed.y = 0;
        }
        print(internalSpeed.y);
    }

    virtual protected bool isOnGround()
    {
        print("check");
        Debug.DrawLine(transform.position, new Vector2(left.position.x, down.position.y), Color.black);
        RaycastHit2D hit = Physics2D.Linecast(transform.position, new Vector2(left.position.x, down.position.y), creatureCollisionLayers);
        if (!hit)
        {
            hit = Physics2D.Linecast(transform.position, new Vector2(right.position.x, down.position.y), creatureCollisionLayers);
            Debug.DrawLine(transform.position, new Vector2(right.position.x, down.position.y), Color.black);
        }

        return (hit);
    }
}
