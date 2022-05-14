using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// simple fake particle objects
/// </summary>
public class EffectAppear : MonoBehaviour
{
    [SerializeField] float lifetime,fallSpeed,fallAccel,spinSpeed;
    // Start is called before the first frame update
    SpriteRenderer spriteRef;
    float timer = 0;
    void Start()
    {
        if (!spriteRef) { spriteRef = GetComponent<SpriteRenderer>(); }
    }
    public void GiveLife(Sprite newSprite,float newLifeTime,float newFallSpeed=0,float newFallAccel=0,float newSpinSpeed=0)
    {
        if (!spriteRef) { spriteRef = GetComponent<SpriteRenderer>(); }
        lifetime = newLifeTime;
        fallSpeed = newFallSpeed;
        fallAccel = newFallAccel;
        spinSpeed = newSpinSpeed;
        spriteRef.sprite = newSprite;
    }
    // Update is called once per frame
    void Update()
    {
        if (spinSpeed > 0)
        {
            transform.Rotate(new Vector3(0,0,spinSpeed * Time.deltaTime));
        }
        if (fallSpeed > 0)
        {
            transform.position +=Vector3.down* fallSpeed * Time.deltaTime;
            if (fallAccel>0)
            {
                fallSpeed += fallAccel;
            }
        }

        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
