using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieDieColor : MonoBehaviour
{
    
    
    public SkinnedMeshRenderer skinnedMeshRenderer;
  

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    
    public void Materia()
    {

        //skinnedMeshRenderer.material.color = Color.red;

        skinnedMeshRenderer.materials[0].color = Color.red;
        skinnedMeshRenderer.materials[1].color = Color.red;
    }
}
