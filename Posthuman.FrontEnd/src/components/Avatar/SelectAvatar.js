import * as React from 'react';

import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { TextField, MenuItem } from '@mui/material';

import { ApiUrl, LogT, LogI, LogW, LogE } from '../../Utilities/Utilities';
import { AvatarContext } from '../../App';
import { ApiGet, ApiPut } from '../../Utilities/ApiRepository';

// Component allows to select current user (stub until authentication is implemented)
const SelectAvatar = () => {
    LogT("SelectAvatar.Constructor");

    const { activeAvatar, setActiveAvatar } = React.useContext(AvatarContext);
    const [avatars, setAvatars] = React.useState([]);

    const handleAvatarChange = (event) => {
        LogT("SelectAvatar.handleAvatarChange - ID: " + event.target.value);

        var selectedId = event.target.value;

        ApiPut("Avatars/SetActiveAvatar", selectedId, null, data => {
            // Avatar set as "Active" in backend - now do the same change in frontend state
            var selectedAvatar = avatars.find(av => {
                return av.id === selectedId;
            });
            // selectedAvatar.isSelected = true;
            setActiveAvatar(selectedAvatar);
        });
    };

    React.useEffect(() => {
        ApiGet("Avatars", data => setAvatars(data));
    }, [])

    return (
        <Card sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
            <CardContent>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                    Selected Avatar
                </Typography>
                <Typography sx={{ mb: 3 }} variant="h5" component="div">
                    {activeAvatar ? activeAvatar.name : "[Not set]"}
                </Typography>
                <TextField id="select-avatar" select fullWidth label="Avatar" 
                    value={activeAvatar.id} onChange={handleAvatarChange}>
                    {avatars.map((avatar) => (
                        <MenuItem key={avatar.id} value={avatar.id}>
                            {avatar.name}
                        </MenuItem>
                    ))}
                </TextField>
            </CardContent>
        </Card>
    );
}

export default SelectAvatar;