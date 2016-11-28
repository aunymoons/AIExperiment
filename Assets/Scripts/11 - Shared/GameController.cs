using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    //REFERENCE
    public Animator animator;
    //STATE
    public bool isMultiplayer;
    public bool isGameOver;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //MAIN
    public virtual void WinGame(string winTeam, string looseTeam)
    {

    }
    public virtual void LooseGame()
    {

    }
    public virtual void PauseGame()
    {

    }
}
