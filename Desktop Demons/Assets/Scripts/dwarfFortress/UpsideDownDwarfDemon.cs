using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpsideDownDwarfDemon : DwarfDemon
{
    public override void Jump()
    {
        internalSpeed.y = -jumpStrength;
    }

    //public override 
}

