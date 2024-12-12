using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Entities
{
    public class Folder : Base
    {
        public required string FolderName { get; set; }

        [ForeignKey("ParentFolderId")]
        public virtual Folder? ParentFolder { get; set; }

        public int ParentFolderId { get; set; }

        public virtual ICollection<File>? Files { get; set; }
    }
}
