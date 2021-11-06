import * as React from 'react';
import Box from '@mui/material/Box';
import Stepper from '@mui/material/Stepper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';

const StepperWizardStep = ({ label, stepProps, labelProps }) => {

  return (
    <Step key={label} {...stepProps}>
      <StepLabel {...labelProps}>{label}</StepLabel>
    </Step>
  );
}

export default StepperWizardStep;