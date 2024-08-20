using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class EncryptionKey
    {
        [Key]
        public int EncryptionKeyId { get; set; }

        public int DataId { get; set; }
        [ForeignKey("DataId")]
        public Data Data { get; set; }

        [Required]
        public string Key { get; set; }


    }
}
