using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    Animator myAnimator;
    [SerializeField] float pickUpDelay = 1f;
    [SerializeField] AudioClip pickUpSound;
    [SerializeField] int pointsPerCoin = 300;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(PickUpCoin());
    }

    IEnumerator PickUpCoin()
    {
        var myCollider = GetComponent<BoxCollider2D>();
        myCollider.enabled = false;
        AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position);
        myAnimator.SetTrigger("PickUp");
        FindObjectOfType<GameSession>().AddToScore(pointsPerCoin);
        yield return new WaitForSecondsRealtime(pickUpDelay);
        Destroy(gameObject);
    }
}
