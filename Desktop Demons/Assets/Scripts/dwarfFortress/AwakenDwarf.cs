using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenDwarf : DwarfDemon
{
    string awakenFood = "book";

    [SerializeField]  float yLeave=-4.7f;
    bool firstLeave = true;

    static public float timeToLeave = -1;
    [SerializeField] float timeTillAction = 10;

    
    protected override void Start()
    {
        if (timeToLeave == -1)
        {
            timeToLeave = Time.time + timeTillAction;
            print(timeToLeave);
        }
        shouldBreed = false;
        base.Start();
    }
    
    protected override void FixedUpdate()
    {
        if (Time.time > timeToLeave)
        {
            Leave();
            onGround = false;
            applyGravity();
            applyGravity();
        }
        else { 
            Infect();
            CheckIfBounce();
            if (hasParent)
                rb2d.velocity = new Vector2(parent.transform.GetComponent<Rigidbody2D>().velocity.x, rb2d.velocity.y); ;
            onGround = isOnGround();
            applyGravity();
        }
        if (Time.time > timeToLeave+timeTillAction/2)
        {
            timeToLeave = -1;
            Escaped();
        }
        onGround = isOnGround();
        parent = null;
        hasParent = false;
        base.updateVelocity();
    }

    void Infect()
    {
        CheckIfDwarfInfected( Physics2D.Linecast(transform.position, new Vector2(left.position.x, down.position.y)));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position,left.position * 1.1f, creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, new Vector2(left.position.x, up.position.y)));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, up.position*1.1f, creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, new Vector2(right.position.x, up.position.y)));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, right.position * 1.1f, creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, new Vector2(right.position.x, down.position.y)));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, down.position * 1.1f));
    }

    void CheckIfDwarfInfected(RaycastHit2D isDwarf) {
        if (isDwarf) {
            DwarfDemon dwarfScript = isDwarf.collider.gameObject.GetComponent<DwarfDemon>();
            if (dwarfScript)
            {
                dwarfScript.Evolving(awakenFood, dwarf);
            }
        }
    }

    private void Leave()
    {
        if (firstLeave)
        {
            print("here!");
            Jump();
            firstLeave = false;

        }

        GetComponent<Collider2D>().enabled = false;
        if (transform.position.y <= yLeave)
        {
            internalSpeed.y = 0;
            transform.position = new Vector3(transform.position.x, yLeave, transform.position.z);
        }
    }
}
