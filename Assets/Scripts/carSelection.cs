using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSelection : MonoBehaviour
{
    private GameObject[] carList;
    private int currentCar = 0;

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

    public void ToggleCars(string direction) {
        carList[currentCar].SetActive(false);

        if (direction == "Right") {
            currentCar++;
            if (currentCar > carList.Length - 1) {
                currentCar = 0;
            }
        } else if (direction == "Left") {
            currentCar--;
            if (currentCar < 0) {
                currentCar = carList.Length - 1;
            }
        }

        //set the current car to be active from the list
        carList[currentCar].SetActive(true);
    }
}
