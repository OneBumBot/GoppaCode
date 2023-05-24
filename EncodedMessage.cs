using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoppaCodes
{
    [Table("EncodedMessage")]
    internal class EncodedMessage
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString =
        "{0:dd.MM.yyyy hh:mm:ss}", ApplyFormatInEditMode =
        true)]
        public DateTime Date { get; set; }

        public int Polynomial { get; set; }
        public int[] Coefficients { get; set; }
        public int[] Primitives { get; set; }
        public string Path { get; set; }

        internal GoppaContext GoppaContext
        {
            get => default;
            set
            {
            }
        }
    }
}
