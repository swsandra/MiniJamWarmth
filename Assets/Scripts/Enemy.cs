using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;

    public void TakeDamage(){
        health--;
        if (health <= 0){
            Kill();
        }
    }

    public void Kill(){
        Destroy(gameObject, .1f);
    }
}
