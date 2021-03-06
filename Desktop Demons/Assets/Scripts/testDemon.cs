using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDemon : parentDemon
{

    [SerializeField]public bool startRight = true;
    [SerializeField] bool gravity = true;

    [SerializeField] float currentSpawnTimeLeft = 10;
    [SerializeField] float minSpawnTime=10;
    [SerializeField] float maxSpawnTime=20;
    [SerializeField] float maxMen = 10;


    [SerializeField] GameObject parent;
    [SerializeField] bool hasParent = false;

    [SerializeField] GameObject dwarf;

    [SerializeField] bool onGround = true;
    [SerializeField] float gravityStrength = 5;
    [SerializeField] float jumpStrength = 5;

    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform right;
    [SerializeField] Transform left;
    // Start is called before the first frame update
    void Start()
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
                SpawnDwarf();
            }

        }
        currentSpawnTimeLeft -= Time.deltaTime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Transform horizontalDest;
        if (internalSpeed.x > 0)
        {
            horizontalDest = right;
        }
        else { 
            horizontalDest = left;
        }

        RaycastHit2D hit = Physics2D.Linecast(transform.position, horizontalDest.position);
        if (hit)
        {
            GameObject hitObject = hit.collider.gameObject;
            internalSpeed.x = -internalSpeed.x;

            Jump();

        }

        hit = Physics2D.Linecast(transform.position, new Vector2(left.position.x, down.position.y));
        if (!hit)
        {
            hit = Physics2D.Linecast(transform.position, new Vector2(right.position.x, down.position.y));
        }
        onGround = (hit);

        if (hit)
        {
            if (hit.collider.gameObject.GetComponent<parentDemon>())
            {
                parent = hit.collider.gameObject;
                hasParent = true;
            }
            else {
                hasParent = false;
            }
            
        }

        if (!onGround)
        {
            hasParent = false;
            internalSpeed.y -= gravityStrength * Time.fixedDeltaTime;
        }
        else if(onGround&&internalSpeed.y<0) {
            internalSpeed.y = 0;
        }

        base.updateVelocity();

        if (hasParent)
            rb2d.velocity = new Vector2(parent.transform.GetComponent<Rigidbody2D>().velocity.x, rb2d.velocity.y); ;
    }

    public void Jump() {
        print("jump");
        internalSpeed.y = jumpStrength;
    }

    protected void SpawnDwarf()
    {
        print(dwarf);
        GameObject dwarfDemon = Instantiate(dwarf, transform.parent);
        dwarfDemon.transform.position = transform.position;
        testDemon demonScript = dwarfDemon.GetComponent<testDemon>();
        demonScript.dwarf = dwarf;
        demonScript.startRight = !(internalSpeed.x > 0);
        demonScript.Jump();
        
    }
}
