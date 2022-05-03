using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EconomyManager : MonoBehaviour
{
    public abstract bool Pay(int amount);
    public abstract void Add(int amount);

}
