using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material redFlashMat; //material for flash effect
    [SerializeField] private float restoreDefaultMatTime = .2f; // duration of flash

    private Material defaultMat; 
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth; 

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        defaultMat = spriteRenderer.material; // Store default material
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = redFlashMat; // change to flash material
        yield return new WaitForSeconds(restoreDefaultMatTime); //wait to restore
        spriteRenderer.material = defaultMat; // Restore default material
        enemyHealth.DetectDeath(); // check for enemy death
    }
}

