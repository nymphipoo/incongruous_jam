using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpsideDownDwarfDemon : DwarfDemon
{

    [SerializeField] protected bool isOnCeilling = false;

    public override void Jump()
    {
        internalSpeed.y = -jumpStrength;
    }

    protected override bool isOnGround()
    {
        Debug.DrawLine(transform.position, new Vector2(left.position.x + .1f, up.position.y), Color.black);
        RaycastHit2D hit = Physics2D.Linecast(transform.position, new Vector2(left.position.x+.1f, up.position.y), creatureCollisionLayers);
        if (!hit)
        {
            Debug.DrawLine(transform.position, new Vector2(right.position.x - .1f, up.position.y), Color.black);
            hit = Physics2D.Linecast(transform.position, new Vector2(right.position.x-.1f, up.position.y), creatureCollisionLayers);
        }

        if (hit)
        {
            isOnCeilling = (hit.collider.gameObject.tag == "wall");
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

    protected override void applyGravity()
    {
        if (!onGround)
        {
            hasParent = false;
            internalSpeed.y += gravityStrength * Time.fixedDeltaTime;
        }
        else if (onGround && internalSpeed.y > 0)
        {
            print("on ground");
            internalSpeed.y = 0;
        }
    }
}

