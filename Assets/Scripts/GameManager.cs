using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float positionZ;
    public float speed;

    public GameObject pointPrefab;
    public Transform player;
    public List<Vector3> points = new List<Vector3>();
    public LineRenderer line;

    int coins = 0;
    int currentIndex = 0;
    bool isPlaying = true;

    MapGenerator mapGenerator;
    [HideInInspector] public UI UI;

    void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
        UI = FindObjectOfType<UI>();

        points.Add(player.position);   
    }

    void Touch()
    {
        if (isPlaying && Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                var position = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                position.z = positionZ;
                GameObject temp = Instantiate(pointPrefab, position, Quaternion.identity, this.transform);
                points.Add(temp.transform.position);
                DrawLines();
            }
        }
    }

    void DrawLines()
    {
        int pointsCount = points.Count;
        line.positionCount = pointsCount;
        for (int i = 0; i < pointsCount; i++)
            line.SetPosition(i, points[i]);
    }

    void Move()
    {
        if (isPlaying && points.Count > 0)
        {
            if (player.position == points[currentIndex])
            { 
                if (currentIndex + 1 < points.Count)
                    currentIndex++;
            }
            else
                player.position = Vector3.MoveTowards(player.position, points[currentIndex], speed * Time.deltaTime);
        }
    }

    public void AddCoin(GameObject coin)
    {
        mapGenerator.coins.Remove(coin);
        Destroy(coin);
        coins++;
        UI.coinsText.text = coins.ToString();
        if (mapGenerator.coins.Count == 0)
            Event(UI.UIType.Win);
    }

    public void Event(UI.UIType type)
    { 
        isPlaying = false;
        UI.UIEvent(type);
    }

    void Update()
    {
        Touch();
        Move();
    }
}
