using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfWalkCycle : AlwaysWalkCycling
{

    [SerializeField] Sprite[] Blue;
    [SerializeField] Sprite[] Green;
    [SerializeField] Sprite[] yellow;

    private void Awake()
    {
        int dwarfColor = Random.Range(0, 3);
        if (dwarfColor == 0)
        {
            walkFrames = yellow;
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
}
