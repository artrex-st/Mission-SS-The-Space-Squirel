using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    public static string map = "fase";
    public static int cont = 0;
    public static float recarga = 1;
    public GameObject[] datas;
    public static AudioClip[] musicas;

    void Awake()
    {
        datas = GameObject.FindGameObjectsWithTag("Data");
        if (datas.Length >= 2)
        {
            Destroy(datas[0]);
        }
        DontDestroyOnLoad(transform.gameObject);
    }
    void Update()
    {
        //string x = map + cont;
        //if(Application.loadedLevelName != x)
        recarga -= Time.deltaTime;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Cenariodojogo");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
