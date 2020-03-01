using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoomsManager.Models
{
    [Table("MesReserves")]
    public class MesReserves
    {
        #region Propriétés
        [Key]
        public int MesReservesId { get; set; }
        /// <summary>
        /// 
        /// </summary>
       // public int RoomsId { get; set; }
        #endregion
    }
}
