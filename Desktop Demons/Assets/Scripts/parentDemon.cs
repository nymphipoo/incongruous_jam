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
    protected LayerMask creatureCollisionLayers=9;
    [SerializeField]
    EvolutionFood[] foodNameToEvo;
    [System.Serializable]
    public class EvolutionFood
    {
        public string foodTag;
        public GameObject prefabToEvolveTo;
    }

    protected SpriteRenderer spriteRef;

    private void Awake()
    {
        GameObject cc = GameObject.Find("/CreatureCounter");
        if (cc) {
            creatureCounter ccs=  cc.GetComponent<creatureCounter>();
            if (ccs)
            {
                creatureCounterScript = ccs;
                creatureCounterScript.AddCreature(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        spriteRef = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    virtual protected void updateVelocity()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EncounteredFood(collision);
    }

    virtual protected void EncounteredFood(Collision2D collision)
    {
        eatable food = collision.gameObject.GetComponent<eatable>();
        if (food)
        {
            foreach(EvolutionFood eachPair in foodNameToEvo) { 
            //this will be changed when we determine how to this
                if (eachPair.foodTag==food.foodName)
                {
                    Instantiate(eachPair.prefabToEvolveTo, transform.position, transform.rotation);
                    //we ideally would have a custom destroy function for all the items too
                    food.Eaten(transform);
                    Evolving(food.foodName, eachPair.prefabToEvolveTo.name);
                }
            }
        }
    }

    virtual protected void Evolving(string foodName, string evolutionName)
    {
        if (creatureCounterScript)
            creatureCounterScript.evolved(gameObject);
        Destroy(gameObject);
    }

    virtual public void Killed()
    {
        if(creatureCounterScript)
            creatureCounterScript.RemoveCreature(gameObject,false);
        Destroy(gameObject);
    }

    virtual public void Escaped() {
        if (creatureCounterScript)
            creatureCounterScript.RemoveCreature(gameObject, true);
        Destroy(gameObject);
    }
}



