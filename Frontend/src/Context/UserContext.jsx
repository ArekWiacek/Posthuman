import { createContext } from 'react';

const UserContext = createContext({
    userContext: { 
        id: 0, 
        name: 'context default name', 
        email: 'context default name'
    },
    
    setUserContext: () => { }
});

export default UserContext; 
