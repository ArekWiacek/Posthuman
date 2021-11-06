import React, { useState } from 'react';
import StepWizard from 'react-step-wizard';
import WizardStep from './WizardStep';
import Typography from '@mui/material/Typography';

const Wizard = ( { onWizardFinished, onWizardCanceled, todoItem } ) => {
    const [state, updateState] = useState({
        transitions: {
            enterRight: `animated enterRight`,
            enterLeft: `animated enterLeft`,
            exitRight: `animated exitRight`,
            exitLeft: `animated exitLeft`,
            intro: `animated intro`,
        }
    });

    const setInstance = SW => updateState({
        ...state,
        SW,
    });

    const { SW, demo } = state;

    const handleTaskDoneConfirm = () => state.SW.nextStep();
    const handleTaskDoneCancel = () => onWizardCanceled();
    const handleXpGainedConfirm = () => onWizardFinished();

    return (
        <div className={`col-12 col-sm-6 offset-sm-3`}>
            <StepWizard
                // onStepChange={onStepChange}
                transitions={state.transitions}
                instance={setInstance}
            >
                <WizardStep
                    key={1}
                    confirmText={'Yes'}
                    cancelText={'No'}
                    onConfirm={handleTaskDoneConfirm}
                    onCancel={handleTaskDoneCancel}
                >
                    <Typography variant="h3">{'Confirm task completion'}</Typography>
                    <Typography variant="body2">{'Did you completed task: "' + todoItem.title + '"?'}</Typography>
                </WizardStep>

                <WizardStep
                    key={2}
                    confirmText={'Thanks!'}
                    onConfirm={handleXpGainedConfirm}
                >
                    <Typography variant="h3">{'XP Gained!'}</Typography>
                    <Typography variant="body2">{'You gained 25 XP for completing task: "' + todoItem.title + '"!'}</Typography>
                    <Typography variant="body2">{'Congratulations!'}</Typography>
                </WizardStep>
            </StepWizard>
        </div>
    );
};

export default Wizard;