using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eatable : MonoBehaviour
{
    public string foodName = "";

    //i will probably make some animation later
    public void Eaten(Transform demonlocation) {
        Destroy(gameObject);
    }
}
