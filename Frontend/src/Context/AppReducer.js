import React from 'react';

export default (state, action) => {
    switch(action.type) {
        case 'SET_AVATAR':
            return { 
                currentAvatar: [action.currentAvatar, ...state.currentAvatar]

            }

        default:
            return state;
        }
}