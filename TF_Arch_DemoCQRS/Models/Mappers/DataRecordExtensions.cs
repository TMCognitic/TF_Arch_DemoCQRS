using System.Data;
using TF_Arch_DemoCQRS.Models.Entities;

namespace TF_Arch_DemoCQRS.Models.Mappers
{
    internal static class DataRecordExtensions
    {
        internal static Produit ToProduit(this IDataRecord record)
        {
            return new Produit(
                    (int)record["Id"],
                    (string)record["Nom"],
                    (string)record["Description"],
                    (double)record["Prix"],
                    (DateTime)record["DateCreation"],
                    (bool)record["Actif"]
                );
        }
    }
}
