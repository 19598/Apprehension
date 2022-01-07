using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeeking : MonoBehaviour
{
    [SerializeField] MouseLook mouselook;
    public float peekInput;

    public void receivePeekInput(float _peekInput)
    {
        peekInput = _peekInput;
    }
    // Update is called once per frame
    void Update()
    {
        HandlePeeking();
    }
    public void HandlePeeking()
    {
        if (peekInput == 1)
        {
            mouselook.setPosX(-0.15f);
            mouselook.setPosY(0.1f);
        }
        else if (peekInput == -1)
        {

            mouselook.setPosX(0.15f);
            mouselook.setPosY(0.1f);
        }
        else
        {

            mouselook.setPosX(0f);
            mouselook.setPosY(0f);
        }
    }

}
