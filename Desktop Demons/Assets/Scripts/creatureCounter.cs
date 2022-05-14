using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureCounter : MonoBehaviour
{
    List<GameObject> activeCreatures;

    List<string> killedList;
    List<string> escapedList;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCreature(GameObject newCreature)
    {
        activeCreatures.Add(newCreature); 
    }

    public void evolved(GameObject oldcreature)
    {
        activeCreatures.Remove(oldcreature);
    }

    public void RemoveCreature(GameObject deadCreature, bool escaped)
    {
        activeCreatures.Remove(deadCreature);

        if (escaped)
        {
            escapedList.Add(deadCreature.name);
        }
        else {
            killedList.Add(deadCreature.name);
        }
    }

}
