import React, { useContext, useState} from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { TextField, MenuItem } from '@mui/material';
import Api from '../../Utilities/ApiHelper';

// This component allows to select current user from dropdown (stub until authentication is implemented)
const SelectAvatar = ({ isMini }) => {
    //const { activeAvatar, setActiveAvatar } = useContext(AvatarContext);
    //const [avatars, setAvatars] = useState([]);


    const handleAvatarChange = (event) => {
        var selectedId = event.target.value;

    };

    React.useEffect(() => {
        //Api.Get("Avatars", data => setAvatars(data));
    }, []);

    const getAvatarSelector = () => {
        return <React.Fragment /> 
        //(
            //<TextField id="select-avatar" select fullWidth label="Avatar">
                // value={activeAvatar.id} onChange={handleAvatarChange}>
                // {avatars.map((avatar) => (
                //    <MenuItem key={avatar.id} value={avatar.id}>
                //        {avatar.name}
                //    </MenuItem>
                //))}
            //</TextField>
        //);
    };

    return (getAvatarSelector());
}

export default SelectAvatar;