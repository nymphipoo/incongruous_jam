using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoltionDelay : MonoBehaviour
{
    [SerializeField] protected float evolutionDelay = 1;
    [SerializeField] protected float evolutionDelayStep = .1f;
    [SerializeField] GameObject sparkles;

    [SerializeField] AudioClip evolveSound1;
    [SerializeField] AudioClip evolvesSound2;


    public IEnumerator EvolutionUnderway(GameObject newCreature)
    {
        GetComponent<parentDemon>().enabled = false;
        int soundEffect = Random.Range(0, 2);
        
        if (soundEffect == 0)
            GetComponent<AudioSource>().clip = evolveSound1;
        else
            GetComponent<AudioSource>().clip = evolvesSound2;
        GetComponent<AudioSource>().Play();

        Vector3 pos = transform.position;
        for (float i = 0; i < evolutionDelay; i += evolutionDelayStep)
        {
            //transform.position = pos;
            if (sparkles != null)
            {
                Instantiate(sparkles, transform.position + new Vector3(Random.Range(-.1f, .1f), Random.Range(-.5f, .5f), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));

            }
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
