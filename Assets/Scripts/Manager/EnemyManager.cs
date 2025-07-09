using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get { return instance; }
    }
    public EnemyProbabilityList enemyProbabilityList;
    public List<EnemyProbability> allEnemyProbility;
    public GameObject warnCrossPrefab;
    public int layerIndex;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        int layerMask = LayerMask.GetMask("Player","Default");//指定检测层级
        allEnemyProbility = enemyProbabilityList.AllEnemyProbablilityList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawWarnCross(Vector3 generatePoint)
    {
        GameObject warnCross= ObjectPool.Instance.GetObject(warnCrossPrefab);
        warnCross.transform.position = generatePoint;
    }
    public GameObject GenenrateEnemy(Vector3 generatePoint)
    {
        GameObject enemy;
        bool canGenenrate = true;
        Collider2D[] cols = Physics2D.OverlapPointAll(generatePoint,layerIndex);
        foreach (Collider2D col in cols)
        {
            if (col.isTrigger == false)
            {
                canGenenrate = false;
                break;
            }
        }
        if (canGenenrate)
        {
            enemy = ObjectPool.Instance.GetObject(enemyProbabilityList.GetRandomEnemy());
        }
        else
        {
           enemy = GenenrateEnemy(GeneratePoint());
        }
        enemy.transform.position = generatePoint;
        return enemy;
    }

    public Vector3 GeneratePoint()
    {
        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector2.one);
        float x = Random.Range(leftBottom.x, rightTop.x);
        float y = Random.Range(leftBottom.y, rightTop.y);
        Vector3 generatePoint = new Vector3(x, y, 0);
        return generatePoint;
    }

}
