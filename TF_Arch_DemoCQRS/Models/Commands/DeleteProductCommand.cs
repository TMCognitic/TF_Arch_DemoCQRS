using System.Data.Common;
using Tools.Ado;
using Tools.Cqrs;
using Tools.Cqrs.Command;

namespace TF_Arch_DemoCQRS.Models.Commands
{
    public class DeleteProductCommand : ICommand
    {
        public int Id { get; init; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly DbConnection _connection;

        public DeleteProductCommandHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public Result Execute(DeleteProductCommand command)
        {
            try
            {
                using (_connection)
                {
                    _connection.ExecuteNonQuery("DELETE From Produit WHERE Id = @Id", false, command);
                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
