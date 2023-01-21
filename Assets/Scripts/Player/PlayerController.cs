using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float stompRate;
    SpriteRenderer spriteRenderer;
    Vector3 botLeft;
    Vector3 halfWidth;
    Rigidbody2D rb;
    float movX;
    float movY;
    bool canStomp;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Bottom left point of sprite
        botLeft = spriteRenderer.transform.TransformPoint(spriteRenderer.sprite.bounds.min);
        halfWidth = new Vector3(spriteRenderer.bounds.size.x/2, 0, 0);
        canStomp = true;
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(movX, movY) * speed;
    }

    private void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movX = movementVector.x;
        movY = movementVector.y;
    }

    private void OnFire(){
        botLeft = spriteRenderer.transform.TransformPoint(spriteRenderer.sprite.bounds.min);
        Collider2D col = Physics2D.OverlapArea(botLeft, transform.position + halfWidth);
        if (col && col.CompareTag("Enemy") && canStomp){ // Check Collision
            Stomp(col.GetComponent<Enemy>());
        }
    }

    void Stomp(Enemy enemy){
        Debug.Log("Stomp");
        enemy.TakeDamage();
        canStomp = false;
        StartCoroutine(StompCooldown());
    }

    IEnumerator StompCooldown(){
        yield return new WaitForSeconds(1/stompRate);
        canStomp = true;
    }
}
