using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ClickerManager : MonoBehaviour
{

    [SerializeField] private Button clickButton;
    [SerializeField] private Image bar;
    [SerializeField] private Text healthText;
    [SerializeField] private int health;
    [SerializeField] private Enemy Enemys;
    [SerializeField] private GameObject pointSpawn;

    private GameObject currentEnemySpawn;
    
    private void Start()
    {
        clickButton.onClick.AddListener(Click);
        health = Enemys.maxHealthEnemy;
        currentEnemySpawn = Instantiate(Enemys.enemyObj[Random.Range(0, Enemys.enemyObj.Length)], pointSpawn.transform);
        UpdateInfo();
    }

    private void Click()
    {
        health--;
        pointSpawn.transform.DOScale(new Vector3(90, 90, 90),0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            pointSpawn.transform.DOScale(new Vector3(100, 100, 100),0.1f).SetEase(Ease.Linear);  
        });
        
        if (health <= 0)
        {
            StartCoroutine((TimerNextEnemy()));
        }

        UpdateInfo();

    }

    private void UpdateInfo()
    {
        bar.fillAmount = (float)health / (float)Enemys.maxHealthEnemy;
        healthText.text = health + "/" + Enemys.maxHealthEnemy;
    }

    private IEnumerator TimerNextEnemy()
    {
        clickButton.interactable = false;
        currentEnemySpawn.GetComponent<Animator>().SetInteger("State", 9);
        yield return new WaitForSeconds(1);
        
        health = Enemys.maxHealthEnemy;
        Destroy(currentEnemySpawn.gameObject);
        currentEnemySpawn = Instantiate(Enemys.enemyObj[Random.Range(0, Enemys.enemyObj.Length)], pointSpawn.transform);
        UpdateInfo();
        clickButton.interactable = true;
    }
    
    
}
[Serializable]
public class Enemy
{
    public GameObject[] enemyObj;
    public int maxHealthEnemy;
    public int cost;
}