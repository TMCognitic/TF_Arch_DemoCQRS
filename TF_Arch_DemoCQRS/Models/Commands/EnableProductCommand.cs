using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Net.Http.Headers;
using System.Data.Common;
using Tools.Ado;
using Tools.Cqrs;
using Tools.Cqrs.Command;

namespace TF_Arch_DemoCQRS.Models.Commands
{
    public class EnableProductCommand : ICommand
    {
        public int Id { get; init; }

        public EnableProductCommand(int id)
        {
            Id = id;
        }
    }

    public class EnableProductCommandHandler : ICommandHandler<EnableProductCommand>
    {
        private DbConnection _connection;

        public EnableProductCommandHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public Result Execute(EnableProductCommand command)
        {
            try
            {
                using(_connection)
                {
                    int rows = _connection.ExecuteNonQuery("UPDATE Produit SET Actif = 1 WHERE Id = @Id AND Actif = 0;", false, command);

                    switch(rows)
                    {
                        case 0:
                            return Result.Failure("Le produit n'existe pas ou est déjà réactivé.");
                        case 1:
                            return Result.Success();
                        default:
                            return Result.Failure("Something Wrong");
                    }
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
