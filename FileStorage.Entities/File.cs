using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Entities
{
    public class File : Base
    {
        public required string FileName { get; set; }

        public long FileSize { get; set; }

        public required string FileType { get; set; }

        public required string Url { get; set; }

        [ForeignKey("FolderId")]
        public virtual Folder? Folder { get; set; }

        public int? FolderId { get; set; }
    }
}
