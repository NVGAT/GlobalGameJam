using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] slides;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameMusicPlayer;
    private PlayerMovement playerMovement;
    [SerializeField] private int currentSlide;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Cutscene")
        {
            /*
             Slideshow logic: if we press left click while in the cutscene, we loop through to the next frame. If we get an
             IndexOutOfBoundsException that means that we've reached the end of the cutscene and we load the first level
             */
            slides[currentSlide].gameObject.SetActive(false);
            currentSlide++;
            try
            {
                slides[currentSlide].gameObject.SetActive(true);
            }
            catch (IndexOutOfRangeException)
            {
                SceneManager.LoadScene("Level1");
                Instantiate(gameMusicPlayer);
            }
        }
    }

    public void GameOver()
    {
        playerMovement.canMove = false;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Play()
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
