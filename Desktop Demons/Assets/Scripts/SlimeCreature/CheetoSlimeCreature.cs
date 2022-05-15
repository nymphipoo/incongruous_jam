using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheetoSlimeCreature : parentDemon
{
    // Start is called before the first frame update

    [SerializeField] bool grounded = false;
    [SerializeField] float fallAccel, terminalFall, walkSpeed, snapDistance, direction, frameTime;
    Collider2D colRef;
    // Start is called before the first frame update
    float currentFallSpeed = 0;
    float timer = 0;
    int walkFrameIndex = 0;

    [SerializeField] float jumpDelay, jumpForce, xRange, gravityAccel, gravityMax;
    [SerializeField] Transform groundCast;
    float jumpTimer = 0;
    float gravity = 0;
    protected override void Start()
    {
        colRef = GetComponentInChildren<Collider2D>();
        spriteRef = GetComponentInChildren<SpriteRenderer>();
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;
        jumpTimer += Time.deltaTime;
        internalSpeed = Vector2.zero;
        if (!grounded)
        {
            currentFallSpeed = Mathf.Clamp(currentFallSpeed + fallAccel * Time.deltaTime, -jumpForce, terminalFall);
            internalSpeed = -transform.up * currentFallSpeed;
            
        }
            Vector2 horizontalDirection = transform.right * direction * walkSpeed;
            internalSpeed += horizontalDirection;

        if (jumpTimer >= jumpDelay && grounded)
        {
            jumpTimer = 0;
            Jump();
        }
    }
    void Jump()
    {
        currentFallSpeed = -jumpForce;
        gravity = 0;
        grounded = false;
        jumpTimer = 0;
    }
    private void FixedUpdate()
    {
        grounded = Physics2D.Linecast(transform.position, groundCast.position, creatureCollisionLayers);

        base.updateVelocity();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Flippo(collision.GetContact(0).normal);
    }
    void Flippo(Vector3 normal)
    {

        transform.rotation = Quaternion.LookRotation(Vector3.forward, normal);
        grounded = true;
        currentFallSpeed = 0;
    }
    
}

