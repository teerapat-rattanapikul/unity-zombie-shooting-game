using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoUi : MonoBehaviour
{   
    public Text ammo;
    public Text maxAmmo ;
    private float maxcurrentAmmo ;
    private float currentAmmo ;
    // Start is called before the first frame update
    void Start()
    {   
        maxcurrentAmmo = 200;
        currentAmmo = 20;
        maxAmmo.text = maxcurrentAmmo.ToString();
        ammo.text = currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAmmo(int currentAmmo, int maxcurrentAmmo){
        maxAmmo.text = maxcurrentAmmo.ToString();
        ammo.text = currentAmmo.ToString();
    }

}
