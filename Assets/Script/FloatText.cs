using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatText : MonoBehaviour
{

    public float speed = 5f;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        if (gameObject.activeSelf)
            transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
