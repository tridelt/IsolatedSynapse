using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAudio : MonoBehaviour
{

    public void AttackSound()
    {
        GetComponents<AudioSource>()[0].Play();
    }
    public void ShieldSound()
    {
        GetComponents<AudioSource>()[1].Play();
    }
    public void DodgeSound()
    {
        GetComponents<AudioSource>()[2].Play();
    }
    public void GadgetSound()
    {
        GetComponents<AudioSource>()[3].Play();
    }


}
