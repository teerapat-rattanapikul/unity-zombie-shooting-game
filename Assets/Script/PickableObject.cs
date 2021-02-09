using UnityEngine;
using UnityEngine.UI;

public class PickableObject : MonoBehaviour
{
    // หายไปเมื่อผ่านไปกี่วิ...
    public float lifeTime = 30f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public virtual void Picked(PlayerController player)
    {
        // ตอนที่ มี player เก็บไปทำอะไรสักอย่างกับ player นั้น
        Destroy(gameObject);
    }
}
