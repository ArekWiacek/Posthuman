import * as React from 'react';
import LayoutWrapper from "./components/Layout/LayoutWrapper";
import { AuthProvider } from './Hooks/useAuth';
import { AvatarProvider } from './Hooks/useAvatar';
import { SignalRProvider } from './Hooks/useSignalR';
import './App.css';

const App = () => {
    return (
        <AuthProvider>
            <AvatarProvider>
                <SignalRProvider>
                    <div className="App">
                        <LayoutWrapper />
                    </div>
                </SignalRProvider>
            </AvatarProvider>
        </AuthProvider>
    );
}

export default App;
