using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoltionDelay : MonoBehaviour
{
    [SerializeField] protected float evolutionDelay = 1;
    [SerializeField] protected float evolutionDelayStep = .1f;


    public IEnumerator EvolutionUnderway(GameObject newCreature)
    {
        GetComponent<parentDemon>().enabled = false;

        Vector3 pos = transform.position;
        for (float i = 0; i < evolutionDelay; i += evolutionDelayStep)
        {
            //transform.position = pos;
            yield return new WaitForSeconds(evolutionDelayStep);
        }

        CreateNewCreature(newCreature);
        Destroy(gameObject);
    }

    protected void CreateNewCreature(GameObject evolution)
    {
        GameObject newCreature = Instantiate(evolution, transform.position, transform.rotation);
        newCreature.transform.position = transform.position;
        newCreature.transform.parent = transform.parent;
        newCreature.name = evolution.name;
        Destroy(gameObject);
    }
}
