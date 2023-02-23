using System.Data.Common;
using Tools.Ado;
using Tools.Cqrs;
using Tools.Cqrs.Command;

namespace TF_Arch_DemoCQRS.Models.Commands
{
    public class CreateProductCommand : ICommand
    {
        public string Nom { get; init; }
        public string Description { get; init; }
        public double Prix { get; init; }
        public CreateProductCommand(string nom, string description, double prix)
        {
            Nom = nom;
            Description = description;
            Prix = prix;
        }
    }

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly DbConnection _connection;

        public CreateProductCommandHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public Result Execute(CreateProductCommand command)
        {
            try
            {
                using (_connection)
                {
                    int rows = _connection.ExecuteNonQuery("INSERT INTO Produit (Nom, [Description], Prix) VALUES (@Nom, @Description, @Prix)", false, command);

                    if(rows == 1) 
                    {
                        return Result.Success();
                    }

                    return Result.Failure("Something wrong....");
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
