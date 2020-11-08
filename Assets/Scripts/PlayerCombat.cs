using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform projectileExitMain;
    [SerializeField] Transform projectileExitRocket;

    [SerializeField] Rigidbody projectileMain;
    [SerializeField] Rigidbody projectileRocket;


    [SerializeField] float reloadDelayMain = 2f;
    [SerializeField] float reloadDelayRocket = 4f;

    float reloadTimerMain;
    float reloadTimerRocket;


    void Awake()
    {
        reloadTimerMain = reloadDelayMain;
        reloadTimerRocket = reloadDelayRocket;
    }

    void Update()
    {
        ReadInput();        
        ResetDelays();
    }

    private void ResetDelays()
    {
        if (reloadTimerMain < reloadDelayMain) {
            reloadTimerMain += Time.deltaTime;
        }

        if (reloadTimerRocket < reloadDelayRocket) {
            reloadTimerRocket += Time.deltaTime;
        }
    }

    void ReadInput() {
        if(Input.GetMouseButtonDown(0) && reloadTimerMain >= reloadDelayMain ) {
            ProcessFireMain();
            reloadTimerMain = 0f;
        }

        if (Input.GetMouseButtonDown(1) && reloadTimerRocket >= reloadDelayRocket)
        {
            ProcessFireRocket();
            reloadTimerRocket = 0f;
        }
    }

    private void ProcessFireMain()
    {
        Rigidbody projectileInstance = Instantiate(projectileMain, projectileExitMain.position, projectileExitMain.rotation);
    }

    private void ProcessFireRocket()
    {
        Rigidbody projectileInstance = Instantiate(projectileRocket, projectileExitRocket.position, projectileExitRocket.rotation);
    }
}
