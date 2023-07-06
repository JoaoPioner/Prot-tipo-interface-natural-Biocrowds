using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalController : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro meuTexto;
    private Transform targetCamera;
    void Start()
    {
        meuTexto.text = gameObject.name;
        targetCamera = GameObject.Find("SideCamera").transform;
    }
    private void LateUpdate()
    {
        if (targetCamera != null)
        {
            meuTexto.transform.LookAt(targetCamera);
        }
    }

}
