using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Posthuman.Core.Models.Entities
{
    /// <summary>
    /// This class represents game player character (= hero, user profile)
    /// </summary>
    [Table("Avatars")]
    public class Avatar : IEntity
    {
        public Avatar()
        {
            Name = "";
            Bio = "";
            CreationDate = DateTime.Now;
            Level = 1;
            Exp = 0;
            CybertribeName = "Polaczki";
        }

        public int Id { get; set; }
        
        // Owner user - this is connection between authenticated asp.net user and it's avatar in game
        public int UserId { get; set; }
        public User User { get; set; }


        public string Name { get; set; }            // User nickname
        public string Bio { get; set; }             // "About" user
        public DateTime CreationDate { get; set; }  // Date when user created account        


        public int Level { get; set; }              // Avatar level - it grows when gaining experience
        public int Exp { get; set; }                // Experience points
        public int ExpToNewLevel { get; set; }      // When new level will be reached 
        public int ExpToCurrentLevel { get; set; }


        // Owned entities
        [JsonIgnore]
        public virtual ICollection<TodoItem> TodoItems { get; set; }
        [JsonIgnore]
        public virtual ICollection<Habit> Habits { get; set; }


        // NOT YET SURE... - TEST PROPERTIES :)
        // Hacker
        // Wirus
        // Sztuczna inteligencja
        // Cyborg itd...
        public string CybertribeName { get; set; }
    }
}
