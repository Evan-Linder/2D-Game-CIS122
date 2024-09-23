using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private State state;
    private EnemyNavigation enemyNavigation; // reference enemy navigation class

    private void Awake()
    {
        enemyNavigation = GetComponent<EnemyNavigation>();
        state = State.Roaming;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RoamingRoutine());
    }
    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming) // continue while in roaming state
        {
            Vector2 roamPosition = GetRoamingPosition(); // get roaming position
            enemyNavigation.MoveTo(roamPosition); // move to roaming position
            yield return new WaitForSeconds(8f); // wait for 8 seconds
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; //generate random 2d vector.
    }
}
