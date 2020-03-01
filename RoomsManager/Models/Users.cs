using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoomsManager.Models
{
    [Table("Users")]
    public class Users
    {
        #region Properties
        [Key]      
        public string UserEmail { get; set; }

        public string Password { get; set; }
        
        //[Required]
        public string UserRole { get; set; }
        public string Token { get; set; }

        //public Rooms rooms { get; set; }
        //public MesReserves mesReserves { get; set; }
        #endregion
    }
}
