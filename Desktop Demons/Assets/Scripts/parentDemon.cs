using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentDemon : MonoBehaviour
{
    protected Rigidbody2D rb2d;

    [SerializeField] protected Vector2 internalSpeed;
    [SerializeField] protected Vector2 externalSpeed;

    protected float externalSpeedDecayRate = 5f;//this default value will decrease external speed by 1 a second;

    [SerializeField] protected creatureCounter creatureCounterScript;

    [SerializeField] protected bool parentExternalSpeedDecay = true;
    protected LayerMask creatureCollisionLayers=137;
    [SerializeField]
    EvolutionFood[] foodNameToEvo;
    public LassoSlime hitchHiker;
    [System.Serializable]
    public class EvolutionFood
    {
        public string foodTag;
        public GameObject prefabToEvolveTo;
    }

    [SerializeField] AudioClip deathNoise;
    [SerializeField] AudioClip [] collisionNoises;

    protected SpriteRenderer spriteRef;
    [SerializeField] GameObject smokeShart;
     float hitSmokeDelay=.5f;
    float smokeTimer=0;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        creatureCounterScript = creatureCounter.instance;
        if (creatureCounterScript)
        {
            creatureCounterScript.AddCreature(gameObject.name);
        }

        spriteRef = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    virtual protected void updateVelocity()
    {
        smokeTimer += Time.deltaTime;
        Movement();
        rb2d.velocity = internalSpeed+externalSpeed;
    }

    virtual protected void Movement()
    {
        if (parentExternalSpeedDecay)
        {
            ExternalSpeedDecay();
        }
    }

    virtual public void setExternalRateDecay(float newRate)
    {
       externalSpeedDecayRate = newRate;
    }

    virtual public float getExternalRateDecay()
    {
        return externalSpeedDecayRate;
    }

    virtual public void AddExternalSpeed(Vector2 newSpeed)
    {
        externalSpeed += newSpeed;
    }

    virtual public void SetExternalSpeed(Vector2 newSpeed)
    {
        externalSpeed = newSpeed;
    }

    virtual protected void ExternalSpeedDecay()
    {
        if (externalSpeed.x>0)
        {
            externalSpeed.x= Mathf.Max(externalSpeed.x - externalSpeedDecayRate, 0);
        }
        else if (externalSpeed.x < 0)
        {
            externalSpeed.x = Mathf.Min(externalSpeed.x + externalSpeedDecayRate, 0);
        }

        if (externalSpeed.y > 0)
        {
            externalSpeed.y = Mathf.Max(externalSpeed.y - externalSpeedDecayRate, 0);
        }
        else if (externalSpeed.y < 0)
        {
            externalSpeed.y = Mathf.Min(externalSpeed.y + externalSpeedDecayRate, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EncounteredFood(collision);
    }
  
    virtual protected void EncounteredFood(Collider2D collision)
    {
        eatable food = collision.gameObject.GetComponent<eatable>();

        if (food)
        {
            foreach(EvolutionFood eachPair in foodNameToEvo) { 
            //this will be changed when we determine how to this
                if (eachPair.foodTag==food.foodName)
                {
                    //we ideally would have a custom destroy function for all the items too
                    food.Eaten(transform);
                    Evolving(food.foodName, eachPair.prefabToEvolveTo);
                }
            }
        }
    }

    virtual public void Evolving(string foodName, GameObject evolution)
    {
        print("evolving: " + evolution.name + " food:" + foodName);
        internalSpeed = new Vector2(0, 0);
        rb2d.velocity = new Vector2(0, 0);
        if (creatureCounterScript)
            creatureCounterScript.evolved(gameObject.name);
        StartCoroutine(GetComponent<EvoltionDelay>().EvolutionUnderway(evolution));
    }

    virtual public void Killed()
    {
        GetComponent<AudioSource>().clip=  deathNoise;
        GetComponent<AudioSource>().Play();
        if (creatureCounterScript)
        {
            creatureCounterScript.RemoveCreature(gameObject.name, false);
        }
        GetComponent<parentDemon>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject,1);
    }

    virtual public void Escaped() {
        if (creatureCounterScript)
            creatureCounterScript.RemoveCreature(gameObject.name, true);
        GetComponent<parentDemon>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1);
        if (hitchHiker)
        {
            hitchHiker.Escaped();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (smokeTimer >= hitSmokeDelay)
        {
            int selected =(int)Mathf.Floor( Random.Range(0, collisionNoises.Length-1));
            GetComponent<AudioSource>().clip= collisionNoises[selected];
            GetComponent<AudioSource>().pitch = Random.Range(.9f,1.1f);
            GetComponent<AudioSource>().Play();
            smokeTimer = 0;
            if (smokeShart != null )
            {
                Instantiate(smokeShart, collision.GetContact(0).point + new Vector2(Random.Range(-.4f, .4f), Random.Range(-.4f, .4f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                Instantiate(smokeShart, collision.GetContact(0).point + new Vector2(Random.Range(-.4f, .4f), Random.Range(-.4f, .4f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                Instantiate(smokeShart, collision.GetContact(0).point + new Vector2(Random.Range(-.4f, .4f), Random.Range(-.4f, .4f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                Instantiate(smokeShart, collision.GetContact(0).point + new Vector2(Random.Range(-.4f, .4f), Random.Range(-.4f, .4f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            }
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (smokeTimer >= hitSmokeDelay)
        {
            smokeTimer = 0;
            if (smokeShart != null && (collision.collider.gameObject.layer==LayerMask.NameToLayer("Creatures")) )
            {
                Instantiate(smokeShart, collision.GetContact(0).point+new Vector2(Random.Range(-.4f,.4f), Random.Range(-.4f, .4f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                Instantiate(smokeShart, collision.GetContact(0).point + new Vector2(Random.Range(-.4f, .4f), Random.Range(-.4f, .4f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                Instantiate(smokeShart, collision.GetContact(0).point + new Vector2(Random.Range(-.4f, .4f), Random.Range(-.4f, .4f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                Instantiate(smokeShart, collision.GetContact(0).point + new Vector2(Random.Range(-.4f, .4f), Random.Range(-.4f, .4f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            }
        }
    }
}



