using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

     void Update() {
        Vector3 direction = new Vector3(10f, 0f, 0f);
        transform.position += direction * Time.deltaTime;


    }
}

public class Simulation : MonoBehaviour 
{
    GameObject []  = gameObjects;



    private void Update() {
        foreach(GameObject gameObject in gameObjects) {
            Vector3 direction = new Vector3(10f, 0f, 0f);
            gameObject.transform.position += direction * Time.deltaTime;

            List<float> list;

        


        }
    }
}
