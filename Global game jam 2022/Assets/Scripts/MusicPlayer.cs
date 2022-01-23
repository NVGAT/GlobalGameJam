using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    enum PlayerType { MENU, GAME };

    [SerializeField] private PlayerType type;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (type)
        {
            case PlayerType.MENU:
                if (scene.name == "Level1")
                {
                    Destroy(gameObject);
                }
                break;
            case PlayerType.GAME:
                if (!scene.name.Contains("Level"))
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
