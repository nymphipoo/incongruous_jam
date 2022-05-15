using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayDuck : parentDemon
{
    [SerializeField] float growSpeed, maxHeight, walkSpeed, rightPos, bottomPos;
    bool doneGrowing;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRef.size.y < maxHeight || transform.position.y>=bottomPos)
        {
            if (spriteRef.size.y < maxHeight)
            {
                spriteRef.size = new Vector2(spriteRef.size.x, spriteRef.size.y + growSpeed * Time.deltaTime);
            }
            if (transform.position.y >= bottomPos)
            {
                transform.position -= Vector3.up*growSpeed * Time.deltaTime;
            }

        }
        else
        {
            if (!doneGrowing)
            {
                doneGrowing = true;
                GetComponent<AlwaysWalkCycling>().enabled = true;
            }
            transform.position += Vector3.right*walkSpeed * Time.deltaTime;
            if (transform.position.x > rightPos)
            {
                Escaped();
            }
        }
    }
}
