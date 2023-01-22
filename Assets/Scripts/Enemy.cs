using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionSound;
    CameraController camController;

    private void Start() {
        camController = FindObjectOfType<CameraController>();
        Debug.Log("Cam controller "+camController);
    }

    public void TakeDamage(){
        health--;
        camController.Shake();
        if (health <= 0){
            Kill();
        }
    }

    public void Kill(){
        Destroy(gameObject, .1f);
        GameObject go = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        GameManager.instance.Score += 1;
    }
}
