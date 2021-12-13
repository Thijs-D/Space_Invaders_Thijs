using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Medium : NPC
{
    // creates a new NPC of type MEDIUM,
    // the specific values can be read in the NPC class
    protected NPC_Medium() : base(6, 2, 1, 5, 0.1f, 200, AlienTypes.MEDIUM)
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
