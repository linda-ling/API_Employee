using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API2.Models
{
    [Table("TB_M_University")]
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }


        [JsonIgnore]
        public virtual ICollection<Education> Education { get; set; }
    }
}
