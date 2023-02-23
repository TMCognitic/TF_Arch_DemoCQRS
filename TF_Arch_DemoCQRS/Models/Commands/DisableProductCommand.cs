using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Net.Http.Headers;
using System.Data.Common;
using Tools.Ado;
using Tools.Cqrs;
using Tools.Cqrs.Command;

namespace TF_Arch_DemoCQRS.Models.Commands
{
    public class DisableProductCommand : ICommand
    {
        public int Id { get; init; }

        public DisableProductCommand(int id)
        {
            Id = id;
        }
    }

    public class DisableProductCommandHandler : ICommandHandler<DisableProductCommand>
    {
        private DbConnection _connection;

        public DisableProductCommandHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public Result Execute(DisableProductCommand command)
        {
            try
            {
                using(_connection)
                {
                    int rows = _connection.ExecuteNonQuery("UPDATE Produit SET Actif = 0 WHERE Id = @Id AND Actif = 1;", false, command);

                    switch(rows)
                    {
                        case 0:
                            return Result.Failure("Le produit n'existe pas ou est déjà désactivé.");
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
