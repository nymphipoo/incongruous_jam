using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnLeeBootHeadFish : parentDemon
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

        internalSpeed.y = Mathf.Sin(Time.time*3f) * 1f;

        updateVelocity();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        internalSpeed.x = -internalSpeed.x;
        spriteRef.flipX = !spriteRef.flipX;
        if (flipChild)
        {
            transform.GetChild(0).localPosition = new Vector3(-transform.GetChild(0).localPosition.x, transform.GetChild(0).localPosition.y, transform.GetChild(0).localPosition.z);
        }

        base.OnCollisionEnter2D(collision);
    }
}

