using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnLeeFeetFish : parentDemon
{
    [SerializeField] bool grounded = false;
    [SerializeField] float fallAccel,terminalFall, walkSpeed, snapDistance,direction,frameTime;
    [SerializeField] Sprite[] walkFrames;
    Collider2D colRef;
    // Start is called before the first frame update
    float currentFallSpeed = 0;
    float timer = 0;
    int walkFrameIndex = 0;
    protected override void Start()
    {
        colRef = GetComponentInChildren<Collider2D>();
        spriteRef = GetComponent<SpriteRenderer>();
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        internalSpeed = Vector2.zero;
        if (!grounded)
        {
            currentFallSpeed = Mathf.Clamp(currentFallSpeed + fallAccel * Time.deltaTime, 0, terminalFall);
            internalSpeed = -transform.up * currentFallSpeed;
            RaycastHit2D behindYou = Physics2D.Raycast(transform.position, -transform.right, snapDistance,creatureCollisionLayers);
            if(behindYou && behindYou.collider)
            {
                Flippo(behindYou.normal);
            }
        }
        else
        {
            Vector2 horizontalDirection = transform.right * direction * walkSpeed;
            internalSpeed += horizontalDirection;
            if (timer > frameTime)
            {
                walkFrameIndex++;
                if (walkFrameIndex >= walkFrames.Length)
                {
                    walkFrameIndex = 0;
                }
                spriteRef.sprite = walkFrames[walkFrameIndex];
                timer = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        base.updateVelocity();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Flippo(collision.GetContact(0).normal);

        base.OnCollisionEnter2D(collision);
    }
    void Flippo(Vector3 normal)
    {

        transform.rotation = Quaternion.LookRotation(Vector3.forward,normal);
        grounded = true;
        currentFallSpeed = 0;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!colRef.IsTouchingLayers())
        {
            grounded = false;
        }
    }
}
