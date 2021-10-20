import * as React from 'react';
import './App.css';
import { useState, createContext, useMemo } from 'react';
import LayoutWrapper from "./components/Layout/LayoutWrapper";
import axios from 'axios';

import { ApiUrl, LogT, LogI, LogW, LogE } from './Utilities/Utilities';

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

  React.useEffect(() => {
    LogT("App.useEffect");
    LogI("App.ApiCall.Avatars.GetActiveAvatar");

    axios
      .get(ApiUrl + "Avatars/GetActiveAvatar")
      .then(response => {
        LogI("Active Avatar obtained: ");
        LogI(response.data);

        if (response != undefined && response.data != undefined &&
          response.data.id != undefined && response.data.id != 0) {
          var avatar = response.data;
          LogW("Calling 'setActiveAvatar' with Avatar of ID: " + avatar.id);
          setActiveAvatar(avatar);
        }
      })
      .catch(error => {
        LogE("Error occured when obtaining Avatar: ", error);
      })
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
