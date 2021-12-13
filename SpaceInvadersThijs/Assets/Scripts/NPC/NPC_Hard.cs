using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Hard : NPC
{
    // creates a new NPC of type HARD,
    // the specific values can be read in the NPC class
    protected NPC_Hard() : base(12, 1, 0.5f, 5, 0.2f, 300, AlienTypes.HARD)
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
