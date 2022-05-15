using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyswatterController : MonoBehaviour
{
    [SerializeField] float followSpeedMax,followSpeedMin,minDistance,maxDistance, holdDelay,baseWiggle;
    [SerializeField] Sprite hoverSprite, holdSprite;
    [SerializeField] GameObject sparksPrefab, bloodPrefab;
    Collider2D colRef;
    Rigidbody2D rigidRef;
    SpriteRenderer spriteRef;
    Vector2 mousePosition = new Vector2();

    [SerializeField] AudioClip miss1;
    [SerializeField] AudioClip miss2;
    [SerializeField] AudioClip hit;

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
            rigidRef.AddForce(direction.normalized * Mathf.Clamp(followSpeedMax *((direction.magnitude)-minDistance /maxDistance),followSpeedMin,followSpeedMax) * Time.fixedDeltaTime *transform.localScale.magnitude,ForceMode2D.Impulse);
            rigidRef.AddForce(Vector3.up * Mathf.Sin(Time.time)*baseWiggle, ForceMode2D.Impulse);
        }

    }
    void SlapStart()
    {


        //do force around slap maybe?

        //become solid
        timer = holdDelay;
        colRef.enabled = true;
        spriteRef.sprite = holdSprite;
        held = true;
        //slap gfx
        Instantiate(sparksPrefab, colRef.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

        //do slap check
        Collider2D[] overlappingGuys = new Collider2D[5];
        Physics2D.OverlapCollider(colRef, new ContactFilter2D(), overlappingGuys);

        int soundEffect = Random.Range(0, 2);

        float pitch = Random.Range(.5f, 1.5f);
        GetComponent<AudioSource>().pitch = pitch;

        if (soundEffect == 0)
            GetComponent<AudioSource>().clip = miss1;
        else
            GetComponent<AudioSource>().clip = miss2;


        foreach (Collider2D eachCol in overlappingGuys)
        {
            if (eachCol)
            {
                parentDemon eachDemon = eachCol.GetComponent<parentDemon>();
                if (eachDemon)
                {
                    eachDemon.Killed();
                    Instantiate(bloodPrefab, eachDemon.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    GetComponent<AudioSource>().clip = hit;

                }
            }
        }
        GetComponent<AudioSource>().Play();
    }
    void SlapEnd()
    {

        timer = 0;
        colRef.enabled = false;
        spriteRef.sprite = hoverSprite;
        held = false;
    }
}
