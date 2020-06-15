using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControllerScript : GenericSingletonClass<ScoreControllerScript>
{
    public int p1_score { get; private set; }
    public int p2_score { get; private set; }
    public int p3_score { get; private set; }
    public int p4_score { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        p1_score = 0;
        p2_score = 0;
        p3_score = 0;
        p4_score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

/*    public void Awake()
    {
        base.Awake();
    }*/

    public void incrementScore(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                p1_score++;
                break;
            case 2:
                p2_score++;
                break;
            case 3:
                p3_score++;
                break;
            case 4:
                p4_score++;
                break;
        }
    }

}
