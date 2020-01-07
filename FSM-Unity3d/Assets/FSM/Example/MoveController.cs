using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CLToolKits.FSM;

public class MoveController : MonoBehaviour
{
    public float speed =1f;
    public bool isMove;
    private FSMMachine fSMMachine;
    private State idle;
    private State move;
    private Transition idle2Move;
    private Transition move2idle;

    // Start is called before the first frame update
    void Start()
    {
        idle = new State("Idle");
        idle.OnEnter +=(IState state)=>{
            Debug.Log("进入Idle状态");
        };

        move = new State("Move");
        move.OnUpdate+=(float deltaTime)=>{
            transform.position += transform.forward * speed;
        };

        idle2Move = new Transition(idle,move);
        idle2Move.OnCheck+=()=>{
            return isMove;
        };
        idle.AddTransition(idle2Move);

        move2idle = new Transition(move,idle);
        move2idle.OnCheck += () => {
            return !isMove;
        };

        move.AddTransition(move2idle);

        fSMMachine = new FSMMachine("Root",idle);
        fSMMachine.AddState(move);
    }

    // Update is called once per frame
    void Update()
    {
        fSMMachine.UpdateCallback(Time.deltaTime);
    }

    void OnGUI()
    {
        if(GUILayout.Button("Move"))
        {
            isMove = true;
        }
        if(GUILayout.Button("Stop"))
        {
            isMove = false;
        }
    }
}
