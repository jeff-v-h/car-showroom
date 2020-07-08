using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSelection : MonoBehaviour
{
    private GameObject[] carList;

    // Start is called before the first frame update
    void Start() {
        carList = new GameObject[transform.childCount];

        // Loop through the child items and fill the list in the correct slots
        for (int i = 0; i < transform.childCount; ++i) {
            carList[i] = transform.GetChild(i).gameObject;
        }

        // Deactivate all the gameObjects in the list
        foreach(GameObject gameObj in carList) {
            gameObj.SetActive(false);
        }

        // Set the initial GO (GameObject) to be active
        if (carList[0]) {
            carList[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
