using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShakeForever : MonoBehaviour
{
    [SerializeField] float shakeForce,shakeFreq;
    Vector3 originalPos;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shakeFreq)
        {
            timer = 0;
            transform.position = new Vector3(Random.Range(originalPos.x - shakeForce, originalPos.x + shakeForce), Random.Range(originalPos.y - shakeForce, originalPos.y + shakeForce), originalPos.z);
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
