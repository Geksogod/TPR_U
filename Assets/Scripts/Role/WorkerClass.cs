using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Human))]
public class WorkerClass : HumanClass
{
    [SerializeField]
    private float extractionForce;
    private Inventory inventory;
    private Human human;
    private CreatureMove creatureMove;
    [SerializeField][ReadOnly]
    private bool goingToStorage;
    private Building nearbyStorage;

    public void AddExtractionForce(float _extractionForce)
    {
        extractionForce += _extractionForce;
    }

    public void SetExtractionForce(float newExtractionForce)
    {
        extractionForce = newExtractionForce;
    }

    public override void StartDoTask()
    {
        base.StartDoTask();
        switch (currentTask.GetTaskType())
        {
            case TaskManager.TaskType.GetResources:
                creatureMove.AddDestinationTarget(currentTask.GetTaskTarget().transform.position);
                goingToStorage = false;
                human.ChangeState(Creature.CreatureState.Moving);
                creatureMove.ChangeStopingDistance(2);
                break;
            case TaskManager.TaskType.MoveTo:
                creatureMove.AddDestinationTarget(currentTask.GetTaskTarget().transform.position);
                human.ChangeState(Creature.CreatureState.Moving);
                creatureMove.ChangeStopingDistance(currentTask.GetTaskTarget().GetComponent<Collider>().bounds.size.x);
                break;
            case TaskManager.TaskType.BuildBuilding:
                creatureMove.AddDestinationTarget(currentTask.GetTaskTarget().transform.position);
                human.ChangeState(Creature.CreatureState.Moving);
                creatureMove.ChangeStopingDistance(currentTask.GetTaskTarget().GetComponent<Collider>().bounds.size.x);
                break;

        }
    }

    public override void DoindTask()
    {
        base.DoindTask();
        if (currentTask == null)
            return;

        switch (currentTask.GetTaskType())
        {
            case TaskManager.TaskType.GetResources:
                if (creatureMove.isArrived()&& !goingToStorage && creatureMove.GetLastDestinationTarget() == currentTask.GetTaskTarget().transform.position)
                {
                    GetResources(currentTask.GetTaskTarget().GetComponent<ResourcesMaterial>());
                    human.ChangeState(Creature.CreatureState.Working);
                    goingToStorage = inventory.IsFull();
                    if (goingToStorage)
                        nearbyStorage = BuildingManager.instance.FindNearbyStorage(transform.position, BuildingManager.BuildingType.Storage);
                }
                else if(goingToStorage && creatureMove.isArrived() && creatureMove.GetLastDestinationTarget() != nearbyStorage.transform.position)
                {
                    creatureMove.AddDestinationTarget(nearbyStorage.transform.position);
                    human.ChangeState(Creature.CreatureState.Moving);
                }
                else if(goingToStorage && creatureMove.isArrived() && creatureMove.GetLastDestinationTarget() == nearbyStorage.transform.position)
                {
                    nearbyStorage.GetComponent<Inventory>().AddItems(inventory.GetAllResources());
                    human.ChangeState(Creature.CreatureState.Idle);
                    goingToStorage = false;
                }
                else if(!goingToStorage && creatureMove.isArrived() && creatureMove.GetLastDestinationTarget() == nearbyStorage.transform.position)
                {
                    creatureMove.AddDestinationTarget(currentTask.GetTaskTarget().transform.position);
                    human.ChangeState(Creature.CreatureState.Moving);
                }
                break;
            case TaskManager.TaskType.MoveTo:
                break;
            case TaskManager.TaskType.BuildBuilding:
                if (creatureMove.isArrived() && !goingToStorage && creatureMove.GetLastDestinationTarget() == currentTask.GetTaskTarget().transform.position)
                {
                    human.ChangeState(Creature.CreatureState.Working);
                    currentTask.GetTaskTarget().GetComponent<Building>().AddProgress(Time.deltaTime*3);
                }
                break;
        }
    }

    private void GetResources(ResourcesMaterial resurcesTarget)
    {
        if (!resurcesTarget.CanTakeResources())
            resurcesTarget.ExtractResources(extractionForce);
        else
            inventory.AddItem(resurcesTarget.TakeResources());
    }

    protected override void InitializationHumanClass()
    {
        inventory = GetComponent<Inventory>();
        human = GetComponent<Human>();
        creatureMove = gameObject.GetComponent<CreatureMove>();
        WorkerManager.Initialize(this);
    }

    protected override void FinisedTask()
    {
        if (currentTask.GetTaskType() == TaskManager.TaskType.MoveTo && currentTask.GetTaskTarget().GetComponent<Inventory>() != null)
        {
            currentTask.GetTaskTarget().GetComponent<Inventory>().AddItems(inventory.GetAllResources());
        }
            base.FinisedTask();
        human.ChangeState(Creature.CreatureState.Idle);
    }

}
