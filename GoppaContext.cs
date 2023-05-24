using Microsoft.EntityFrameworkCore;
namespace GoppaCodes
{
    internal class GoppaContext:DbContext
    {

        public DbSet<UserException> UserExceptions { get; set; }
        public DbSet<EncodedMessage> EncodedMessages { get; set; }
        public DbSet<DecodedMessage> DecodedMessages { get; set; }
        public DbSet<PrimitivePolynomial> primitivePolynomials { get; set; }
        public EncForm EncForm
        {
            get => default;
            set
            {
            }
        }

        public DecForm DecForm
        {
            get => default;
            set
            {
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GoppaCodes;Username=postgres;Password=admin");
        }

    }
}
