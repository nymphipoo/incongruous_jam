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

        Vector2 offset = new Vector2(transform.position.x, transform.position.y) + (rb2d.velocity.normalized * (transform.localScale.magnitude / 2));
        RaycastHit2D hit = Physics2D.Raycast(offset, transform.TransformDirection(rb2d.velocity).normalized, 1, creatureCollisionLayers);
        if (hit)
        {
            GameObject hitObject = hit.collider.gameObject;
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
