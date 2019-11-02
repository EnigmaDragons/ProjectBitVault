using System;
using UnityEngine;

public class HeroDirectionController : MonoBehaviour
{
    private bool _readyForNewInput = true;

    void Update()
    {
        var hDir = Input.GetAxis("Horizontal");
        var vDir = Input.GetAxis("Vertical");

        if (Math.Abs(hDir) > 0.01)
        {
            if (!_readyForNewInput) return;
            
            transform.position += new Vector3(1 * Math.Sign(hDir), 0, 0);
            _readyForNewInput = false;
        }
        else if (Math.Abs(vDir) > 0.01)
        {
            if (!_readyForNewInput) return;
            
            transform.position += new Vector3(0, 1 * Math.Sign(vDir), 0);
            _readyForNewInput = false;
        }
        else
        {
            _readyForNewInput = true;
        }
    }
}
