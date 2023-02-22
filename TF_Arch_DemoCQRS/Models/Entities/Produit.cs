namespace TF_Arch_DemoCQRS.Models.Entities
{
    public class Produit
    {
        public int Id { get; init; }
        public string Nom { get; init; }
        public string Description { get; init; }
        public double Prix { get; init; }
        public DateTime DateCreation { get; init; }
        public bool Actif { get; init; }
        
        public Produit(int id, string nom, string description, double prix, DateTime dateCreation, bool actif)
        {
            Id = id;
            Nom = nom;
            Description = description;
            Prix = prix;
            DateCreation = dateCreation;
            Actif = actif;
        }
    }
}
