using System.Data.Common;
using TF_Arch_DemoCQRS.Models.Entities;
using TF_Arch_DemoCQRS.Models.Mappers;

namespace TF_Arch_DemoCQRS.Models.Queries
{
    public class GetOneProductQuery
    {
        public int Id { get; init; }

        public GetOneProductQuery(int id)
        {
            Id = id;
        }
    }

    public class GetOneProductQueryHandler
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
                using (DbCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "Select Id, Nom, [Description], Prix, DateCreation, Actif From Produit Where Id = @Id;";
                    DbParameter idParameter = command.CreateParameter();
                    idParameter.ParameterName = "Id";
                    idParameter.Value = query.Id;
                    command.Parameters.Add(idParameter);

                    _connection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return reader.ToProduit();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}
