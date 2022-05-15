using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoSlime : SlimeCreature
{
    [SerializeField] Transform target;
    parentDemon targetDemon;
    Vector3 offsetFromTarget = new Vector3();
    // Start is called before the first frame update

    private void Start()
    {
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
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.transform == target)
        {
            target = null;
            targetDemon = null;
        }
    }
}
