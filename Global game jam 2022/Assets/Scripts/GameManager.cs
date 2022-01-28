using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Components and assignables")]
    [SerializeField] private Animator backgroundAnim;
    [SerializeField] private GameObject[] dayItems;
    [SerializeField] private GameObject[] nightItems;
    [Header("Values")]
    [SerializeField] private bool isDay;

    private void Start()
    {
        SwitchToDay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //When we press space, the game switches to the opposite daytime
            isDay = !isDay;
            SwitchDay(isDay);
        }
    }

    void SwitchDay(bool isDaytime)
    {
        if (isDaytime)
        {
            SwitchToNight();
        }
        else
        {
            SwitchToDay();
        }
    }

    void SwitchToDay()
    {
        //Plays the animation on the background
        backgroundAnim.Play("SwitchToDay");

        //Activates all day objects
        foreach (GameObject dayPlatform in dayItems)
        {
            try
            {
                dayPlatform.gameObject.SetActive(true);
            }
            catch (MissingReferenceException)
            {
                //did this so i dont have to cope with the errors :)
            }
        }

        //Deactivates night objects
        foreach (GameObject nightPlatform in nightItems)
        {
            try
            {
                nightPlatform.gameObject.SetActive(false);
            }
            catch (MissingReferenceException)
            {

            }
        }
    }

    void SwitchToNight()
    {
        //Plays the background animation
        backgroundAnim.Play("SwitchToNight");

        //Activates all night objects
        foreach (GameObject nightPlatform in nightItems)
        {
            try
            {
                nightPlatform.gameObject.SetActive(true);
            }
            catch (MissingReferenceException)
            {

            }
        }

        //Deactivates all day objects
        foreach (GameObject dayPlatform in dayItems)
        {
            try
            {
                dayPlatform.gameObject.SetActive(false);
            }
            catch (MissingReferenceException)
            {

            }
        }
    }
}
