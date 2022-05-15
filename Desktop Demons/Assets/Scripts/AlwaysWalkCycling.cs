using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysWalkCycling : MonoBehaviour
{
    [SerializeField] protected Sprite[] walkFrames;
    [SerializeField] float frameTime;
    SpriteRenderer spriteRef;
    float timer = 0;
    int walkFrameIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRef = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > frameTime)
        {
            walkFrameIndex++;
            if (walkFrameIndex >= walkFrames.Length)
            {
                walkFrameIndex = 0;
            }
            spriteRef.sprite = walkFrames[walkFrameIndex];
            timer = 0;
        }
    }
}
