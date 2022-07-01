using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HeroSpawnPosition : MonoBehaviour
{
    [SerializeField] private GameObject[] _heroes;
    private GameObject CurrentCar;
    [SerializeField] private GameObject _camera; 

    private void Awake() {
        //_camera = GameObject.FindGameObjectWithTag("CinemachineCamera");

        foreach(GameObject hero in _heroes)
        {
            if(hero.name == SaveDataController.equippedCar)
            {
                CurrentCar = Instantiate(hero, gameObject.transform.position, Quaternion.identity);
                break;
            }
        }

        //_camera.GetComponent<CinemachineVirtualCamera>().Follow = CurrentCar.transform;

    }
}
