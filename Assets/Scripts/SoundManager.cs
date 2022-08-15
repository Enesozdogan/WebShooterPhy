using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
  public AudioSource eggSound;
  public AudioSource playerDeathSound;
  
    public void OnClick(){
        eggSound.Play();
    }
    public void DieSound(){
        playerDeathSound.Play();
    }
}
