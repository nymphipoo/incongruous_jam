using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeDwarf : UpsideDownDwarfDemon
{
    [SerializeField] GameObject dwarfhole;
    [SerializeField] float yHole;

    bool hasHole = false;

    [SerializeField] float miningTimeLeft;
    [SerializeField] float totalMiningTime = 10;

    protected override void Start()
    {
        base.Start();
        internalSpeed.x = 0;
        shouldBreed = false;
    }
    protected override void FixedUpdate()
    {
        CheckIfBounce();
        onGround = isOnGround();
        applyGravity();

        if (onGround)
        {
            miningTimeLeft -= Time.fixedDeltaTime;
            if (miningTimeLeft < 0) {
                if(!hasHole)
                    createhole();
            }
        }
        else
            miningTimeLeft = totalMiningTime;

        base.updateVelocity();

        if (hasParent)
            rb2d.velocity = new Vector2(parent.transform.GetComponent<Rigidbody2D>().velocity.x, rb2d.velocity.y); ;

    }

    void createhole() {
        hasHole = true;
        GameObject dhole = Instantiate(dwarfhole);
        dhole.transform.position =new Vector3( transform.position.x, yHole, transform.position.z);
        //dhole.GetComponent<DwarfHole>().escape(gameObject);
    }
}
