using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterBase : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro display;
    //[SerializeField] private Type _characterType;
    [SerializeField] private SpriteRenderer hpBar;

    [SerializeField] private EventDispatcher eventDispatcher;
    
    public enum Type {
        GUARDIAN,
        ENEMY
    }

    public enum Status {
        IDLE,
        CHASE,
        ATTACK,
        DIE,
        DISAPPEAR,
        SIT,
    }

    private const float TargetDetectInterval = 1f;

    public Type CharacterType { get; private set; }
    private Character character;

    public Status CurStatus => status;
    private Status status = Status.IDLE;
    private CharacterBase targetCharacter;

    public Stat CharacterStat => stat;
    private Stat stat = new Stat();

    public bool IsDead => isDead;
    private bool isDead = false;

    private float attackGuage = 0f;
    private float targetDetectTime = 0f;

    private static readonly Dictionary<Status, int> animationHash = new Dictionary<Status, int>() {
        {Status.IDLE, Animator.StringToHash("Idle")},
        {Status.CHASE, Animator.StringToHash("Chase")},
        {Status.ATTACK, Animator.StringToHash("Attack")},
        {Status.DIE, Animator.StringToHash("Die")},
        {Status.DISAPPEAR, Animator.StringToHash("Disappear")},
        {Status.SIT, Animator.StringToHash("Sit")},
    };

    private static readonly int Progress = Shader.PropertyToID("_Progress");
    //private static readonly int Attack1 = Animator.StringToHash("Attack");

    public void SetInfo(Character character, Type type) {
        this.character = character;
        CharacterType = type;
        // TODO: stat setting
        stat.hp = character.curHp;
    }
    
    // public void SetInfo() {
    //     CharacterType = _characterType;
    // }

    private void Start() {
        eventDispatcher.EventCallback += HandleEvent;
    }

    public void Init() {
        animator.Play("Idle");
        attackGuage = stat.attackInterval;
        targetDetectTime = TargetDetectInterval;
        RefreshHp();
    }

    public void SetPosition(Vector2Int coord) {
        transform.localPosition = new Vector3(coord.x * GameManager.CELL_OFFSET, -coord.y * GameManager.CELL_OFFSET + 5f, 0f);
    }

    private void Update() {
        if (GameManager.Instance.GameoverType != GAMEOVER_TYPE.NONE) return;
        
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
                    Debug.Log("target character is dead");
                    Idle();
                    break;
                }
                
                var vec = targetCharacter.transform.position - transform.position;
                
                if (Mathf.Abs(vec.magnitude) > stat.attackDetectRange) {
                    transform.Translate(vec * (Time.deltaTime * stat.speed), Space.Self);
                } else if (attackGuage <= 0f) {
                    Attack();
                }
                break;
            case Status.ATTACK:
                // damage 체크
                break;
            case Status.DIE:
                // die.....
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
        attackGuage = stat.attackInterval;
    }

    public void HandleEvent(string eventName) {
        if (eventName == "GiveDamage") {
            GameManager.Instance.Attack(targetCharacter, stat.attackPower);
        } else if (eventName == "FinishAttack") {
            ChangeStatus(Status.IDLE);
        } else if (eventName == "Disappear") {
            ChangeStatus(Status.DISAPPEAR);
            GameManager.Instance.Disappear(this);
        }
    }

    // // animation event
    // private void GiveDamage() {
    //     GameManager.Instance.Attack(targetCharacter, characterInfo.attackPower);
    // }
    //
    // // animation event
    // private void FinishAttack() {
    //     ChangeStatus(Status.IDLE);
    // }

    public void AttackedFromOther(float dmg) {
        stat.hp -= dmg;
        RefreshHp();
    }

    public void GameOver() {
        ChangeStatus(Status.SIT);
    }

    private void RefreshHp() {
        hpBar.material.SetFloat(Progress, stat.hp / character.hp);    // 현재 능력치 hp (curhp) / 캐릭터 기본 hp (max hp)
        if (stat.hp <= 0) {
            ChangeStatus(Status.DIE);
            isDead = true;
        }
    }

    private void ChangeStatus(Status s) {
        status = s;
        animator.SetTrigger(animationHash[status]);
    }
    
}

