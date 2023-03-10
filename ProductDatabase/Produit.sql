CREATE TABLE [dbo].[Produit]
(
	[Id] INT NOT NULL IDENTITY,
	[Nom] NVARCHAR(128) NOT NULL,
	[Description] NVARCHAR(600) NOT NULL,
	[Prix] FLOAT NOT NULL,
	[DateCreation] DATETIME2(7) NOT NULL
		CONSTRAINT DF_Produit_DateCreation DEFAULT(SYSDATETIME()),
    [Actif] BIT NOT NULL
		CONSTRAINT DF_Produit_Actif DEFAULT(1), 
    CONSTRAINT [PK_Produit] PRIMARY KEY ([Id]) 
);
