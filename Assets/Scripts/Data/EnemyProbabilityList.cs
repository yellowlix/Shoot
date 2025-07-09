using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProbabilityList", menuName = "Enemy/Probability/EnemyProbabilityList")]
public class EnemyProbabilityList : ScriptableObject
{
    public float totalProbability;//totalProbability应该介于0-1之间
    public List<EnemyProbability> AllEnemyProbablilityList = new List<EnemyProbability>();
    EnemyProbabilityList()
    {
        GetTotalProbability();
    }
    public void GetTotalProbability()
    {
        totalProbability = 0;
        foreach (EnemyProbability item in AllEnemyProbablilityList)
        {
            totalProbability += item.probability;
        }
    }
    public GameObject GetRandomEnemy()
    {
        float choice = Random.Range(0, totalProbability);
        float cumulative = 0f;
        foreach(EnemyProbability Item in AllEnemyProbablilityList)
        {
            cumulative += Item.probability;
            if(choice <= cumulative)
            {
                return Item.enemyPrefab;
            }
        }
        return AllEnemyProbablilityList[0].enemyPrefab;
    }
    public void AdjustEnmeyProbility(string name, float increaseProbability)
    {
        EnemyProbability targetEnemy = AllEnemyProbablilityList.Find(x => x.name == name);
        if (targetEnemy == null)
        {
            Debug.LogError("该敌人不存在");
            return;
        }
        float lastRemainProbability = totalProbability - targetEnemy.probability;
        if(lastRemainProbability == 0f)
        {
            Debug.Log("敌人概率已经达到最大");
        }
        targetEnemy.probability = Mathf.Clamp(targetEnemy.probability + increaseProbability, 0,totalProbability);
        float remainProbability = Mathf.Clamp(totalProbability - targetEnemy.probability,0,totalProbability);
        foreach(EnemyProbability item in AllEnemyProbablilityList)
        {
            item.probability = (remainProbability/lastRemainProbability) * item.probability;
        }
    }
}
[System.Serializable]
public class EnemyProbability
{
    public int id;
    public string name;
    public float probability;//权重
    public GameObject enemyPrefab;
}
