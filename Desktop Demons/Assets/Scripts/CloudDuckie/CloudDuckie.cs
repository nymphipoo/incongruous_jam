using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDuckie : parentDemon
{
    [SerializeField] bool startRight = true;
    [SerializeField] bool flipChild = false;
    [SerializeField] float upDownTime = 1;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (startRight)
        {
            internalSpeed = new Vector2(1, 1);
        }
        else
        {
            internalSpeed = new Vector2(-1, 1);
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
            internalSpeed.y = -internalSpeed.y;
            spriteRef.flipX = !spriteRef.flipX;
        }

        timer += Time.deltaTime;
        if (timer > upDownTime)
        {
            internalSpeed.y = -internalSpeed.y;
            timer = 0;
        }


        updateVelocity();
    }
}
