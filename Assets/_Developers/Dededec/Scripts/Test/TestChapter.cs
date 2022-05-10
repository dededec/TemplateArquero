using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChapter : MonoBehaviour
{
    public World world;

    // Start is called before the first frame update
    void Start()
    {
        // world.rewards[0].reward.Invoke();
        // world.highestStageReached = 5;
        StageManager.AssignWorld(world);
        StageManager.Play();
    }
}
