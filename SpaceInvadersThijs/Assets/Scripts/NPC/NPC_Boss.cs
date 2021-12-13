using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Boss : NPC
{
    /// creates a new NPC of type BOSS,
    // the specific values can be read in the NPC class
    protected NPC_Boss() : base(60, 2, 0.5f, 5, 0.1f, 500, AlienTypes.BOSS)
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
