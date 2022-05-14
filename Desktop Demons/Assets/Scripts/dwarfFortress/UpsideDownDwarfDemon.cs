using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpsideDownDwarfDemon : DwarfDemon
{
    public override void Jump()
    {
        print("jump");
        internalSpeed.y = -jumpStrength;
    }

    protected override bool isOnGround()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, new Vector2(left.position.x, up.position.y));
        if (!hit)
        {
            hit = Physics2D.Linecast(transform.position, new Vector2(right.position.x, up.position.y));
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

    protected override void applyGravity()
    {
        if (!onGround)
        {
            hasParent = false;
            internalSpeed.y += gravityStrength * Time.fixedDeltaTime;
            print(internalSpeed.y);
        }
        else if (onGround && internalSpeed.y > 0)
        {
            print("on ground");
            internalSpeed.y = 0;
        }
    }
}

