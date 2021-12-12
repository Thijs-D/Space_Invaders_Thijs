using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Medium : NPC
{
    // create new NPC
    protected NPC_Medium() : base(6, 2, 1, 5, 0.1f, 200, AlienTypes.MEDIUM)
    {

    }

    new

        // Start is called before the first frame update
        void Start()
    {
        base.Start();
    }

    new

        // Update is called once per frame
        void Update()
    {
        base.Update();
    }
}
