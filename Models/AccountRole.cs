using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API2.Models
{
    [Table("TB_M_AccountRole")]
    public class AccountRole
    {
        [ForeignKey("Account")]
        public string NIK { get; set; }

        [ForeignKey("Role")]
        public int Role_Id { get; set; }


        [JsonIgnore]
        public virtual Account Account { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
