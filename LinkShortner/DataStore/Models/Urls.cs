using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortner.DataStore.Models
{
    public class Urls
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
