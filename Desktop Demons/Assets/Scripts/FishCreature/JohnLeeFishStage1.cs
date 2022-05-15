using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnLeeFishStage1 : parentDemon
{

    [SerializeField] bool startRight = true;
    [SerializeField] bool flipChild = false;
    // Start is called before the first frame update
    void Start()
    {

        if (startRight)
        {
            internalSpeed = new Vector2(2, 0);
        }
        else
        {
            internalSpeed = new Vector2(-2, 0);
        }
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 fwd = transform.TransformDirection(internalSpeed);
        Vector2 length = rb2d.velocity;

        Vector2 offset = new Vector2(transform.position.x, transform.position.y) + (rb2d.velocity.normalized * (transform.localScale.magnitude / 2));
        RaycastHit2D hit = Physics2D.Raycast(offset, transform.TransformDirection(rb2d.velocity).normalized, .5f*transform.localScale.x,creatureCollisionLayers);
        if (hit)
        {
            GameObject hitObject = hit.collider.gameObject;
            internalSpeed.x = -internalSpeed.x;
            spriteRef.flipX = !spriteRef.flipX;
            if (flipChild)
            {
                transform.GetChild(0).localPosition = new Vector3(-transform.GetChild(0).localPosition.x, transform.GetChild(0).localPosition.y, transform.GetChild(0).localPosition.z);
            }
        }
        Debug.DrawRay(offset, new Vector3(1, 0, 0), Color.white);


        /*
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow, 20, false);
            Debug.Log("Did Hit");
        }
        Debug.DrawRay(transform.position, fwd, Color.yellow, r2d.velocity.magnitude);*/

        updateVelocity();
    }

}

