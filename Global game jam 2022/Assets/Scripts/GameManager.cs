using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            dayPlatform.gameObject.SetActive(true);
        }

        //Deactivates night objects
        foreach (GameObject nightPlatform in nightItems)
        {
            nightPlatform.gameObject.SetActive(false);
        }
    }

    void SwitchToNight()
    {
        //Plays the background animation
        backgroundAnim.Play("SwitchToNight");
        
        //Activates all night objects
        foreach (GameObject nightPlatform in nightItems)
        {
            nightPlatform.gameObject.SetActive(true);
        }

        //Deactivates all day objects
        foreach (GameObject dayPlatform in dayItems)
        {
            dayPlatform.gameObject.SetActive(false);
        }
    }
}
