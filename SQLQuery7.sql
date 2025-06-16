CREATE TABLE [dbo].[GymnastClasses] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [GymnastId] NCHAR(9) NOT NULL,
    [ClassId] INT NOT NULL,
    CONSTRAINT [PK_GymnastClasses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GymnastClasses_Gymnast] FOREIGN KEY ([GymnastId]) REFERENCES [dbo].[Gymnast] ([Id]),
    CONSTRAINT [FK_GymnastClasses_Class] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[StudioClasses] ([Id])
);
