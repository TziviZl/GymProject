CREATE TABLE [dbo].[StudioClasses] (
    [Id]                  INT        NOT NULL,
    [Name]                NVARCHAR(50) NOT NULL,
    [Level]               NCHAR (1)  NOT NULL,
    [Price]               INT        NOT NULL,
    [Date]                DATETIME   NOT NULL,
    [Participants_number] INT        NOT NULL,
    [TrainerId]           NCHAR (9)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Table_Trainer] FOREIGN KEY ([TrainerId]) REFERENCES [dbo].[Trainer] ([Id])
);

