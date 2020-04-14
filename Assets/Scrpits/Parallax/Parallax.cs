using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Alvo;
    public float velocidadeRelativa;
    public float posicaoAntX;
    // Start is called before the first frame update
    void Start()
    {
        if (velocidadeRelativa < 1)
            velocidadeRelativa = 1;
        Alvo =  GameObject.FindGameObjectWithTag("Player").transform;
        posicaoAntX = Alvo.position.x;

    }

    void EfeitoParallax()
    {
        transform.Translate((Alvo.position.x - posicaoAntX) / velocidadeRelativa, 0, 0);
        posicaoAntX = Alvo.position.x;
    }


    // Update is called once per frame
    void Update()
    {
        EfeitoParallax();
    }
}
