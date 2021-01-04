﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class Character : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro display;
    
    public enum Type {
        GUARDIAN,
        ENEMY
    }

    public enum Status {
        IDLE,
        CHASE,
        ATTACK,
        ATTACKED,
        DIE,
    }

    public Type CharacterType { get; set; }

    //private Type characterType;
    private Status status = Status.IDLE;
    private Character targetCharacter;

    private float speed = 0.2f;

    public void SetInfo() {
        
    }

    private void Start() {
        animator.Play("Idle");
    }

    private void Update() {
        display.text = status.ToString()[0].ToString();
        
        switch (status) {
            case Status.IDLE:
                targetCharacter = GameManager.Instance.GetCloseCharacter(this);
                if (targetCharacter != null) {
                    status = Status.CHASE;
                    animator.SetTrigger("Chase");
                }
                break;
            case Status.CHASE:
                var vec = targetCharacter.transform.position - transform.position;
                transform.Translate(vec * (Time.deltaTime * speed), Space.Self);
                var distance = targetCharacter.transform.position - transform.position;
                if (Mathf.Abs(distance.magnitude) <= 2f) {
                    status = Status.ATTACK;
                    Attack();
                    animator.SetTrigger("Attack");

                }
                break;
            case Status.ATTACK:
                
                break;
        }
    }

    private void Attack() {
        
    }
}

