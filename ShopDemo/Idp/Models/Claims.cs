
using System.ComponentModel.DataAnnotations;


namespace Idp.Models
{
    public class Claims
    {
        [MaxLength(36)]
        public int ClaimsId { get; set; }

        [MaxLength(32)]
        public string Type { get; set; }

        [MaxLength(50)]
        public string Value { get; set; }

        public virtual User User { get; set; }
    }
}
