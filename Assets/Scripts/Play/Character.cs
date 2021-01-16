using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro display;
    [SerializeField] private Type _characterType;
    [SerializeField] private SpriteRenderer hpBar;
    
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

    private const float TargetDetectInterval = 1f;

    public Type CharacterType { get; set; }
    
    private Status status = Status.IDLE;
    private Character targetCharacter;

    private CharacterInfo characterInfo = new CharacterInfo();

    private float attackGuage = 0f;
    private float targetDetectTime = 0f;

    private static readonly Dictionary<Status, int> animationHash = new Dictionary<Status, int>() {
        {Status.IDLE, Animator.StringToHash("Idle")},
        {Status.CHASE, Animator.StringToHash("Chase")},
        {Status.ATTACK, Animator.StringToHash("Attack")},
        {Status.ATTACKED, Animator.StringToHash("Attacked")},
        {Status.DIE, Animator.StringToHash("Die")},
    };

    private static readonly int Progress = Shader.PropertyToID("Progress");
    //private static readonly int Attack1 = Animator.StringToHash("Attack");

    public void SetInfo() {
        CharacterType = _characterType;
    }

    public void Init() {
        animator.Play("Idle");
        attackGuage = characterInfo.attackInterval;
        targetDetectTime = TargetDetectInterval;
    }

    private void Update() {
        display.text = status.ToString()[0].ToString();
        attackGuage -= Time.deltaTime;

        // idle or chase일때 타겟 캐릭터를 재탐색한다.
        // TODO: 공격이 끝나거나 하면 바로 탐색하도록?
        if (status == Status.IDLE || status == Status.CHASE) {
            targetDetectTime -= Time.deltaTime;
        }
        
        if (targetDetectTime <= 0f) {
            targetDetectTime = TargetDetectInterval;
            targetCharacter = GameManager.Instance.GetCloseCharacter(this);
        }

        switch (status) {
            case Status.IDLE:
                if (targetCharacter != null) {
                    Chase();
                }
                break;
            case Status.CHASE:
                // chase 중에 적이 없어진 경우(죽은경우)
                if (targetCharacter == null) {
                    Debug.LogError("chase -> idle");
                    Idle();
                    break;
                }
                
                var vec = targetCharacter.transform.position - transform.position;
                
                if (Mathf.Abs(vec.magnitude) > characterInfo.attackDetectRange) {
                    transform.Translate(vec * (Time.deltaTime * characterInfo.speed), Space.Self);
                } else if (attackGuage <= 0f) {
                    Attack();
                }
                break;
            case Status.ATTACK:
                // damage 체크
                break;
        }
    }

    private void Chase() {
        ChangeStatus(Status.CHASE);
    }

    private void Idle() {
        ChangeStatus(Status.IDLE);
    }
    
    private void Attack() {
        ChangeStatus(Status.ATTACK);
        attackGuage = characterInfo.attackInterval;
    }

    private void Damage() {
        Debug.LogError("DAMAGE");
        GameManager.Instance.Attack(targetCharacter, characterInfo.attackPower);
    }

    public void AttackedFromOther(float dmg) {
        ChangeStatus(Status.ATTACKED);
        characterInfo.hp -= dmg;
        hpBar.material.SetFloat(Progress, characterInfo.hp / 10f);
        if (characterInfo.hp <= 0) {
            ChangeStatus(Status.DIE);
        }
    }

    private void ChangeStatus(Status s) {
        status = s;
        animator.SetTrigger(animationHash[status]);
    }

    private void HI() {
        Debug.LogError("HI");
    }
    
}

