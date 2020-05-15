using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Material materialPortal;
    public GameObject chieldPortal;
    public float portalOutLine;
    public string namePortal;
    [ColorUsage(true, true)]
    public Color colorOutLineShield;
    public bool teste;
    private void Awake()
    {
        //shader cache
        materialPortal = GetComponent<SpriteRenderer>().material;
        
        // chields (filhos)
        chieldPortal = GameObject.Find(gameObject.name+"/PortalActive");
    }

    void Update()
    {
        if (teste)
        {
            ActivePortal();
        }
        else
            InactivatePortal();
    }

    void ActivePortal()
    {
        materialPortal.SetFloat("_OutLineStrengh", 4);
        materialPortal.SetColor("_Color", colorOutLineShield);
        chieldPortal.SetActive(true);
        
    }
    void InactivatePortal()
    {
        materialPortal.SetFloat("_OutLineStrengh", 0);
        materialPortal.SetColor("_Color", new Color(0, 0, 0, 0));
        chieldPortal.SetActive(false);
    }
}
