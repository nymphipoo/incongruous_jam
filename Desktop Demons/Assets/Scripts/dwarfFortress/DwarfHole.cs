using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfHole : MonoBehaviour
{
    [SerializeField] Transform killlocation;

    [SerializeField] float increment = .005f;
    [SerializeField] float pauselength = .01f;     

    public IEnumerator escape(GameObject dwarf)
    {
        dwarf.GetComponent<BoxCollider2D>().enabled = false;
        print("here at start");
        for (float i = 0; i < 1; i += increment) {
            dwarf.transform.position= Vector3.Lerp(transform.position, killlocation.position, i);
            yield return new WaitForSeconds(pauselength);
        }
        print("here at finished");
        dwarf.GetComponent<DwarfDemon>().enabled = true;
        dwarf.GetComponent<DwarfDemon>().Escaped();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DwarfDemon ds = other.gameObject.GetComponent<DwarfDemon>();
        if (ds) {
            ds.enabled = false;
            ds.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StartCoroutine( escape(ds.gameObject));
        }
    }
}
