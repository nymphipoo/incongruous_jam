using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfWalkCycle : AlwaysWalkCycling
{

    [SerializeField] Sprite[] Red;
    [SerializeField] Sprite[] Blue;
    [SerializeField] Sprite[] Green;

    private void Awake()
    {
        int dwarfColor = Random.Range(0, 2);
        if (dwarfColor == 0)
        {
            walkFrames = Red;
        }
        else if (dwarfColor == 1)
        {
            walkFrames = Blue;
        }
        else if (dwarfColor == 2)
        {
            walkFrames = Green;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
