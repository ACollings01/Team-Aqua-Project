using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }

    private static GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Returns the player GameObject
    public GameObject GetPlayer()
    {
        return _player;
    }

    // Script will be filled later, will activate when player steps on a magic circle
    public void ActivateMCircle()
    {
        Debug.Log("Player Stepped On A Magic Circle");
    }
}
