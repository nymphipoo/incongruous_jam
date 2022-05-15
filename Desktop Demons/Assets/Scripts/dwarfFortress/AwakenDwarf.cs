using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenDwarf : DwarfDemon
{
    string awakenFood = "book";

    [SerializeField]  float yLeave=-5;

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
        if (Time.time > timeToLeave) {
            Leave();
        }
        if (Time.time > timeToLeave+timeTillAction)
        {
            timeToLeave = -1;
            Escaped();
        }
        base.FixedUpdate();
        Infect();
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
            print(isDwarf.collider.name);
            DwarfDemon dwarfScript = isDwarf.collider.gameObject.GetComponent<DwarfDemon>();
            if (dwarfScript)
            {
                dwarfScript.Evolving(awakenFood, dwarf);
            }
        }
    }

    private void Leave()
    {
        Jump();
        GetComponent<BoxCollider2D>().enabled = false;
        if (transform.position.y >= yLeave)
        {
            internalSpeed.y = 0;
            transform.position = new Vector3(transform.position.x, yLeave, transform.position.z);
        }
    }
}
