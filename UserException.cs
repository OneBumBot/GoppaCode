using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoppaCodes
{
    [Table("UserException")]
    internal class UserException
    {
        public int Id { get; set; }

        public string? Message { get; set; }

        public string? TargetSite { get; set; }

        [DisplayFormat(DataFormatString =
        "{0:dd.MM.yyyy hh:mm:ss}", ApplyFormatInEditMode =
        true)]
        public DateTime? DateTimeexc { get; set; }

        internal GoppaContext GoppaContext
        {
            get => default;
            set
            {
            }
        }
    }
}
