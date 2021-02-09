using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialBossScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.playerInstance.SetActive(false);
        GameManager.Instance.playerInstance.transform.position = transform.position;
        GameManager.Instance.playerInstance.transform.rotation = transform.rotation;
        GameManager.Instance.playerInstance.GetComponent<PlayerController>().canMove = true;
        GameManager.Instance.playerInstance.SetActive(true);
    }

}
