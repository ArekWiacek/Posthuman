import * as React from 'react';
import { useState, useEffect, createContext, useMemo } from 'react';
import LayoutWrapper from "./components/Layout/LayoutWrapper";
import { LogT, LogI, LogW } from './Utilities/Utilities';
import { ApiGet } from './Utilities/ApiRepository';
import './App.css';

export const AvatarContext = createContext({
  activeAvatar: {},
  setActiveAvatar: () => { }
});

const App = () => {
  LogT("App.Constructor");

  const [activeAvatar, setActiveAvatar] = useState({
    id: 0,
    name: "[No active user]"
  });

  const activeAvatarValue = useMemo(
    () => ({ activeAvatar, setActiveAvatar }),
    [activeAvatar]
  );

  useEffect(() => {
    LogT("App.useEffect");
    LogI("App.ApiCall.Avatars.GetActiveAvatar");

    ApiGet("Avatars/GetActiveAvatar", data => {
      if (data !== undefined && data.id !== undefined && data.id !== 0) {
        var avatar = data;
        LogW("Calling 'setActiveAvatar' with Avatar of ID: " + avatar.id);
        setActiveAvatar(avatar);
      }
    });
  }, [])

  return (
    <AvatarContext.Provider value={activeAvatarValue}>
      <div className="App">
        <LayoutWrapper />
      </div>
    </AvatarContext.Provider>
  );
}

export default App;
