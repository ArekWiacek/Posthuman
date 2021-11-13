import React, { useState } from 'react';
import { Typography } from '@mui/material';
import StepWizard from 'react-step-wizard';
import WizardStep from './WizardStep';

const Wizard = ( {onWizardFinished, onWizardCanceled, todoItem, xpGained} ) => {
    const [state, updateState] = useState({
        transitions: {
            enterRight: `animated enterRight`,
            enterLeft: `animated enterLeft`,
            exitRight: `animated exitRight`,
            exitLeft: `animated exitLeft`,
            intro: `animated intro`,
        }
    });

    const setInstance = SW => updateState({ ...state, SW });
    const handleTaskDoneConfirm = () => state.SW.nextStep();
    const handleTaskDoneCancel = () => onWizardCanceled();
    const handleXpGainedConfirm = () => onWizardFinished();

    return (
        <div className={`col-12 col-sm-6 offset-sm-3`}>
            <StepWizard
                transitions={state.transitions}
                instance={setInstance}>
                <WizardStep
                    key={1}
                    confirmText={'Yes'}
                    cancelText={'No'}
                    onConfirm={handleTaskDoneConfirm}
                    onCancel={handleTaskDoneCancel}>
                    <Typography variant="h4">{todoItem.title}</Typography>
                    <Typography variant="body2">{'Confirm that you completed this task.'}</Typography>
                </WizardStep>

                <WizardStep
                    key={2}
                    confirmText={'Thanks!'}
                    onConfirm={handleXpGainedConfirm}>
                    <Typography variant="h4" sx={{color: 'success.main'}}>
                        {'+ ' + xpGained + ' XP!'}
                    </Typography>
                    <Typography variant="body2">
                        {'For completing task "' + todoItem.title + '".'}
                    </Typography>
                </WizardStep>
            </StepWizard>
        </div>
    );
};

export default Wizard;