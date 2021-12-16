using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface ITodoItemsCyclesService
    {
        TodoItemCycle CreateCycleInfo(RepetitionPeriod repetitionPeriod, DateTime startDate, DateTime? endDate);
        TodoItemCycle CreateCycleInfo(CreateTodoItemDTO createTodoItemDTO);


        void RecalculateCycleInstancesInfo(TodoItemCycle cycleInfo);
    }
}
