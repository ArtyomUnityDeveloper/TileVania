using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip ("Game units per second")]
    [SerializeField] float scrollRate = 0.125f;

    //данный скрипт обеспечивает поднятие уровня воды на последней локации
 
    void Update()
    {
        float yMove = scrollRate * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
        // в лекции сказано что это самый "чистый"
        // способ двигать воду
        // изначально я двигал через 
        // waterRigidbody.velocity по аналогии с игроком
    }
}
