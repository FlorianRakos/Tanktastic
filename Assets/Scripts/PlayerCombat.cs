using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform projectileExitMain;
    [SerializeField] private Transform projectileExitRocket;

    [SerializeField] private Rigidbody projectileMain;
    [SerializeField] private Rigidbody projectileRocket;

    [SerializeField] private float reloadDelayMain = 2f;
    [SerializeField] private float reloadDelayRocket = 4f;

    private float reloadTimerMain;
    private float reloadTimerRocket;


    void Awake()
    {
        reloadTimerMain = reloadDelayMain;
        reloadTimerRocket = reloadDelayRocket;
    }


    void Update()
    {
        ResetDelays();
        ReadInput();                
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
