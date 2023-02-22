using System.Data.Common;
using TF_Arch_DemoCQRS.Models.Entities;
using TF_Arch_DemoCQRS.Models.Mappers;
using Tools.Cqrs.Queries;

namespace TF_Arch_DemoCQRS.Models.Queries
{
    public class GetAllProductQuery : IQuery
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
                using (DbCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "Select Id, Nom, [Description], Prix, DateCreation, Actif From Produit;";

                    _connection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader.ToProduit();
                        }
                    }
                }
            }
        }
    }

}
