using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float radius = 1f;

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        Deactivate();
    }
}
