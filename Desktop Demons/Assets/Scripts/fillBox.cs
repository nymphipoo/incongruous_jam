using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillBox : MonoBehaviour
{
    creatureCounter cc;
    [SerializeField] Transform topRightCorner;
    [SerializeField] Transform bottomLeftCorner;

    [SerializeField] GameObject[] prefabList;
    [SerializeField] List<string> displayList;

    [SerializeField]bool isDeadDisplay;

    private void Awake()
    {
        cc = creatureCounter.instance;
        if (!cc) {
            print("ERROR: CANNOT FIND CREATURE COUNTER!!");
        }
    }

    private void Start()
    {
        cc = creatureCounter.instance;
        if (!cc)
        {
            print("ERROR: CANNOT FIND CREATURE COUNTER!!");
        }

        if (isDeadDisplay) { 
            displayList = cc.killedList;
            cc.killedList =new List<string>();
        }
        else { 
            displayList = cc.escapedList;
            cc.escapedList = new List<string>();
        }

        for (int i =0;i<displayList.Count;i++) {
            spawn(displayList[i]);

        }

    }

    public void spawn(string name)
    {
        float x = Random.Range(topRightCorner.position.x, bottomLeftCorner.position.x);
        float y = Random.Range(topRightCorner.position.y, bottomLeftCorner.position.y);
        for (int i = 0; i < prefabList.Length; i++)
        {
            if (prefabList[i].name == name) {
                GameObject creature = Instantiate(prefabList[i]);
                creature.GetComponent<parentDemon>().enabled = false;
                creature.GetComponent<Collider2D>().enabled = false;
                creature.transform.position = new Vector3(x, y, 0);
            }
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
