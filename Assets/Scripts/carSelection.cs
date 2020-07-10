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
        // Get the cloudParticle prefab we created in Resources folder to add to the car
        GameObject cloudSystem = (GameObject)Instantiate(Resources.Load("cloudParticle"));
        // Create dynamic particle system named cloudPuff which will grab the loaded cloudSystem
        // and then get the ParticleSystem component (a tab under Inspector in Unity)
        ParticleSystem cloudPuff = cloudSystem.GetComponent<ParticleSystem>();
        cloudPuff.Play();
        // Set transform position of cloudPuff as the same position of the current object this script is attached to (cars)
        //cloudPuff.transform.position = transform.position;
        // This creates position in specified X, Y and Z.
        cloudPuff.transform.position = new Vector3(21.85f, -1f, -3f);
        // Destroy after 2 seconds (same duration specified in Unity for our cloudPuff) since new ones create clones (via Instantiate above)
        Destroy(cloudSystem, 2f);
    }
}
