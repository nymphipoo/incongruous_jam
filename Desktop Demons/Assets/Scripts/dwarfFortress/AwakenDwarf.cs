using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenDwarf : DwarfDemon
{
    GameObject awakenDwarfPrefab;
    string awakenFood = "book";

    [SerializeField]  float yLeave=-5;

    [SerializeField] public float timeToLeave = -1;
    [SerializeField] float timeTillAction = 10;


    protected override void Start()
    {
        print("here");
        base.Start();

        if (timeToLeave == -1)
        {
            timeToLeave = Time.time + timeTillAction;
            print(timeToLeave);
        }
        else {
            print("why:"+timeToLeave);
        }
        shouldBreed = false;
    }

    protected override void FixedUpdate()
    {
        if (Time.time > timeToLeave) {
            print(timeToLeave);
            Leave();
        }
        Infect();
        base.FixedUpdate();
    }

    void Infect()
    {
        CheckIfDwarfInfected( Physics2D.Linecast(transform.position, new Vector2(left.position.x, down.position.y), creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position,left.position, creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, new Vector2(left.position.x, up.position.y), creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, up.position, creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, new Vector2(right.position.x, up.position.y), creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, right.position, creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, new Vector2(right.position.x, down.position.y), creatureCollisionLayers));
        CheckIfDwarfInfected(Physics2D.Linecast(transform.position, down.position, creatureCollisionLayers));
    }

    void CheckIfDwarfInfected(RaycastHit2D dwarf) {
        if (dwarf) { 
            DwarfDemon dwarfScript = dwarf.collider.gameObject.GetComponent<DwarfDemon>();
            if (dwarfScript)
            {
                dwarfScript.Evolving(awakenFood, awakenDwarfPrefab);
            }
        }
    }

    private void Leave()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        if (transform.position.y >= yLeave)
        {
            internalSpeed.y = 0;
            transform.position = new Vector3(transform.position.x, yLeave, transform.position.z);
        }
    }
}
