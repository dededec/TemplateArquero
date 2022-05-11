using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChapter : MonoBehaviour
{
    public World world;
    public KeyCode startTestKey;

    // Start is called before the first frame update
    void Update()
    {
        // world.rewards[0].reward.Invoke();
        // world.highestStageReached = 5;
        if(Input.GetKeyDown(startTestKey))
        {
            WorldManager.AssignWorld(world);
            WorldManager.Play();
        }
    }
}
