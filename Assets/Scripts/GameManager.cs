using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameManagerDelegate();

    public static GameManagerDelegate ResetLevelDelegate;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameDataScript.LoadLevelDataFromJson();
    }
}