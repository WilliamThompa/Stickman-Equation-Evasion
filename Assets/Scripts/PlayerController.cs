using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public SpriteRenderer[] spriteRenderer;
    private List<SpriteRenderer> spriteRenderList;
    public GameObject Player;

    private void Start()
    {
        GetSprites();
    }

    private void GetSprites()
    {

    }
}
