using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentDemon : MonoBehaviour
{
    protected Rigidbody2D rb2d;

    [SerializeField] protected Vector2 internalSpeed;
    [SerializeField] protected Vector2 externalSpeed;

    [SerializeField] protected float externalSpeedDecayRate = .01f;//this default value will decrease external speed by 1 a second;

    [SerializeField] protected bool parentExternalSpeedDecay = true;

    [SerializeField]
    Dictionary<string, parentDemon> foodNameToEvo;

    // Start is called before the first frame update
    protected void Start()
    {
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
            //this will be changed when we determine how to this
            if (foodNameToEvo.ContainsKey(food.foodName))
            {
                Instantiate(foodNameToEvo[food.name], transform.position, transform.rotation);
                //we ideally would have a custom destroy function for all the items too
                food.Eaten(transform);
                Evolving();
            }
        }
    }

    virtual protected void Evolving()
    {
        Destroy(gameObject);
    }

    virtual protected void Killed()
    {
        Destroy(gameObject);
    }
}



