using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : SoundManager
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
