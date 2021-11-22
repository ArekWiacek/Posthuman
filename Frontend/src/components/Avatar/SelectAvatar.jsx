import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { TextField, MenuItem } from '@mui/material';
import { AvatarContext } from '../../App';
import Api from '../../Utilities/ApiHelper';

// This component allows to select current user from dropdown (stub until authentication is implemented)
const SelectAvatar = ({ isMini }) => {
    const { activeAvatar, setActiveAvatar } = React.useContext(AvatarContext);
    const [avatars, setAvatars] = React.useState([]);


    const handleAvatarChange = (event) => {
        var selectedId = event.target.value;

        Api.Put("Avatars/SetActiveAvatar", selectedId, null, data => {
            // Avatar set as "Active" in backend - now do the same change in frontend state
            var selectedAvatar = avatars.find(av => {
                return av.id === selectedId;
            });
            // selectedAvatar.isSelected = true;
            setActiveAvatar(selectedAvatar);
        });
    };

    React.useEffect(() => {
        Api.Get("Avatars", data => setAvatars(data));
    }, []);

    const getAvatarSelector = () => {
        return (
            <TextField id="select-avatar" select fullWidth label="Avatar"
                value={activeAvatar.id} onChange={handleAvatarChange}>
                {avatars.map((avatar) => (
                    <MenuItem key={avatar.id} value={avatar.id}>
                        {avatar.name}
                    </MenuItem>
                ))}
            </TextField>
        );
    };

    return (getAvatarSelector());

        // <Card sx={{ p: isMini ? 0 : 2, display: 'flex', flexDirection: 'column' }}>
        //     <CardContent>
        //         <Typography sx={{ mb: 1.5 }} color="text.secondary">
        //             Selected Avatar
        //         </Typography>
        //         <Typography sx={{ mb: 3 }} variant="h5" component="div">
        //             {activeAvatar ? activeAvatar.name : "[Not set]"}
        //         </Typography>
        //         <TextField id="select-avatar" select fullWidth label="Avatar"
        //             value={activeAvatar.id} onChange={handleAvatarChange}>
        //             {avatars.map((avatar) => (
        //                 <MenuItem key={avatar.id} value={avatar.id}>
        //                     {avatar.name}
        //                 </MenuItem>
        //             ))}
        //         </TextField>
        //     </CardContent>
        // </Card>
    //);
}

export default SelectAvatar;