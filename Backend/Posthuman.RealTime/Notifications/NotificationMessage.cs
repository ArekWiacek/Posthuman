using System;

namespace Posthuman.RealTime.Notifications
{
    public class NotificationMessage
    {
        public NotificationMessage()
        {
            Title = "";
            Subtitle = "";
            Text = "";
            SecondText = "";
            AvatarName = "";
            Reward = "";
            ShowInModal = false;
        }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Text { get; set; }

        public string SecondText { get; set; }

        public DateTime Occured { get; set;}

        public string AvatarName { get; set; }

        public string Reward { get; set; }
        
        public bool ShowInModal { get; set; }
    }
}
