using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProbabilityList", menuName = "Enemy/Probability/EnemyProbabilityList")]
public class EnemyProbabilityList : ScriptableObject
{
    public float totalProbability;//totalProbabilityӦ�ý���0-1֮��
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
            Debug.LogError("�õ��˲�����");
            return;
        }
        float lastRemainProbability = totalProbability - targetEnemy.probability;
        if(lastRemainProbability == 0f)
        {
            Debug.Log("���˸����Ѿ��ﵽ���");
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
    public float probability;//Ȩ��
    public GameObject enemyPrefab;
}
