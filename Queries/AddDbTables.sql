CREATE TABLE [dbo].[Arrivals] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [ArrivalTime] DATETIME NOT NULL,
    [QrCode]      INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Students] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (255) NOT NULL,
    [Address] VARCHAR (255) NOT NULL,
    [QrCode]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

/* Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KnockKnockLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
