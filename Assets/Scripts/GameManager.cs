using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    int playerHp = 10;
    int playerGold = 0;

    Image hpImage;
    Text gold;
    Text message;
    GameObject player;

    int latestMapX = 18;
    public GameObject coin;
    public GameObject chest;
    public GameObject wall;
    public GameObject killOrb;
    public GameObject chestOrb;
    public GameObject boss;
    float latestSpawnX = 0;
    int hpHealFailCnt = 0;

    bool isHasBoss = false;
    // Start is called before the first frame update
    void Start()
    {
        hpImage = GameObject.Find("HPIMAGE").GetComponent<Image>();
        gold = GameObject.Find("GOLD").GetComponent<Text>();
        message = GameObject.Find("MESSAGE").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("autoDamage");
        StartCoroutine("slowMapGen");
        isHasBoss = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHp > 0)
        {
            hpImage.fillAmount = playerHp / 10f;
            gold.text = "GOLD : " + playerGold.ToString();
        }
        else
        {
            hpImage.fillAmount = 0f;
            message.text = "게임 오버";
            Destroy(player);
            StartCoroutine("gameover");
        }
        if (playerHp > 0)
        {
            Vector3 spawnLoc = player.transform.position;
            spawnLoc.y = Random.Range(0.03f, -3.8f);
            spawnLoc.x = spawnLoc.x + 20;
            if (spawnLoc.x > latestSpawnX)
            {
                hpHealFailCnt++;
                GameObject spawnObj = null;
                switch (Random.Range(0, 11))
                {
                    case 0:
                        hpHealFailCnt--;
                        spawnObj = chest;
                        break;
                    case 1:
                        spawnObj = wall;
                        break;
                    case 2:
                        spawnObj = coin;
                        break;
                    case 3:
                        spawnObj = coin;
                        break;
                    case 4:
                        if (playerGold >= 1000)
                        {
                            if (Random.Range(0, 2) == 0)
                            {
                                spawnObj = killOrb;
                            }
                            if (Random.Range(0, 2) == 0)
                                Instantiate(boss, Vector3.zero, Quaternion.identity);
                        }
                        else if (playerGold >= 400)
                        {
                            if (Random.Range(0, 3) == 0)
                            {
                                spawnObj = killOrb;
                            }
                            if (Random.Range(0, 5) == 0)
                                Instantiate(boss, Vector3.zero, Quaternion.identity);
                        }
                        else if (playerGold >= 100)
                        {
                            if (Random.Range(0, 5) == 0)
                            {
                                spawnObj = killOrb;
                            }
                            if (Random.Range(0, 5) == 0)
                                Instantiate(boss, Vector3.zero, Quaternion.identity);
                        }
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        spawnObj = coin;
                        break;
                    case 10:
                        spawnObj = chestOrb;
                        break;
                }
                if (hpHealFailCnt > 4)
                {
                    spawnObj = chest;
                    hpHealFailCnt = 0;
                }
                latestSpawnX = spawnLoc.x;
                latestSpawnX += Random.Range(0.1f, 5f);
                if (spawnObj != null)
                {
                    Instantiate(spawnObj, spawnLoc, Quaternion.identity);
                }
            }
            if (playerGold >= 50)
            {
                player.GetComponent<PlayerMove>().maxspeed = 11f;
            }
        }
    }

    IEnumerator slowMapGen()
    {
        yield return new WaitForSeconds(0.05f);
        //create inf world
        Tilemap ground = GameObject.Find("Ground").GetComponent<Tilemap>();
        Tilemap wall = GameObject.Find("Wall").GetComponent<Tilemap>();

        for (int i = 2; i >= -5; i--)
        {
            if (i == 2 || i == 1 || i == -5)
            {
                TileBase ti = wall.GetTile(new Vector3Int(18, i, 0));
                wall.SetTile(new Vector3Int(latestMapX, i, 0), ti);
            }
            else
            {
                TileBase ti = ground.GetTile(new Vector3Int(18, i, 0));
                ground.SetTile(new Vector3Int(latestMapX, i, 0), ti);
            }

        }
        latestMapX++;
        StartCoroutine("slowMapGen");
    }

    IEnumerator autoDamage()
    {
        yield return new WaitForSeconds(1.0f);
        playerHp--;
        StartCoroutine("autoDamage");
    }

    IEnumerator gameover()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Title");
    }

    public void getCoin(int num)
    {
        num = Random.Range(1, num);
        playerGold += num;
        message.text = num.ToString() + "GOLD를 획득했다.";
    }

    public void healHp(int num)
    {
        num = Random.Range(1, num);
        playerHp += num;
        message.text = "회복약을 발견했다\n" + num.ToString() + "만큼 회복했다.";
        if (playerHp > 10)
        {
            playerHp = 10;
        }
    }

    public void hitOrb()
    {
        playerHp -= 20;
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 20, ForceMode2D.Impulse);
    }

    public void getOrb(int amount)
    {
        player.GetComponent<PlayerMove>().orbCount += amount;
    }

    public void hitTree()
    {
        playerHp -= 1;
        player.GetComponent<PlayerMove>().speed *= 0.9f;
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
    }
}
