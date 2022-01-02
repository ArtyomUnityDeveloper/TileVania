using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    /* � ���������� �����. ������ �� �������� ScenePersist ������ ��� ���������� ���������� �������
     * �������� � �� �������� ����� ������ ������, �� ���� ����� ������ �������, �����, ����� �������
     * �� �������� ������� �� ���������. ��� ����� ������� ����� ���� ������� ������� �, ���������, ������� � �����
     */
    int startingSceneIndex;


    // �������� �� Awake 
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

    // �� ������ ���� ������ ����� � ������� ��������
    void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // �� ������� ��������� ������� ����� ������� �������� � ���������
    /* � ������, ����� ������ �� ��������! ���� � ���, ��� ������� � ������ ����� 
     * ScenePersist ������ ��� ������ ����� ��� ��������� �����, ��� ��� �� ������
     * �� ��������! � ��������� ���� ������ ������ ������ ������ */
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }
    }

    // ����������� ���������� ScenePersist ��� �������� �� ����� ������� � ����� ����
    public void WipeScenePersistence()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
