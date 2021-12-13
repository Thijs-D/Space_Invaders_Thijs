using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Normal : NPC
{
    // creates a new NPC of type NORMAL,
    // the specific values can be read in the NPC class
    protected NPC_Normal() : base(3, 1, 2, 5, 0.3f, 100, AlienTypes.NORMAL)
    {

    }

    new

        // Start is called before the first frame update
        void Start()
    {
        // base calls the parent class
        base.Start();
    }

    new

        // Update is called once per frame
        void Update()
    {
        // base calls the parent class
        base.Update();
    }
}
