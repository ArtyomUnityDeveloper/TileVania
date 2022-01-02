using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadingDelay = 2f;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;
    [SerializeField] AudioClip exitSound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<ScenePersist>().WipeScenePersistence();
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = LevelExitSlowMoFactor;
        AudioSource.PlayClipAtPoint(exitSound, Camera.main.transform.position);
        yield return new WaitForSecondsRealtime(loadingDelay);
        Time.timeScale = 1f;

        //Destroy(FindObjectOfType<ScenePersist>().gameObject); 

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
