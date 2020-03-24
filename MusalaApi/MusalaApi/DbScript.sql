IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Gateway] (
    [SerialNumber] nvarchar(20) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [IPAddress] nvarchar(15) NULL,
    CONSTRAINT [PK_Gateway] PRIMARY KEY ([SerialNumber])
);

GO

CREATE TABLE [Device] (
    [DeviceId] int NOT NULL IDENTITY,
    [Vendor] nvarchar(50) NOT NULL,
    [Date] datetime2 NOT NULL,
    [Status] nvarchar(7) NOT NULL,
    [SerialNumber] nvarchar(20) NULL,
    CONSTRAINT [PK_Device] PRIMARY KEY ([DeviceId]),
    CONSTRAINT [FK_Device_Gateway_SerialNumber] FOREIGN KEY ([SerialNumber]) REFERENCES [Gateway] ([SerialNumber]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Device_SerialNumber] ON [Device] ([SerialNumber]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200323204451_InitialDatabase', N'2.1.14-servicing-32113');

GO

