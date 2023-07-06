using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using Biocrowds.Core;
using System;

public class Controller : MonoBehaviour
{

    [SerializeField]
    List<GameObject> allGoals;
    [SerializeField]
    List<GameObject> trajectory;
    [SerializeField]
    List<GameObject> allSpawns;

    [SerializeField]
    private GameObject world;

    KeywordRecognizer keywordRecognizer;
    [SerializeField]
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    int selectedGroup = -1;
    // Start is called before the first frame update
    void Start()
    {
        Spawn_Controller(0);
        keywords.Add("ir para "+allGoals[0].name, () =>
        {
            Debug.Log("ir para area A");
            Goal_Controller(0);
        });
        keywords.Add("ir para "+ allGoals[1].name, () =>
        {
            Debug.Log("ir para area B");
            Goal_Controller(1);
        });
        keywords.Add("ir para " + allGoals[2].name, () =>
        {
            Debug.Log("ir para area C");
            Goal_Controller(2);
        });
        keywords.Add("ir para " + allGoals[3].name, () =>
        {
            Debug.Log("ir para area D");
            Goal_Controller(3);
        });
        keywords.Add("ir para " + allGoals[4].name, () =>
        {
            Debug.Log("ir para area E");
            Goal_Controller(4);
        });
        keywords.Add("adicionar agente", () =>
        {
            world.GetComponent<World>().addMaxPop();
        });
        keywords.Add("remover agente", () =>
        {
            world.GetComponent<World>().removeMaxPop();
        });



        keywords.Add("grupo um", () =>
        {
            Debug.Log("Grupo 1 selected");
            selectedGroup = 0;
            GroupSelected_controller(0);
        });
        keywords.Add("grupo dois", () =>
        {
            Debug.Log("Grupo 2 selected");
            selectedGroup = 1;
            GroupSelected_controller(1);
        });
        keywords.Add("grupo tres", () =>
        {
            Debug.Log("Grupo 3 selected");
            selectedGroup = 2;
            GroupSelected_controller(2);
        });
        keywords.Add("grupo quatro", () =>
        {
            Debug.Log("Grupo 4 selected");
            selectedGroup = 3;
            GroupSelected_controller(3);
        });
        keywords.Add("grupo cinco", () =>
        {
            Debug.Log("Grupo 5 selected");
            selectedGroup = 4;
            GroupSelected_controller(4);
        });
        keywords.Add("Todos", () =>
        {
            Debug.Log("Todos selected");
            selectedGroup = -1;
            GroupSelected_controller(-1);
        });



        keywords.Add("fazer trajetória cirular", () =>
        {
            Debug.Log("ciruclando");
            Trajectory_Controller();
        });

        keywords.Add("transferir para grupo um", () =>
        {
            Debug.Log("Grupo 1 transfered");
            Transfer_controller(0);
        });
        keywords.Add("transferir para grupo dois", () =>
        {
            Debug.Log("Grupo 2 transfered");
            Transfer_controller(1);
        });
        keywords.Add("transferir para grupo tres", () =>
        {
            Debug.Log("Grupo 3 transfered");
            Transfer_controller(2);
        });
        keywords.Add("transferir para grupo quatro", () =>
        {
            Debug.Log("Grupo 4 transfered");
            Transfer_controller(3);
        });
        keywords.Add("transferir para grupo cinco", () =>
        {
            Debug.Log("Grupo 5 transfered");
            Transfer_controller(4);
        });


        keywords.Add("dividir para grupo um", () =>
        {
            Debug.Log("Divided to Grupo 1");
            Divide_controller(0);
        });
        keywords.Add("dividir para grupo dois", () =>
        {
            Debug.Log("Divided to Grupo 2");
            Divide_controller(1);
        });
        keywords.Add("dividir para grupo tres", () =>
        {
            Debug.Log("Divided to Grupo 3");
            Divide_controller(2);
        });
        keywords.Add("dividir para grupo quatro", () =>
        {
            Debug.Log("Divided to Grupo 4");
            Divide_controller(3);
        });
        keywords.Add("dividir para grupo cinco", () =>
        {
            Debug.Log("Divided to Grupo 5");
            Divide_controller(4);
        });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void Divide_controller(int groupIndex)
    {
        switch (selectedGroup)
        {
            case 0:
                world.GetComponent<World>().DivideAndSetGroup(world.GetComponent<World>().group1, groupIndex);
                break;
            case 1:
                world.GetComponent<World>().DivideAndSetGroup(world.GetComponent<World>().group2, groupIndex);
                break;
            case 2:
                world.GetComponent<World>().DivideAndSetGroup(world.GetComponent<World>().group3, groupIndex);
                break;
            case 3:
                world.GetComponent<World>().DivideAndSetGroup(world.GetComponent<World>().group4, groupIndex);
                break;
            case 4:
                world.GetComponent<World>().DivideAndSetGroup(world.GetComponent<World>().group5, groupIndex);
                break;
            default:
                world.GetComponent<World>().DivideAndSetGroup(world.GetComponent<World>()._agents, groupIndex);
                break;
        }
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    void Spawn_Controller(float pops) {
        world.GetComponent<World>().setMaxPop(pops);
    }

    void Goal_Controller(int goalIndex) {
        switch (selectedGroup) {
            case 0:
                world.GetComponent<World>().changeObjective(allGoals[goalIndex], world.GetComponent<World>().group1);
                break;
            case 1:
                world.GetComponent<World>().changeObjective(allGoals[goalIndex], world.GetComponent<World>().group2);
                break;
            case 2:
                world.GetComponent<World>().changeObjective(allGoals[goalIndex], world.GetComponent<World>().group3);
                break;
            case 3:
                world.GetComponent<World>().changeObjective(allGoals[goalIndex], world.GetComponent<World>().group4);
                break;
            case 4:
                world.GetComponent<World>().changeObjective(allGoals[goalIndex], world.GetComponent<World>().group5);
                break;
            default:
                world.GetComponent<World>().changeObjective(allGoals[goalIndex], world.GetComponent<World>()._agents);
                break;
        }
        
    }

    void Trajectory_Controller()
    {
        switch (selectedGroup)
        {
            case 0:
                world.GetComponent<World>().inLoopChangeObjective(trajectory, world.GetComponent<World>().group1);
                break;
            case 1:
                world.GetComponent<World>().inLoopChangeObjective(trajectory, world.GetComponent<World>().group2);
                break;
            case 2:
                world.GetComponent<World>().inLoopChangeObjective(trajectory, world.GetComponent<World>().group3);
                break;
            case 3:
                world.GetComponent<World>().inLoopChangeObjective(trajectory, world.GetComponent<World>().group4);
                break;
            case 4:
                world.GetComponent<World>().inLoopChangeObjective(trajectory, world.GetComponent<World>().group5);
                break;
            default:
                world.GetComponent<World>().inLoopChangeObjective(trajectory, world.GetComponent<World>()._agents);
                break;
        }

    }

    void GroupSelected_controller(int groupIndex) {
        world.GetComponent<World>().setSelectedGroup(groupIndex);
    }

    void Transfer_controller(int groupIndex)
    {
        switch (selectedGroup)
        {
            case 0:
                world.GetComponent<World>().setGroup(world.GetComponent<World>().group1, groupIndex);
                break;
            case 1:
                world.GetComponent<World>().setGroup(world.GetComponent<World>().group2, groupIndex);
                break;
            case 2:
                world.GetComponent<World>().setGroup(world.GetComponent<World>().group3, groupIndex);
                break;
            case 3:
                world.GetComponent<World>().setGroup(world.GetComponent<World>().group4, groupIndex);
                break;
            case 4:
                world.GetComponent<World>().setGroup(world.GetComponent<World>().group5, groupIndex);
                break;
            default:
                world.GetComponent<World>().setGroup(world.GetComponent<World>()._agents, groupIndex);
                break;
        }
    }

     // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
            world.GetComponent<World>().addMaxPop();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            world.GetComponent<World>().removeMaxPop();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            world.GetComponent<World>().printGroupCounts();

    }
}
