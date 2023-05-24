using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoppaCodes
{
    [Table("PrimitivePolynomial")]
    internal class PrimitivePolynomial
    {
        public int Id { get; set; }
        public int Value { get; set; }

    }
}
