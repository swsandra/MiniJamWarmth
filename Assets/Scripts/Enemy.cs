using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] float invincibleDuration;
    [SerializeField] Material flashMaterial;
    CameraController camController;
    SpriteRenderer spriteRenderer;
    Material defaultMaterial;
    Color originalColor;
    Coroutine flashRoutine;
    bool canTakeDamage;

    private void Start() {
        camController = FindObjectOfType<CameraController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        canTakeDamage = true;
        originalColor = spriteRenderer.color;
        flashRoutine = null;
    }

    public void TakeDamage(){
        if (!canTakeDamage) return;
        canTakeDamage = false;
        health--;
        camController.Shake();
        if (health <= 0){
            Kill();
        }else{
            Flash();
        }
    }

    void Flash(){
        if (flashRoutine != null){
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine(){
        spriteRenderer.color = Color.white;
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(invincibleDuration);
        spriteRenderer.color = originalColor;
        spriteRenderer.material = defaultMaterial;
        flashRoutine = null;
        canTakeDamage = true;
    }

    public void Kill(){
        Destroy(gameObject, .1f);
        GameObject go = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        GameManager.instance.Score += 1;
    }
}
