using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumberFrame
{
    class MPerson
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool? Gender { get; set; }
        public string WorkPhoneNum { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime? WorkBegin { get; set; }
        public DateTime? WorkEnd { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
        public int ServiceId { get; set; }
        public int isBoss { get; set; }
    }
}
