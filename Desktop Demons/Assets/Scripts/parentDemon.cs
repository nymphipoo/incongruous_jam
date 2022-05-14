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



    // Start is called before the first frame update
    protected void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void updateVelocity()
    {
        Movement();
        rb2d.velocity = internalSpeed+externalSpeed;
    }

    private void Movement()
    {
        if (parentExternalSpeedDecay)
        {
            ExternalSpeedDecay();
        }
        Vector2 vel = internalSpeed + externalSpeed;
        //move player in the direction of the movement coords
        rb2d.MovePosition(rb2d.position + vel * Time.fixedDeltaTime);
    }


    public void AddExternalSpeed(Vector2 newSpeed)
    {
        externalSpeed += newSpeed;
    }

    public void SetExternalSpeed(Vector2 newSpeed)
    {
        externalSpeed = newSpeed;
    }

    protected void ExternalSpeedDecay()
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
}



