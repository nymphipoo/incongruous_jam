using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creatureCounter : MonoBehaviour
{
    [SerializeField] string frontPage = "FrontPage";
    [SerializeField] string instructionPage = "Instructions";
    [SerializeField] string mainGame = "Game";
    [SerializeField] string endingSceneGood = "endingGood";
    [SerializeField] string endingSceneBad = "BadEnding";

    [SerializeField] AudioClip escape;
    

    public List<string> activeCreatures= new List<string>();

    public List<string> killedList = new List<string>();
    public List<string> escapedList = new List<string>();

    public static creatureCounter instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            print("duplicate CC!!");

        }
    }

    private void Start()
    {
        GetComponent<AudioSource>().clip = escape;
    }

    private void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            if (SceneManager.GetActiveScene().name == frontPage)
            {
#if UNITY_WEBGL
#else
                print("quitting");
                Application.Quit();
#endif
            }
            else
            {
                SceneManager.LoadScene(frontPage, LoadSceneMode.Single);
            }
        }
        else if (Input.GetButtonDown("mute")) {
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = !Camera.main.gameObject.GetComponent<AudioListener>().enabled;
        }
    }

    public void ResetStuff()
    {
        activeCreatures = new List<string>();
        killedList = new List<string>();
        escapedList = new List<string>();
    }

    public void maingame()
    {
        ResetStuff();
;        print("going to main game");
        SceneManager.LoadScene(mainGame, LoadSceneMode.Single);
    }

    public void Instructions()
    {
        print("going to instructions");
        SceneManager.LoadScene(instructionPage, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        print("exiting game");
        Application.Quit();
    }

    public void EndGameGood()
    {
        print("going to good end");
        SceneManager.LoadScene(endingSceneGood, LoadSceneMode.Single);
    }

    public void EndGameBad()
    {
        print("going to bad end");
        SceneManager.LoadScene(endingSceneBad, LoadSceneMode.Single);
    }

    public void TitleScreen()
    {
        print("going to title screen");
        SceneManager.LoadScene(frontPage, LoadSceneMode.Single);
    }

    public void AddCreature(string newCreature)
    {
        activeCreatures.Add(newCreature); 
    }

    public void evolved(string oldcreature)
    {
        activeCreatures.Remove(oldcreature);
    }

    public void RemoveCreature(string deadCreature, bool escaped)
    {
        activeCreatures.Remove(deadCreature);

        if (escaped)
        {
            GetComponent<AudioSource>().Play();
            escapedList.Add(deadCreature);
        }
        else {
            killedList.Add(deadCreature);
        }
        isGameOver();
    }

    public void isGameOver()
    {
        if (activeCreatures.Count == 0) {
            if (escapedList.Count >= killedList.Count) {
                EndGameGood();
            }
            else {
                EndGameBad();
            }
        }

    }
}
