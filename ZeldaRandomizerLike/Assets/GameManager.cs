using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
		DOTween.Init(false, false, LogBehaviour.ErrorsOnly);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
