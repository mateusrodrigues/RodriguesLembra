using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RodriguesLembra.Models
{
    public class Realm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RealmID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }

        /* Navigation Properties */
        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Todo> Todos { get; set; }
    }
}