using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardRaycastCheck : MonoBehaviour {
    

    [SerializeField]
    float _warningDistance = 10;

    [SerializeField]
    float _headSpaceMinDistance = 10;

    public bool GiveWarning;
    public bool ProhibitHead;
    

    private void Update()
    {
        DoRayCast();

      //  Debug.Log(GiveWarning + " " + ProhibitHead);
    }

    void DoRayCast()
    {
       GiveWarning = Physics.Raycast(this.transform.position, this.transform.forward, _warningDistance);
        Debug.DrawRay(this.transform.position, this.transform.forward * _warningDistance, Color.blue,1);

        ProhibitHead = Physics.Raycast(this.transform.position, this.transform.forward, _headSpaceMinDistance);
        Debug.DrawRay(this.transform.position + Vector3.up, this.transform.forward * _warningDistance, Color.yellow,1);
    }

    
}
