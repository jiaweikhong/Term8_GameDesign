using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// crate sprite : http://clipart-library.com/clipart/22069.htm
// sound : https://freesound.org/people/DigestContent/sounds/458877/

public class PodiumGenerator : MonoBehaviour
{
    public GameObject cratePrefab;
    float[] XValues = {-2f, -0.5f, 1f, 2.5f};   // same for player positions, Y-val is at 6
    float[] YValues = {-2.52f, -1.53f, -0.54f, 0.45f};
    // int[] positions = {3, 1, 2, 4};
    int[] positions;

    public GameObject[] controls;
    public int numberOfPlayers = 4;
    [SerializeField]
    private Transform[] characters;
    public GameObject platforms;    // under Grid/Jumpable
    [SerializeField]
    private AfterMatchManager afterMatchManager;
    private ScreensTransitionManager screensTransitionManager;
    private GameManager gameManager;
    private bool gotPositions;
    AudioSource thump;

    // public bool start = false;

    void OnEnable()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.EnterPodium();

        numberOfPlayers = screensTransitionManager.requiredPlayersToStart;
        positions = new int[numberOfPlayers];
        characters = new Transform[numberOfPlayers];
        gotPositions = false;
        
        platforms.SetActive(false);
        
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
                    Debug.Log(characters[i]);
                    break;
                }
            }
        }
        thump = GetComponent<AudioSource>();
        StartCoroutine(SpawnPodium());
    }

    private IEnumerator SpawnPodium()
    {
        GetPlayerPositions();
        yield return new WaitUntil(() => gotPositions);
        Debug.Log("Positions received");

        // yield return new WaitForSeconds(0);
        StartCoroutine(SpawnCrate());   // SpawnCrate will call SpawnPlayers after (sorry for messy coding)
        yield return new WaitForSeconds(1);
    }

    private void GetPlayerPositions()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = afterMatchManager.getPlayerRankPodium(i)[0] - '0';
            // Debug.Log("Player" + i + "'s position is " + positions[i].ToString());
        }
        gotPositions = true;
    }


    private IEnumerator SpawnCrate()
    {
        for (int y = 0; y < numberOfPlayers ; y++)
        {
            for (int x = 0; x < numberOfPlayers; x++)
            {
                if (y >= (YValues.Length-positions[x]+1))
                {
                    continue;
                }
                GameObject crate = (GameObject) Instantiate(cratePrefab, new Vector3(XValues[x], 6f, 0f), Quaternion.identity, transform);
                StartCoroutine(CrateLanding(x, y, crate));
            }
            if (y < 3) yield return new WaitForSeconds(0.1f * (y+1) * (y+1) + 0.9f);
            else yield return new WaitForSeconds(1);
        }
        // Debug.Log("Spawning players...");
        SpawnPlayers();
/*        yield return new WaitForSeconds(10f);
        screensTransitionManager.ToTitle();*/
    }

    private IEnumerator CrateLanding(int x, int y, GameObject crate)
    {
        yield return new WaitUntil(() => crate.transform.position.y <= YValues[y]);
        thump.Play();
        crate.GetComponent<Rigidbody2D>().isKinematic = true;
        crate.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        crate.transform.position = new Vector3(XValues[x], YValues[y], 0);
    }

    private void SpawnPlayers()
    {
        for (int x = 0; x < characters.Length; x++)
        {
            characters[x].position = new Vector3(XValues[x], 5f, 0);
            characters[x].gameObject.SetActive(true);
            characters[x].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            characters[x].gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Color tmp = characters[x].gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            characters[x].gameObject.GetComponent<SpriteRenderer>().color = tmp;
            // characters[x].gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            characters[x].gameObject.GetComponent<BoxCollider2D>().enabled = true;

        }

    }

}
