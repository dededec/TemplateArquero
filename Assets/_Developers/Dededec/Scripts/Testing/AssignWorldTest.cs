using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AssignWorldTest : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private World _world;

    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(delegate { WorldManager.AssignWorld(_world);});
    }

}
