using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class playAnimation : MonoBehaviour
{
    private GameObject _objToAnimate;
    private string _obj_type;
    private Animator anim;
    
    int dragon_scream;
    int dragon_basicAttack;
    int dragon_clawAttack;
    int dragon_flameAttack;
    int dragon_defend;
    int dragon_getHit;
    int dragon_sleep;
    int dragon_walk;
    int dragon_run;
    int dragon_takeOff;
    int dragon_flyFlameAttack;
    int dragon_flyForward;
    int dragon_flyGlide;
    int dragon_land;
    int dragon_die;
    int dragon_idle01;
    int dragon_idle02;

    int wizard_basicAttack;
    int wizard_die;
    int wizard_getHit;
    int wizard_walk;
    int wizard_run;
    int wizard_idle;
    int wizard_idleCombat;

    int scream;
    int basicAttack;
    int clawAttack;
    int flameAttack;
    int defend;
    int getHit;
    int sleep;
    int walk;
    int run;
    int takeOff;
    int flyFlameAttack;
    int flyForward;
    int flyGlide;
    int land;
    int die;
    int idle01;
    int idle02;

    private void UpdateObjToAnim()
    {
        _objToAnimate = GameObject.Find("Terrain").GetComponent<ActionModels>()._obj;
        string[] name = _objToAnimate.name.Split('_');
        _obj_type = name[0];
        if (_obj_type == "dragon")
        {
            anim = _objToAnimate.GetComponent<Animator>();

            scream = dragon_scream;
            basicAttack = dragon_basicAttack;
            clawAttack = dragon_clawAttack;
            flameAttack = dragon_flameAttack;
            defend = dragon_defend;
            getHit = dragon_getHit;
            sleep = dragon_sleep;
            walk = dragon_walk;
            run = dragon_run;
            takeOff = dragon_takeOff;
            flyFlameAttack = dragon_flyFlameAttack;
            flyForward = dragon_flyForward;
            flyGlide = dragon_flyGlide;
            land = dragon_land;
            die = dragon_die;
            idle01 = dragon_idle01;
            idle02 = dragon_idle02;
        }

        if(_obj_type == "wizard")
        {
            basicAttack = wizard_basicAttack;
            die = wizard_die;
            getHit =  wizard_getHit;
            walk = wizard_walk;
            run = wizard_run;
            idle02 = wizard_idle;
            idle02 = wizard_idleCombat;
        }
    }

    bool CheckAniClip(string clipname)
    {
        if (_objToAnimate.GetComponent<Animation>().GetClip(clipname) == null)
            return false;
        else if (_objToAnimate.GetComponent<Animation>().GetClip(clipname) != null)
            return true;

        return false;
    }

    void Awake()
    {
        // dragon animations
        dragon_scream = Animator.StringToHash("Scream");
        dragon_basicAttack = Animator.StringToHash("Basic Attack");
        dragon_clawAttack = Animator.StringToHash("Claw Attack");
        dragon_flameAttack = Animator.StringToHash("Flame Attack");
        dragon_defend = Animator.StringToHash("Defend");
        dragon_getHit = Animator.StringToHash("Get Hit");
        dragon_sleep = Animator.StringToHash("Sleep");
        dragon_walk = Animator.StringToHash("Walk");
        dragon_run = Animator.StringToHash("Run");
        dragon_takeOff = Animator.StringToHash("Take Off");
        dragon_flyFlameAttack = Animator.StringToHash("Fly Flame Attack");
        dragon_flyForward = Animator.StringToHash("Fly Forward");
        dragon_flyGlide = Animator.StringToHash("Fly Glide");
        dragon_land = Animator.StringToHash("Land");
        dragon_die = Animator.StringToHash("Die");
        dragon_idle01 = Animator.StringToHash("Idle01");
        dragon_idle02 = Animator.StringToHash("Idle02");

        // wizard animation
        wizard_basicAttack = Animator.StringToHash("attack_short_001");
        wizard_die = Animator.StringToHash("dead");
        wizard_getHit = Animator.StringToHash("damage_001");
        wizard_walk = Animator.StringToHash("move_forward");
        wizard_run = Animator.StringToHash("move_forward_fast");
        wizard_idle = Animator.StringToHash("idle_normal");
        wizard_idleCombat = Animator.StringToHash("idle_combat");
    }


    public void Scream()
    {
        UpdateObjToAnim();
        anim.SetTrigger(scream);
    }

    public void BasicAttack()
    {
        UpdateObjToAnim();

        if (_obj_type == "dragon")
            anim.SetTrigger(basicAttack);

        else if (_obj_type == "wizard")
        {
            if (CheckAniClip("attack_short_001") == false) return;
            Debug.Log("basicAttack");
            _objToAnimate.GetComponent<Animation>().CrossFade("attack_short_001", 0.0f);
            _objToAnimate.GetComponent<Animation>().CrossFadeQueued("idle_combat");
        }
    }
    
    public void SendBasicAttackAnimation() { 
        // send string
        CreateString sendString = GameObject.Find("ScriptHolderCreateString").GetComponent<CreateString>();
        sendString.Create(OrderType.ATTACK,
            _objToAnimate.name, Vector3.zero);
    }

    public void ClawAttack()
    {
        UpdateObjToAnim();

        if (_obj_type == "dragon")
            anim.SetTrigger(clawAttack);

        else if (_obj_type == "wizard")
        {
            if (CheckAniClip("attack_short_001") == false) return;
            Debug.Log("basicAttack");
            _objToAnimate.GetComponent<Animation>().CrossFade("attack_short_001", 0.0f);
            _objToAnimate.GetComponent<Animation>().CrossFadeQueued("idle_combat");
        }
    }

    public void FlameAttack()
    {
        UpdateObjToAnim();

        if (_obj_type == "dragon")
            anim.SetTrigger(flameAttack);
        
        else if (_obj_type == "wizard")
        {
            if (CheckAniClip("attack_short_001") == false) return;
            Debug.Log("basicAttack");
            _objToAnimate.GetComponent<Animation>().CrossFade("attack_short_001", 0.0f);
            _objToAnimate.GetComponent<Animation>().CrossFadeQueued("idle_combat");
        }
    }

    public void SendFlameAttackAnimation()
    {
        // send string
        CreateString sendString = GameObject.Find("ScriptHolderCreateString").GetComponent<CreateString>();
        sendString.Create(OrderType.HUGEATTACK,
            _objToAnimate.name, Vector3.zero);
    }

    public void Defend()
    {
        UpdateObjToAnim();
        anim.SetTrigger(defend);
    }

    public void GetHit()
    {
        UpdateObjToAnim();
        if (_obj_type == "dragon")
            anim.SetTrigger(getHit);
        else if (_obj_type == "wizard")
        {
            if (CheckAniClip("damage_001") == false) return;

            _objToAnimate.GetComponent<Animation>().CrossFade("damage_001", 0.0f);
            _objToAnimate.GetComponent<Animation>().CrossFadeQueued("idle_combat");
        }
    }

    public void SendGetHitAnimation()
    {
        // send string
        CreateString sendString = GameObject.Find("ScriptHolderCreateString").GetComponent<CreateString>();
        sendString.Create(OrderType.GETHIT,
            _objToAnimate.name, Vector3.zero);
    }

    public void Sleep()
    {
        UpdateObjToAnim();
        anim.SetTrigger(sleep);
    }

    public void Walk()
    {
        UpdateObjToAnim();
        if (_obj_type == "dragon")
            anim.SetTrigger(walk);
        else if (_obj_type == "wizard")
        {
            if (CheckAniClip("move_forward") == false) return;

            _objToAnimate.GetComponent<Animation>().CrossFade("move_forward");
        }
    }

    public void Run()
    {
        UpdateObjToAnim();
        if (_obj_type == "dragon")
            anim.SetTrigger(run);
        else if (_obj_type == "wizard")
        {
            if (CheckAniClip("move_forward_fast") == false) return;

            _objToAnimate.GetComponent<Animation>().CrossFade("move_forward_fast");
        }
    }

    public void TakeOff()
    {
        UpdateObjToAnim();
        anim.SetTrigger(takeOff);
    }

    public void FlyFlameAttack()
    {
        UpdateObjToAnim();
        anim.SetTrigger(flyFlameAttack);
    }

    public void FlyForward()
    {
        UpdateObjToAnim();
        anim.SetTrigger(flyForward);
    }

    public void FlyGlide()
    {
        UpdateObjToAnim();
        anim.SetTrigger(flyGlide);
    }

    public void Land()
    {
        UpdateObjToAnim();
        anim.SetTrigger(land);
    }

    public void Die()
    {
        UpdateObjToAnim();
        if (_obj_type == "dragon")
            anim.SetTrigger(die);
        else if (_obj_type == "wizard")
        {
            if (CheckAniClip("dead") == false) return;

            _objToAnimate.GetComponent<Animation>().CrossFade("dead", 0.2f);
        }
    }

    public void SendDieAnimation()
    {
        // send string
        CreateString sendString = GameObject.Find("ScriptHolderCreateString").GetComponent<CreateString>();
        sendString.Create(OrderType.DIE,
            _objToAnimate.name, Vector3.zero);
    }

    public void Idle01()
    {
        UpdateObjToAnim();
        if (_obj_type == "dragon")
            anim.SetTrigger(idle01);
        else if (_obj_type == "wizard")
        {
            if (CheckAniClip("idle_normal") == false) return;

            _objToAnimate.GetComponent<Animation>().CrossFade("idle_normal", 0.0f);
            _objToAnimate.GetComponent<Animation>().CrossFadeQueued("idle_normal");
        }

        // send string
        CreateString sendString = GameObject.Find("ScriptHolderCreateString").GetComponent<CreateString>();
        sendString.Create(OrderType.IDLE1,
            _objToAnimate.name, Vector3.zero);
    }

    public void Idle02()
    {
        UpdateObjToAnim();
        if(_obj_type == "dragon")
            anim.SetTrigger(idle02);
        else if(_obj_type == "wizard")
        {
            if (CheckAniClip("idle_combat") == false) return;

            _objToAnimate.GetComponent<Animation>().CrossFade("idle_combat", 0.0f);
            _objToAnimate.GetComponent<Animation>().CrossFadeQueued("idle_normal");
        }
    }

}
