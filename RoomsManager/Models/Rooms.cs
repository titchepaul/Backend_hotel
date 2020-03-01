using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoomsManager.Models
{
    [Table("Rooms")]
    public class Rooms
    {
        #region Propriétés
        [Key]
        public int RoomsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NbrePieces { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Enfants{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Adulte { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Prix { get; set; }
        /// <summary>
        /// 
        /// </summary>
        
        public string UserEmail { get; set; }

       // public MesReserves mesReserves{ get; set; }

        #endregion

    }
}
