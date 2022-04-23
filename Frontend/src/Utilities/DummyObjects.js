// Place where stub/mock/test objects can be generated


export function CreateDummyTodoItems(howMany) {
    var dummyTodoItems = [];

    for (var i = 1; i <= howMany; i++) {

        var dummyTodo = {
            id: i,
            title: "[Dummy todo title: " + i + "]",
            description: "[Sample description: " + i + "]",
            isCompleted: i % 2 ? false : true,
            deadline: null, //new Date(),
            projectId: null,
            avatarId: 1,
            isCyclic: false,
            repetitionPeriod: 1,
            startDate: new Date(),
            endDate: null
        };

        dummyTodoItems.push(dummyTodo);
    }

    return dummyTodoItems;
}


export function CreateDummyProject() {
    var dummyProject = {
        id: 666,
        title: "[Dummy project title]",
        description: "[Dummy project description]",
        isFinished: false,
        startDate: new Date(),
        avatarId: 1,
        totalSubtasks: 4,
        completedSubtasks: 3
    };

    return dummyProject;
}

export function CreateDummyProjects(howMany) {
    var dummyProjects = [];
    
    for (var i = 1; i <= howMany; i++) {
        var dummyProject = {
            id: i,
            title: "[Dummy project " + i + " title]",
            description: "[Dummy project " + i + " description]",
            isFinished: i % 2 ? true : false,
            startDate: new Date(),
            avatarId: 1,
            totalSubtasks: 4,
            completedSubtasks: i % 3,
        };
        dummyProjects.push(dummyProject);
    }

    return dummyProjects;
}

export function CreateDummyHabit() {
    var dummyHabit = {
        id: 666,
        title: "[Dummy habit title]",
        description: "[Dummy habit description]",
    };

    return dummyHabit;
}

export function CreateDummyHabits(howMany) {
    var dummyHabits = [];

    for(var i = 1 ; i <= howMany ; i++) {
        dummyHabits.push(CreateDummyHabit());
    }
    
    return dummyHabits;
}