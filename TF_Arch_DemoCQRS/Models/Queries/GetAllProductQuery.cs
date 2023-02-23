using System.Data.Common;
using TF_Arch_DemoCQRS.Models.Entities;
using TF_Arch_DemoCQRS.Models.Mappers;
using Tools.Ado;
using Tools.Cqrs.Queries;

namespace TF_Arch_DemoCQRS.Models.Queries
{
    public class GetAllProductQuery : IQuery<IEnumerable<Produit>>
    {
    }

    public class GetAllProductQueryHandler : IQueryHandler<GetAllProductQuery, IEnumerable<Produit>>
    {
        private readonly DbConnection _connection;

        public GetAllProductQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Produit> Execute(GetAllProductQuery query)
        {
            using (_connection)
            {
                return _connection.ExecuteReader("Select Id, Nom, [Description], Prix, DateCreation, Actif From Produit;", false, dr => dr.ToProduit(), null).ToList();
            }
        }
    }
}
