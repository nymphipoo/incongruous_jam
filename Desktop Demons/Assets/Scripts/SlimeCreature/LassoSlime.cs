using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoSlime : SlimeCreature
{
    [SerializeField] Transform target;
    parentDemon targetDemon;
    Vector3 offsetFromTarget = new Vector3();
    bool latched = false;
    Collider2D colRef;
    // Start is called before the first frame update

    private void Start()
    {
        colRef = GetComponent<Collider2D>();
        transform.rotation = Quaternion.identity;
        //print(mask.value);
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (target)
        {
            rb2d.position = target.position + offsetFromTarget;
        }
        else
        {
            if (latched)
            {
                latched = false;
           
                target = null;
                targetDemon.hitchHiker = null;
                targetDemon = null;
                colRef.enabled = true;
            }
            base.Update();
        }
    }
    protected override void FixedUpdate()
    {
        if (!target)
        {
            base.FixedUpdate();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (target == null)
        {
            targetDemon = collision.gameObject.GetComponent<parentDemon>();
            if (targetDemon)
            {
                target = collision.transform;
                offsetFromTarget = transform.position - target.position;
                targetDemon.hitchHiker = this;
                latched = true;
                colRef.enabled = false;
            }
        }
        base.OnCollisionEnter2D(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
