using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextGame : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        int manhientai = SceneManager.GetActiveScene().buildIndex;
        int quaman = manhientai + 1;
        if(quaman == SceneManager.sceneCountInBuildSettings)
        {
            quaman = 0;
        }
        SceneManager.LoadScene(quaman);
    }
}
