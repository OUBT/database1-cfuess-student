CREATE TABLE [dbo].[Customer] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (100) NOT NULL,
    [LastName]  NVARCHAR (100) NOT NULL,
    [Age]       INT            DEFAULT ((15)) NOT NULL,
    [City]      NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Product] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    [Qty]  INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Order] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [CustomerId] INT      NOT NULL,
    [OrderDate]  DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_ToCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);
GO

CREATE TABLE [dbo].[OrderProduct] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [OrderId]   INT NOT NULL,
    [ProductId] INT NOT NULL,
    [Qty]       INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderProduct_ToOrder] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_OrderProduct_ToProduct] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);
GO


