using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Entities
{
    public class Permission : Base
    {
        [ForeignKey("FileId")]
        public virtual File? File { get; set; }

        public int? FileId { get; set; }

        [ForeignKey("FolderId")]
        public virtual Folder? Folder { get; set; }

        public int? FolderId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public int UserId { get; set; }

        public AccessLevel AccessLevel { get; set; }
    }
}
