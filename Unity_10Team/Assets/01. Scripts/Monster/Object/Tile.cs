using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public void Activate(Vector3 position, Transform parent)
    {
        transform.position = position;
        transform.SetParent(parent);
        gameObject.SetActive(true);
    }
}
