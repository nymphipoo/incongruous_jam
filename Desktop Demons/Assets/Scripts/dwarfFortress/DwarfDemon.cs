using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfDemon : parentDemon
{

    [SerializeField] public bool startRight = true;
    [SerializeField] protected bool gravity = true;

    [SerializeField] protected float currentSpawnTimeLeft = 10;
    [SerializeField] protected float minSpawnTime = 10;
    [SerializeField] protected float maxSpawnTime = 20;
    [SerializeField] protected float maxMen = 10;


    [SerializeField] protected GameObject parent;
    [SerializeField] protected bool hasParent = false;
    [SerializeField] protected bool shouldBreed = true;

    [SerializeField] protected GameObject dwarf;

    [SerializeField] protected bool onGround = true;
    [SerializeField] protected float gravityStrength = 5;
    [SerializeField] protected float jumpStrength = 5;

    [SerializeField] protected Transform up;
    [SerializeField] protected Transform down;

    [SerializeField] protected Transform rightUP;
    [SerializeField] protected Transform leftUP;

    [SerializeField] protected Transform right;
    [SerializeField] protected Transform left;
    // Start is called before the first frame update
    override protected void Start()
    {
        currentSpawnTimeLeft = Random.Range(minSpawnTime, maxSpawnTime);
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

    private void Update()
    {
        if (currentSpawnTimeLeft < 0)
        {
            currentSpawnTimeLeft = Random.Range(minSpawnTime, maxSpawnTime);
            if (transform.parent.childCount < 10)
            {
                if (shouldBreed)
                {

                    SpawnDwarf();
                }
            }

        }
        currentSpawnTimeLeft -= Time.deltaTime;
    }
    // Update is called once per frame
    virtual protected void FixedUpdate()
    {
        CheckIfBounce();
        onGround = isOnGround();
        applyGravity();
        
        base.updateVelocity();

        if (hasParent)
            rb2d.velocity = new Vector2(parent.transform.GetComponent<Rigidbody2D>().velocity.x, rb2d.velocity.y); ;
    }

    protected void CheckIfBounce()
    {
        RaycastHit2D hit;

        if (internalSpeed.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            hit = Physics2D.Linecast(transform.position, right.position, creatureCollisionLayers); 
            if(!hit)
               hit= Physics2D.Linecast(transform.position, rightUP.position, creatureCollisionLayers);

            Debug.DrawLine(transform.position, right.position, Color.black);
            Debug.DrawLine(transform.position, rightUP.position, Color.black);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            hit = Physics2D.Linecast(transform.position, left.position, creatureCollisionLayers);
            if (!hit)
                hit = Physics2D.Linecast(transform.position, leftUP.position, creatureCollisionLayers);

            Debug.DrawLine(transform.position, left.position, Color.black);
            Debug.DrawLine(transform.position, leftUP.position, Color.black);
        }
        if (hit)
        {
            GameObject hitObject = hit.collider.gameObject;
            internalSpeed.x = -internalSpeed.x;

            Jump();

        }
    }

    virtual protected void applyGravity()
    {
        if (!onGround)
        {
            hasParent = false;
            internalSpeed.y -= gravityStrength * Time.fixedDeltaTime;
        }
        else if (onGround && internalSpeed.y < 0)
        {
            internalSpeed.y = 0;
        }
    }

    virtual protected bool isOnGround()
    {
        Debug.DrawLine(transform.position, new Vector2(left.position.x + .1f, down.position.y), Color.black);
        RaycastHit2D hit = Physics2D.Linecast(transform.position, new Vector2(left.position.x+.1f, down.position.y), creatureCollisionLayers);
        if (!hit)
        {
            hit = Physics2D.Linecast(transform.position, new Vector2(right.position.x - .1f, down.position.y), creatureCollisionLayers);
            Debug.DrawLine(transform.position, new Vector2(right.position.x - .1f, down.position.y), Color.black);
        }

        if (hit)
        {
            if (hit.collider.gameObject.GetComponent<parentDemon>())
            {
                parent = hit.collider.gameObject;
                hasParent = true;
            }
            else
            {
                hasParent = false;
            }
        }
        return (hit);
    }

    virtual public void Jump()
    {
        internalSpeed.y = jumpStrength;
    }

    protected void SpawnDwarf()
    {
        GameObject dwarfDemon = Instantiate(dwarf, transform.parent);
        dwarfDemon.transform.position = transform.position;
        DwarfDemon demonScript = dwarfDemon.GetComponent<DwarfDemon>();
        demonScript.dwarf = dwarf;
        demonScript.startRight = !(internalSpeed.x > 0);
        demonScript.Jump();

    }
}
