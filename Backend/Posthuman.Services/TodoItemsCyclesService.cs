using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.Core.Services;
using Posthuman.Services.Helpers;
using Posthuman.RealTime.Notifications;

namespace Posthuman.Services
{
    public class TodoItemsCyclesService
    {
        public TodoItemsCyclesService()
        {
        }

        //public TodoItemCycle CreateCycleInfo(RepetitionPeriod repetitionPeriod, DateTime startDate, DateTime? endDate = null)
        //{
        //    if (endDate.HasValue && startDate > endDate)
        //        throw new ArgumentException("startDate can not be greater than endDate");

        //    TodoItemCycle cycleInfo = new TodoItemCycle();
        //    cycleInfo.RepetitionPeriod = repetitionPeriod;
        //    cycleInfo.StartDate = startDate;
        //    cycleInfo.EndDate = endDate;
        //    cycleInfo.IsInfinite = !endDate.HasValue;

        //    return cycleInfo;
        //}


        //private void ValidateCycleData(CreateTodoItemDTO createTodoItemDTO)
        //{
        //    if (!createTodoItemDTO.IsCyclic)
        //        throw new ArgumentException("Cannot create cycle info for non-cyclic todo item");

        //    if (!createTodoItemDTO.RepetitionPeriod.HasValue)
        //        throw new ArgumentException("Cannot create cycle info - RepetitionPeriod is not set");

        //    if (createTodoItemDTO.EndDate.HasValue &&
        //        createTodoItemDTO.StartDate.HasValue &&
        //        createTodoItemDTO.StartDate.Value > createTodoItemDTO.EndDate.Value)
        //        throw new ArgumentException("StartDate can not be greater than EndDate");
        //}

        //public TodoItemCycle CreateCycleInfo(CreateTodoItemDTO createTodoItemDTO)
        //{
        //    ValidateCycleData(createTodoItemDTO);

        //    TodoItemCycle cycleInfo = new TodoItemCycle
        //    {
        //        RepetitionPeriod = (RepetitionPeriod)createTodoItemDTO.RepetitionPeriod.Value,
        //        StartDate = createTodoItemDTO.StartDate.HasValue ? createTodoItemDTO.StartDate.Value : DateTime.Now.Date,
        //        EndDate = createTodoItemDTO.EndDate,
        //        IsInfinite = !createTodoItemDTO.EndDate.HasValue,
        //        TotalInstances = 0,
        //        MissedInstances = 0,
        //        CompletedInstances = 0,
        //        InstancesStreak = 0
        //    };
            
        //    RecalculateCycleInstancesInfo(cycleInfo);

        //    return cycleInfo;
        //}

        //public void RecalculateCycleInstancesInfo(TodoItemCycle cycleInfo)
        //{
        //    if (!cycleInfo.IsInfinite && cycleInfo.EndDate.HasValue)
        //    {
        //        int futureInstances = 0;
        //        int pastInstances = 0;
        //        int totalInstances = CalculateInstancesBetweenDates(cycleInfo.RepetitionPeriod, cycleInfo.StartDate, cycleInfo.EndDate.Value);
                
        //        if(cycleInfo.StartDate.Date > DateTime.Now.Date)
        //            futureInstances = CalculateInstancesBetweenDates(cycleInfo.RepetitionPeriod, cycleInfo.StartDate, cycleInfo.EndDate.Value);
        //        else
        //            futureInstances = CalculateInstancesBetweenDates(cycleInfo.RepetitionPeriod, DateTime.Now.Date, cycleInfo.EndDate.Value);

        //        if(cycleInfo.StartDate.Date < DateTime.Now.Date)
        //            pastInstances = CalculateInstancesBetweenDates(cycleInfo.RepetitionPeriod, cycleInfo.StartDate, DateTime.Now);

        //        cycleInfo.MissedInstances = pastInstances - cycleInfo.CompletedInstances;
        //        cycleInfo.TotalInstances = totalInstances;
        //        cycleInfo.InstancesToComplete = futureInstances;
        //    }
        //    else
        //    {
        //        cycleInfo.MissedInstances = 0;
        //        cycleInfo.TotalInstances = 0;
        //        cycleInfo.InstancesToComplete = 0;
        //    }
        //}


        //private DateTime GetNearestInstanceDate(RepetitionPeriod repetitionPeriod, DateTime startDate)
        //{
        //    var today = DateTime.Now.Date;

        //    // Task starts today so for sure today is nearest instance
        //    if (today == startDate)
        //        return today;

        //    var nextInstance = startDate;
        //    switch (repetitionPeriod)
        //    {
        //        case RepetitionPeriod.Daily:
        //            nextInstance = (endDate.Date - startDate.Date).Days + 1;
        //            break;

        //        case RepetitionPeriod.Weekly:
        //            instances = (endDate - startDate).Days / 7 + 1;
        //            break;

        //        case RepetitionPeriod.Monthly:
        //            instances = 666;
        //            break;

        //        default:
        //            break;
        //    }
        //}

        /// <summary>
        /// Returns number of task instances between two given dates
        /// </summary>
        //private int CalculateInstancesBetweenDates(RepetitionPeriod repetitionPeriod, DateTime startDate, DateTime endDate)
        //{
        //    int instances = 0;
        //    //if (repetitionPeriod == RepetitionPeriod.Daily)
        //    //{
        //    //    instances = (endDate.Date - startDate.Date).Days + 1;
        //    //}
        //    //else
        //    //{
        //        var nextDate = GetNextInstanceDate(repetitionPeriod, startDate);

        //        while (nextDate < endDate)
        //        {
        //            instances++;
        //            nextDate = GetNextInstanceDate(repetitionPeriod, nextDate);
        //        }
        //    //}

        //    return instances;
        //}

        //// For given date, returns next date after given repetition period
        //private DateTime GetNextInstanceDate(RepetitionPeriod repetitionPeriod, DateTime date)
        //{
        //    DateTime nextInstanceDate = date;

        //    switch (repetitionPeriod)
        //    {
        //        //case RepetitionPeriod.Daily:
        //        //    nextInstanceDate = DateTime.Now.AddDays(1);
        //        //    break;

        //        case RepetitionPeriod.Weekly:
        //            nextInstanceDate = DateTime.Now.AddDays(7);
        //            break;

        //        case RepetitionPeriod.Monthly:
        //            nextInstanceDate = DateTime.Now.AddMonths(1);
        //            break;
        //    }

        //    return nextInstanceDate;
        //}

        //// For given date, returns previous date before given repetition period
        //private DateTime? GetPreviousInstanceDate(RepetitionPeriod repetitionPeriod, DateTime date)
        //{
        //    DateTime previousInstanceDate = date;

        //    switch (repetitionPeriod)
        //    {
        //        //case RepetitionPeriod.Daily:
        //        //    previousInstanceDate = date.AddDays(-1);
        //        //    break;

        //        case RepetitionPeriod.Weekly:
        //            previousInstanceDate = date.AddDays(-7);
        //            break;

        //        case RepetitionPeriod.Monthly:
        //            previousInstanceDate = date.AddMonths(-1);
        //            break;
        //    }

        //    return previousInstanceDate;
        //}

        //private bool DidMissedLastInstance(TodoItem todoItem)
        //{
        //    if (todoItem.TodoItemCycle == null)
        //        return false;

        //    var cycleInfo = todoItem.TodoItemCycle;
        //    var previousInstanceDate = GetPreviousInstanceDate(cycleInfo.RepetitionPeriod, DateTime.Now.Date);

        //    // There was no previous instance - first one is still ahead of current time
        //    if (!previousInstanceDate.HasValue)
        //        return false;

        //    // Previous instance for sure missed - no completion date is saved
        //    if (!cycleInfo.LastCompletedInstanceDate.HasValue)
        //        return true;

        //    // Both dates are present - they should be equal
        //    if (previousInstanceDate.Value.Date == cycleInfo.LastCompletedInstanceDate.Value.Date)
        //        return false;
        //    else
        //        return true;
        //}

    }
}
