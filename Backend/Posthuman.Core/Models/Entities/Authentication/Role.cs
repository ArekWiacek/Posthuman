using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posthuman.Core.Models.Entities.Authentication
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
