using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puse : MonoBehaviour
{
    public bool isPaused;
    public Canvas pauseScreen;
    // Start is called before the first frame update
    private void Awake()
    {
        pauseScreen = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            pauseScreen.enabled = true;
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.enabled = false;
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }
}
