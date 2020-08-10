using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PodiumGenerator : MonoBehaviour
{
    public int numberOfPlayers = 4;
    private float[] XValues = {-2f, -0.5f, 1f, 2.5f};   // same for player positions, Y-val is at 6
    private float[] YValues = {-2.52f, -1.53f, -0.54f, 0.45f};
    // int[] positions = {3, 1, 2, 4};
    public GameObject[] controls;
    [SerializeField]
    private Transform[] characters;
    private int[] positions;
    public GameObject cratePrefab;
    public GameObject platforms;    // under Grid/Jumpable
    [SerializeField]
    private AfterMatchManager afterMatchManager;
    private ScreensTransitionManager screensTransitionManager;
    private GameManager gameManager;
    private bool gotPositions;
    private AudioSource thump;


    void OnEnable()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.EnterPodium();

        numberOfPlayers = screensTransitionManager.requiredPlayersToStart;
        positions = new int[numberOfPlayers];
        characters = new Transform[numberOfPlayers];
        
        platforms.SetActive(false);
        
        // Get the character's Transform for each player
        for(int i = 0; i < controls.Length; i++)
        {
            GameObject control = controls[i];
             for (int c = 0; c < control.transform.childCount; c++)
            {
                if (control.transform.GetChild(c).gameObject.activeSelf == true)
                {
                    characters[i] = control.transform.GetChild(c);

                    characters[i].gameObject.GetComponent<Animator>().SetBool("Hurt", false);
                    characters[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);

                    characters[i].gameObject.SetActive(false);
                    // Debug.Log(characters[i]);
                    break;
                }
            }
        }

        thump = GetComponent<AudioSource>();

        // ======================================
        // get player positions
        bool gotPositions = false;
        while (!gotPositions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = afterMatchManager.getPlayerRankPodium(i)[0] - '0';
                // Debug.Log("Player" + i + "'s position is " + positions[i].ToString());
            }
            gotPositions = true;
        }
        Debug.Log("Positions received");

        // generate podium
        StartCoroutine(SpawnPodium());
    }

    // ===== Generate crates and players to create podium =====
    private IEnumerator SpawnPodium()
    {
        // Spawn Crates
        for (int y = 0; y < numberOfPlayers; y++)    // iterate across row, then column
        {
            for (int x = 0; x < numberOfPlayers; x++)
            {
                if (y >= (YValues.Length-positions[x]+1))   // skip crate spawn if already at correct podium size
                {
                    continue;
                }
                GameObject crate = (GameObject) Instantiate(cratePrefab, new Vector3(XValues[x], 6f, 0f), Quaternion.identity, transform);
                StartCoroutine(CrateLanding(x, y, crate));
            }

            // quadratic delay between each row of crates (for dramatic effect)
            // only wait for 1 second after last row is spawned 
            if (y < YValues.Length-1) yield return new WaitForSeconds(0.1f * (y+1) * (y+1) + 0.9f);
            else yield return new WaitForSeconds(1);
        }

        // Spawn Players
        for (int x = 0; x < characters.Length; x++)
        {
            characters[x].position = new Vector3(XValues[x], 5f, 0);
            characters[x].gameObject.SetActive(true);
            // lines below to handle characters who were SetActive(false) in the midst of death animation
            characters[x].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            characters[x].gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Color tmp = characters[x].gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            characters[x].gameObject.GetComponent<SpriteRenderer>().color = tmp;
            // characters[x].gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            characters[x].gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        // TODO : podium to title

    }

    // ===== ensure crate lands in the right position ======
    private IEnumerator CrateLanding(int x, int y, GameObject crate)
    {
        yield return new WaitUntil(() => crate.transform.position.y <= YValues[y]);
        thump.Play();
        crate.GetComponent<Rigidbody2D>().isKinematic = true;
        crate.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        crate.transform.position = new Vector3(XValues[x], YValues[y], 0);
    }



}
