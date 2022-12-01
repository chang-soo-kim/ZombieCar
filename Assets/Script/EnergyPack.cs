using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPack : MonoBehaviour, IItem
{
    public void Use(GameObject taget)
    {
        PlayerMove playermove = taget.GetComponent<PlayerMove>();
        //에너지를 5 추가해준다.
        playermove.energyPlus += 5f;
        Destroy(gameObject);
    }
}
