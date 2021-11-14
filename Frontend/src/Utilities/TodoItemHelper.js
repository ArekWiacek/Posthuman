import { LogE } from './Utilities';
import * as ArrayHelper from './ArrayHelper';

const TodoItemHelper = {
    CreateProgressText: (todoItem) => {
        var progressText = "";

        if (todoItem.hasSubtasks) {
            var progressPercentage = Math.round((todoItem.finishedSubtasksCount / todoItem.subtasksCount) * 100);
            progressText = todoItem.finishedSubtasksCount + " / " + todoItem.subtasksCount + " (" + progressPercentage + "%)";
        }
        else {
            todoItem.isCompleted ? progressText = "Done!" : progressText = "Not completed";
        }

        return progressText;
    },

    CalculateNestingLevel: (todoItems, todoItem) => {
        let nestingLevel = 0;

        if(!todoItem.parentId) {
            //let parent = 
        }
    } 
}

export default TodoItemHelper;