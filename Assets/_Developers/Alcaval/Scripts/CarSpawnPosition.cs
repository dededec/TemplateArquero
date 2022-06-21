using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarSpawnPosition : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    private GameObject CurrentCar;
    [SerializeField] private GameObject cm; 

    private void Awake() {
        cm = GameObject.FindGameObjectWithTag("CinemachineCamera");

        foreach(GameObject car in cars)
        {
            if(car.name == SaveDataController.equippedCar)
            {
                CurrentCar = Instantiate(car, gameObject.transform.position, Quaternion.identity);
                break;
            }
        }

        cm.GetComponent<CinemachineVirtualCamera>().Follow = CurrentCar.transform;

    }
}
