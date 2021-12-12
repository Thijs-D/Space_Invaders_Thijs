using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Boss : NPC
{
    // create new NPC
    protected NPC_Boss() : base(50, 0.5f, 5, 0.1f, 500, true)
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
