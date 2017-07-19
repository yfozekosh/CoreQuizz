IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Users] (
    [Email] nvarchar(450) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [Id] int NOT NULL,
    [MiddleName] nvarchar(max),
    [ModifieDateTime] datetime2 NOT NULL,
    [Name] nvarchar(max),
    [PasswordHash] nvarchar(max),
    [Salt] nvarchar(max),
    [SecondName] nvarchar(max),
    CONSTRAINT [PK_Users] PRIMARY KEY ([Email]),
    CONSTRAINT [AK_Users_Id] UNIQUE ([Id])
);

GO

CREATE TABLE [Surveys] (
    [Id] int NOT NULL IDENTITY,
    [CreatedByEmail] nvarchar(450),
    [CreatedDate] datetime2 NOT NULL,
    [ModifieDateTime] datetime2 NOT NULL,
    [Title] nvarchar(max),
    CONSTRAINT [PK_Surveys] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Surveys_Users_CreatedByEmail] FOREIGN KEY ([CreatedByEmail]) REFERENCES [Users] ([Email]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Questions] (
    [Id] int NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [ModifieDateTime] datetime2 NOT NULL,
    [QuestionLabel] nvarchar(max),
    [QuestionType] int NOT NULL,
    [ResultId] int,
    [SurveyId] int,
    [Value] nvarchar(max),
    CONSTRAINT [PK_Questions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Questions_Surveys_SurveyId] FOREIGN KEY ([SurveyId]) REFERENCES [Surveys] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [QuestionOptions] (
    [Id] int NOT NULL IDENTITY,
    [CheckboxQuestionId] int,
    [CreatedDate] datetime2 NOT NULL,
    [IsSelected] bit,
    [ModifieDateTime] datetime2 NOT NULL,
    [QuestionId] int,
    [RadioQuestionId] int,
    [Value] nvarchar(max),
    CONSTRAINT [PK_QuestionOptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_QuestionOptions_Questions_CheckboxQuestionId] FOREIGN KEY ([CheckboxQuestionId]) REFERENCES [Questions] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_QuestionOptions_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_QuestionOptions_Questions_RadioQuestionId] FOREIGN KEY ([RadioQuestionId]) REFERENCES [Questions] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Questions_SurveyId] ON [Questions] ([SurveyId]);

GO

CREATE INDEX [IX_QuestionOptions_CheckboxQuestionId] ON [QuestionOptions] ([CheckboxQuestionId]);

GO

CREATE INDEX [IX_QuestionOptions_QuestionId] ON [QuestionOptions] ([QuestionId]);

GO

CREATE INDEX [IX_QuestionOptions_RadioQuestionId] ON [QuestionOptions] ([RadioQuestionId]);

GO

CREATE INDEX [IX_Surveys_CreatedByEmail] ON [Surveys] ([CreatedByEmail]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170719090518_SurveyModel', N'1.1.2');

GO


