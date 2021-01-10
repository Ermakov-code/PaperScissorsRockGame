using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class Player : MonoBehaviour
{
   
   
   
   [System.Serializable]
   public class SpriteGameObj
   {
      public string tag;
      public Sprite sprite;
   }
   
   [SerializeField] private float health;
   [SerializeField] private float speed = 5f;
   [SerializeField] private List<GameObject> allObj;
   [SerializeField] private List<SpriteGameObj> currentSprite;
   [SerializeField] private GameObject[] hearts;
   [SerializeField] private SpriteRenderer spriteForCurrentGameObject;
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private GameManager gameManager;
   private GameObject _currentGameObject;
   private float _score = 0;

   public bool IsAlive = true;
   
   
  

   private Rigidbody _rb;

  

   private void Start()
   {
      
      Random Ram = new Random();
      _rb = GetComponent<Rigidbody>();
      _currentGameObject = allObj[Ram.Next(allObj.Count)];
      foreach (SpriteGameObj spriteGameObj in currentSprite)
      {
         if (_currentGameObject.tag.Equals(spriteGameObj.tag))
         {
            spriteForCurrentGameObject.sprite = spriteGameObj.sprite;
         }
      }
      _rb.velocity = new Vector3(0, 0, speed);
      
   }

   public void ApplyDamage(float damage)
   {
     
         health -= damage;
         if (health <= 0)
         {
            IsAlive = false;
            _rb.velocity = Vector3.zero;
            gameManager.Instantiate.GameOver();
         }
         if (IsAlive)
         {
            for (float i = health + damage - 1; i >= health; --i)
            {
               hearts[(int) i].SetActive(false);
            }
         }
    
       
       
   }

   private void ScoreUp()
   {
      scoreText.text = "Score: " + ++_score;
   }

   public void OnTriggerEnter(Collider other)
   {
      ScoreUp();
      if (_currentGameObject.tag.Equals("Scissors"))
      {
         if (_currentGameObject.tag.Equals(other.tag))
         {
            ApplyDamage(1f);
         }
         else if (other.tag.Equals("Rock"))
         {
            ApplyDamage(2f);
         }
         else
         {
            
         }
      }
      else if (_currentGameObject.tag.Equals("Rock"))
      {
         if (_currentGameObject.tag.Equals(other.tag))
         {
            ApplyDamage(1f);
         }
         else if (other.tag.Equals("Paper"))
         {
            ApplyDamage(2f);
         }
         else
         {
            
         }
      }
      else if (_currentGameObject.tag.Equals("Paper"))
      {
         if (_currentGameObject.tag.Equals(other.tag))
         {
            ApplyDamage(1f);
         }
         else if (other.tag.Equals("Scissors"))
         {
            ApplyDamage(2f);
         }
         else
         {
            
         }
      }
      Debug.Log(health);
      _currentGameObject = other.gameObject;
      foreach (SpriteGameObj spriteGameObj in currentSprite)
      {
         if (_currentGameObject.tag.Equals(spriteGameObj.tag))
         {
            spriteForCurrentGameObject.sprite = spriteGameObj.sprite;
         }
      }
      
      if (health < 0.5f)
      {
         //IsAlive = false;
      }
      other.gameObject.SetActive(false);
   }
}
