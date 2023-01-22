using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float stompRate;
    [SerializeField] AudioClip stompSound;
    [SerializeField] AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Vector3 botLeft;
    Vector3 halfWidth;
    Rigidbody2D rb;
    Vector2 movement;
    bool canStomp;
    Animator animator;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Bottom left point of sprite
        botLeft = spriteRenderer.transform.TransformPoint(spriteRenderer.sprite.bounds.min);
        halfWidth = new Vector3(spriteRenderer.bounds.size.x/2, 0, 0);
        canStomp = true;
    }

    private void FixedUpdate() {
        rb.velocity = movement * speed;
    }

    private void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movement = new Vector2(movementVector.x, movementVector.y);
        if (movement.x < 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void OnFire(){
        botLeft = spriteRenderer.transform.TransformPoint(spriteRenderer.sprite.bounds.min);
        Collider2D[] cols = Physics2D.OverlapAreaAll(botLeft, transform.position + halfWidth);
        if (canStomp){
            StompAnimation();
            // Attack enemies
            for (int i=0; i < cols.Length; i++){
                if (cols[i].CompareTag("Enemy")){ // Check Collision
                    cols[i].GetComponent<Enemy>().TakeDamage();;
                }
            }
        }
    }

    void StompAnimation(){
        // Debug.Log("Stomp");
        animator.SetBool("IsStomping", true);
        canStomp = false;
    }

    public void playStomp(){
        audioSource.PlayOneShot(stompSound);
    }

    void StopStomp(){
        // Debug.Log("Stopping stomp");
        animator.SetBool("IsStomping", false);
        canStomp = true;
    }

    // IEnumerator StompCooldown(){
    //     Debug.Log("Stomp cooldown");
    //     yield return new WaitForSeconds(1/stompRate);
    //     Debug.Log("Finish tomp cooldown");
    //     canStomp = true;
    // }

    public void GameOver(){
        rb.velocity = Vector2.zero;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerInput>().enabled = false;
    }
}
