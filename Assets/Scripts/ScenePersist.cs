using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    /* в Инспекторе игров. объект со скриптом ScenePersist служит для сохранения количества монеток
     * поднятых и не поднятых после смерти игрока, то есть игрок поднял монетки, погиб, начал сначала
     * но поднятые монетки не появились. Без этого скрипта можно было поднять монетку А, погибнуть, поднять её снова
     */
    int startingSceneIndex;


    // синглтон на Awake 
    private void Awake()
    {
        //startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // на старте берёт индекс сцены в которой появился
    void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // на апдейте проверяет разницу между текущим индексом и стартовым
    /* В прочем, такой подход не работает! Дело в том, что перейдя в другую сцену 
     * ScenePersist думает что ДРУГАЯ сцена это СТАРТОВАЯ сцена, так что он никуда
     * не исчезает! В остальном свою задачу данный скрипт делает */
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }
    }

    // обязательно выпиливать ScenePersist при переходе на новый уровень и новой игре
    public void WipeScenePersistence()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
