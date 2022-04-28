using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChapter : MonoBehaviour
{
    public Chapter chapter;

    // Start is called before the first frame update
    void Start()
    {
        chapter.rewards[0].reward.Invoke();
        chapter.highestStageReached = 5;
    }
}
