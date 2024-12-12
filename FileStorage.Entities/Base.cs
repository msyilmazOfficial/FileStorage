using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Entities
{
    public abstract class Base
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CreateUserId")]
        public virtual User? CreateUser { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("UpdateUserId")]
        public virtual User? UpdateUser { get; set; }

        public int UpdateUserId { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
