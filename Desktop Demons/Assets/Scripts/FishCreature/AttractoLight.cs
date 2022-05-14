using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractoLight : MonoBehaviour
{
    [SerializeField] float attractorForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        parentDemon creature = collision.GetComponent<parentDemon>();
        if (creature)
        {
            creature.AddExternalSpeed(transform.position -creature.transform.position  * attractorForce*Time.deltaTime);
            print("sucking creature");
        }
    }
}
