import * as React from 'react';
import { useState, useEffect } from 'react';
import {
    Box, TextField, Button, MenuItem, Typography, FormControlLabel, Switch,
    Accordion, AccordionSummary, AccordionDetails
} from '@mui/material';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import AddTaskIcon from '@mui/icons-material/AddTask';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import moment from 'moment';
import { LogI, LogW } from '../../../Utilities/Utilities';
import * as ArrayHelper from '../../../Utilities/ArrayHelper';
import TodoItemCycleForm from './TodoItemCycleForm';

const CreateTodoItemForm = ({ onCreateTodoItem, todoItems, projects, parentTaskId, parentProjectId }) => {
    const [formState, setFormState] = useState({
        title: '', description: '', deadline: null,
        parentId: parentTaskId ? parentTaskId : '',
        projectId: parentProjectId ? parentProjectId : '',
        isCyclic: false, repetitionPeriod: '1',
        startDate: null, endDate: null,
        isInfinite: true
    });

    useEffect(() => {
        setFormState({ ...formState, parentId: parentTaskId });
    }, [parentTaskId]);

    const handleFormChange = (formProperty, newValue) => {
        setFormState({ ...formState, [formProperty]: newValue });
    };

    const handleInputChange = e => {
        const { name, value } = e.target;
        handleFormChange(name, value);
    };

    const handleToggleChange = e => {
        const name = e.target.name;
        const value = e.target.checked;
        handleFormChange(name, value);
    };

    const handleDeadlineChange = newValue => {
        handleFormChange('deadline', newValue ? newValue.toDate() : null);
    };

    const handleCycleDefinitionChange = (todoItemCycle) => {
        setFormState({ ...formState, ...todoItemCycle });
    };

    const handleSubmit = e => {
        e.preventDefault();

        if (!formState.title) {
            LogW("Cannot create TodoItem - title not provided");
            return;
        }

        var todoItem = {
            title: formState.title,
            description: formState.description,
            deadline: formState.deadline,
            parentId: formState.parentId ? formState.parentId : null,
            projectId: formState.projectId ? formState.projectId : null,
            isVisible: true, isCyclic: formState.isCyclic,
            repetitionPeriod: formState.repetitionPeriod,
            startDate: formState.startDate ? formState.startDate : null,
            endDate: formState.endDate ? formState.endDate : null
        };

        onCreateTodoItem(todoItem);
        setFormState({ ...formState, title: '', description: '' });
    };

    return (
        <Box component="form" sx={{
            display: 'flex', flexDirection: 'column', alignItems: 'center',
            '& .MuiTextField-root': { m: 1, width: '100%' } }}
            noValidate autoComplete="off" onSubmit={e => handleSubmit(e)}>

            <Typography variant="h5">Create task</Typography>

            <TextField
                label="Title" name="title" variant="outlined" required autoFocus
                value={formState.title} onChange={handleInputChange} />

            <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center' }}>

                <TextField
                    label="Description" name="description" value={formState.description}
                    onChange={handleInputChange} multiline rows={3} />
                    
                <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                    <DesktopDatePicker
                        label="Deadline" name="deadline" inputFormat="DD.MM.YYYY" mask="__.__.____"
                        value={formState.deadline} onChange={handleDeadlineChange} minDate={moment()}
                        renderInput={(params) => <TextField {...params} />} />

                    <TextField
                        label="Parent task" name="parentId" select
                        disabled={!todoItems || todoItems.length === 0}
                        value={formState.parentId} onChange={handleInputChange}>
                        <MenuItem key={0} value={0}>Select parent task</MenuItem>
                        {todoItems.map((todoItem) => {
                            if (!todoItem.isCompleted && todoItem.isVisible) {
                                return (
                                    <MenuItem key={todoItem.id} value={todoItem.id} sx={{ pl: (todoItem.nestingLevel + 1) * 2 }}>
                                        {todoItem.title}
                                    </MenuItem>
                                )
                            }
                        })}
                    </TextField>
                </Box>
            </Box>
            {/* <TextField
                label="Project" name="projectId" select
                disabled={!projects || projects.length === 0} disabled
                value={formState.projectId} onChange={handleInputChange}>
                <MenuItem key={0} value={0}>Select project</MenuItem>
                {projects.map((project) => (
                    <MenuItem key={project.id} value={project.id}>
                        {project.title}
                    </MenuItem>
                ))}
            </TextField> */}



            <Accordion expanded={formState.isCyclic} name='isCyclic' sx={{ width: '100%' }}>
                <AccordionSummary id="cyclic-task-options">
                    <FormControlLabel
                        control={<Switch checked={formState.isCyclic} onChange={handleToggleChange} name='isCyclic' />}
                        label='Repeat task' />
                </AccordionSummary>
                <AccordionDetails>
                    <TodoItemCycleForm onCycleDefinitionChanged={handleCycleDefinitionChange} />
                </AccordionDetails>
            </Accordion>

            <Button sx={{ m: 1, width: '100%' }}
                variant="contained" type="submit"
                startIcon={<AddTaskIcon />}>
                Create
            </Button>
        </Box>
    );
}

CreateTodoItemForm.defaultProps = {
    todoItems: [],
    projects: [],
    parentTaskId: '',
    projectId: ''
};

export default CreateTodoItemForm;



// import * as React from 'react';
// import { useState, useEffect } from 'react';
// import {
//     Box, TextField, Button, MenuItem, Typography, FormControlLabel, Switch,
//     Accordion, AccordionSummary, AccordionDetails
// } from '@mui/material';
// import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
// import AddTaskIcon from '@mui/icons-material/AddTask';
// import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
// import moment from 'moment';
// import { LogI, LogW } from '../../../Utilities/Utilities';
// import * as ArrayHelper from '../../../Utilities/ArrayHelper';
// import TodoItemCycleForm from './TodoItemCycleForm';

// const CreateTodoItemForm = ({ onCreateTodoItem, todoItems, projects, parentTaskId, parentProjectId }) => {
//     const [formState, setFormState] = useState({
//         title: '', description: '', deadline: null,
//         parentId: parentTaskId ? parentTaskId : '',
//         projectId: parentProjectId ? parentProjectId : '',
//         isCyclic: false, repetitionPeriod: '1',
//         startDate: null, endDate: null
//     });

//     useEffect(() => {
//         setFormState({ ...formState, parentId: parentTaskId });
//     }, [parentTaskId]);

//     const handleFormChange = (formProperty, newValue) => {
//         setFormState({ ...formState, [formProperty]: newValue });
//     };

//     const handleInputChange = e => {
//         const { name, value } = e.target;
//         handleFormChange(name, value);
//     };

//     const handleToggleChange = e => {
//         const name = e.target.name;
//         const value = e.target.checked;
//         handleFormChange(name, value);
//     };

//     const handleDeadlineChange = newValue => {
//         handleFormChange('deadline', newValue ? newValue.toDate() : null);
//     };

//     const handleCycleDefinitionChange = (todoItemCycle) => {
//         setFormState({ ...formState, ...todoItemCycle });
//     };

//     const handleSubmit = e => {
//         e.preventDefault();

//         if (!formState.title) {
//             LogW("Cannot create TodoItem - title not provided");
//             return;
//         }

//         var todoItem = {
//             title: formState.title,
//             description: formState.description,
//             deadline: formState.deadline,
//             parentId: formState.parentId ? formState.parentId : null,
//             projectId: formState.projectId ? formState.projectId : null,
//             isVisible: true, isCyclic: formState.isCyclic,
//             repetitionPeriod: formState.repetitionPeriod,
//             startDate: formState.startDate ? formState.startDate : null,
//             endDate: formState.endDate ? formState.endDate : null
//         };

//         onCreateTodoItem(todoItem);
//         setFormState({ ...formState, title: '', description: '' });
//     };

//     return (
//         <Box component="form" sx={{
//             display: 'flex', flexDirection: 'column', alignItems: 'center',
//             '& .MuiTextField-root': { m: 1, width: '100%' } }}
//             noValidate autoComplete="off" onSubmit={e => handleSubmit(e)}>

//             <Typography variant="h5">Create task</Typography>

//             <TextField
//                 label="Title" name="title" variant="outlined" required autoFocus
//                 value={formState.title} onChange={handleInputChange} />

//             <TextField
//                 label="Description" name="description" value={formState.description}
//                 onChange={handleInputChange} multiline rows={3} />

//             <DesktopDatePicker
//                 label="Deadline" name="deadline" inputFormat="DD.MM.YYYY" mask="__.__.____"
//                 value={formState.deadline} onChange={handleDeadlineChange} minDate={moment()}
//                 renderInput={(params) => <TextField {...params} />} />

//             <TextField
//                 label="Project" name="projectId" select
//                 disabled={!projects || projects.length === 0} disabled
//                 value={formState.projectId} onChange={handleInputChange}>
//                 <MenuItem key={0} value={0}>Select project</MenuItem>
//                 {projects.map((project) => (
//                     <MenuItem key={project.id} value={project.id}>
//                         {project.title}
//                     </MenuItem>
//                 ))}
//             </TextField>

//             <TextField
//                 label="Parent task" name="parentId" select
//                 disabled={!todoItems || todoItems.length === 0}
//                 value={formState.parentId} onChange={handleInputChange}>
//                 <MenuItem key={0} value={0}>Select parent task</MenuItem>
//                 {todoItems.map((todoItem) => {
//                     if (!todoItem.isCompleted && todoItem.isVisible) {
//                         return (
//                             <MenuItem key={todoItem.id} value={todoItem.id} sx={{ pl: (todoItem.nestingLevel + 1) * 2 }}>
//                                 {todoItem.title}
//                             </MenuItem>
//                         )
//                     }
//                 })}
//             </TextField>

//             <Accordion expanded={formState.isCyclic} name='isCyclic' sx={{width: '100%'}}>
//                 <AccordionSummary id="cyclic-task-options">
//                     <FormControlLabel
//                         control={<Switch checked={formState.isCyclic} onChange={handleToggleChange} name='isCyclic' />}
//                         label='Repeat task' />
//                 </AccordionSummary>
//                 <AccordionDetails>
//                     <TodoItemCycleForm onCycleDefinitionChanged={handleCycleDefinitionChange} />
//                 </AccordionDetails>
//             </Accordion>

//             <Button sx={{ m: 1, width: '100%' }}
//                 variant="contained" type="submit"
//                 startIcon={<AddTaskIcon />}>
//                 Create
//             </Button>
//         </Box>
//     );
// }

// CreateTodoItemForm.defaultProps = {
//     todoItems: [],
//     projects: [],
//     parentTaskId: '',
//     projectId: ''
// };

// export default CreateTodoItemForm;