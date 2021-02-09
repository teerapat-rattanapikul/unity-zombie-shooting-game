using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventPlayerFall : MonoBehaviour
{
    public Transform backPos;

    public void movePlayer(Transform player) {
        player.gameObject.SetActive(false);
        player.position = backPos.position;
        player.gameObject.SetActive(true);
    }
}
