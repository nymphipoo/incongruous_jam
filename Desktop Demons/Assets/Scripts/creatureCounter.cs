using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creatureCounter : MonoBehaviour
{
    [SerializeField] string frontPage = "FrontPage";
    [SerializeField] string instructionPage = "Instructions";
    [SerializeField] string mainGame = "Game";
    [SerializeField] string endingSceneGood = "GoodEnding";
    [SerializeField] string endingSceneBad = "BadEnding";
    
    List<GameObject> activeCreatures;

    public List<string> killedList;
    public List<string> escapedList;

    public static creatureCounter instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            print("duplicate rage managers!!");

        }
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
    }

    public void maingame()
    {
        print("going to main game");
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
