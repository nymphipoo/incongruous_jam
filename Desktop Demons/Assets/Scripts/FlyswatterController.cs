using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyswatterController : MonoBehaviour
{
    [SerializeField] float followSpeedMax,followSpeedMin,minDistance,maxDistance, holdDelay,baseWiggle;
    [SerializeField] Sprite hoverSprite, holdSprite;
    Collider2D colRef;
    Rigidbody2D rigidRef;
    SpriteRenderer spriteRef;
    Vector2 mousePosition = new Vector2();
    float timer = 0;
    bool held = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidRef = GetComponent<Rigidbody2D>();
        colRef = GetComponentInChildren<Collider2D>();
        spriteRef = GetComponent<SpriteRenderer>();
        spriteRef.sprite = hoverSprite;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && timer<=0) {
            SlapStart();
        }
        if (held )
        {
            timer -= Time.deltaTime;
            if (timer<0 && !Input.GetMouseButton(0))
             {
               SlapEnd();
              }
        }
    }
    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(mousePosition.x,mousePosition.y,transform.position.z) - transform.position;
        if (direction.magnitude > minDistance)
        {
            rigidRef.AddForce(direction.normalized * Mathf.Clamp(followSpeedMax *((direction.magnitude)-minDistance /maxDistance),followSpeedMin,followSpeedMax) * Time.fixedDeltaTime,ForceMode2D.Impulse);
            rigidRef.AddForce(Vector3.up * Mathf.Sin(Time.time)*baseWiggle, ForceMode2D.Impulse);
        }

    }
    void SlapStart()
    {
        //do slap check

        //do force around slap maybe?

        //become solid
        timer = holdDelay;
        colRef.enabled = true;
        spriteRef.sprite = holdSprite;
        held = true;
    }
    void SlapEnd()
    {

        timer = 0;
        colRef.enabled = false;
        spriteRef.sprite = hoverSprite;
        held = false;
    }
}
