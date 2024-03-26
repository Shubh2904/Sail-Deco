using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static GM singleton;

    public static GameObject player;
    public static InteractRange interactRange;
    public static Boat playersCurrentBoat;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;  

        player = GameObject.Find("Player"); //todo, make better 
    }

    public void resetGame()
    {
        SceneManager.LoadScene(0);
    }

}
