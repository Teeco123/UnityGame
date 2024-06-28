using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingPosition : MonoBehaviour
{
    string uId;
    public Vector3 startingPosition;
    public Quaternion startingRotation;

    void Awake()
    {
        IDGenerator idGenerator = gameObject.GetComponent<IDGenerator>();
        uId = idGenerator.guid;

        transform.position = ES3.Load("GOPosition: " + uId, startingPosition);
        transform.rotation = ES3.Load("GORotation: " + uId, startingRotation);
    }

    void OnDestroy()
    {
        ES3.Save("GOPosition: " + uId, transform.position);
        ES3.Save("GORotation: " + uId, transform.rotation);
    }
}
