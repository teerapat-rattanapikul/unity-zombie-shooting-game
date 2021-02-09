using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            // load boss level
            GameManager.Instance.ChangeToBossScene();
        }
    }
}
