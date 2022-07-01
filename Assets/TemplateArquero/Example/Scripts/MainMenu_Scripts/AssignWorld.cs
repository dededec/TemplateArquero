using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignWorld : MonoBehaviour
{
    [SerializeField] private World[] worlds;
    [SerializeField] private SwipeMenu swipeMenu;

    public void assignCurrentWorld()
    {
        WorldManager.AssignWorld(worlds[swipeMenu.currentSelectedWorld]);
    }
}
