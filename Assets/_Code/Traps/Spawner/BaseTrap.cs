using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrap : MonoBehaviour
{
    protected IEnumerable<PlayerController> GetPlayers()
    {
        return FindObjectsOfType<PlayerController>();
    }

}
