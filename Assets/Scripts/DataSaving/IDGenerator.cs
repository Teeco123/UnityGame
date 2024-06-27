using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDGenerator : MonoBehaviour
{
    public string guid = System.Guid.NewGuid().ToString();
}
