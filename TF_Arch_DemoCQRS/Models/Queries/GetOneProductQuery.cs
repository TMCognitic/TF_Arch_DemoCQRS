using System.Data.Common;
using TF_Arch_DemoCQRS.Models.Entities;
using TF_Arch_DemoCQRS.Models.Mappers;
using Tools.Ado;
using Tools.Cqrs.Queries;

namespace TF_Arch_DemoCQRS.Models.Queries
{
    public class GetOneProductQuery : IQuery<Produit?>
    {
        public int Id { get; init; }

        public GetOneProductQuery(int id)
        {
            Id = id;
        }
    }

    public class GetOneProductQueryHandler : IQueryHandler<GetOneProductQuery, Produit?>
    {
        private readonly DbConnection _connection;

        public GetOneProductQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public Produit? Execute(GetOneProductQuery query)
        {
            using (_connection)
            {
                return _connection.ExecuteReader("Select Id, Nom, [Description], Prix, DateCreation, Actif From Produit Where Id = @Id;", false, dr => dr.ToProduit(), query).SingleOrDefault();                
            }
        }
    }
}
