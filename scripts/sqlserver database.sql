
CREATE TABLE [dbo].[DataTypes](
	[Id] [UNIQUEIDENTIFIER] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[UseLength] [bit] NOT NULL,
	CONSTRAINT [PkDataType] PRIMARY KEY([Id])
)

CREATE TABLE [dbo].[Entities](
	[Id] [UNIQUEIDENTIFIER] NOT NULL DEFAULT newsequentialid(),
	[Name] [nvarchar](128) NOT NULL,
	CONSTRAINT [PkEntity] PRIMARY KEY([Id])
)

CREATE TABLE [dbo].[Attributes](
	[Id] [UNIQUEIDENTIFIER] NOT NULL DEFAULT newsequentialid(),
	[EntityId] [UNIQUEIDENTIFIER] NOT NULL,
	[DataTypeId] [UNIQUEIDENTIFIER] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[AllowNull] [bit] NOT NULL,
	[Length] [int] NULL,
	[GenericType] [nvarchar](256) NULL,
	CONSTRAINT [PkAttribute] PRIMARY KEY([Id]),
	CONSTRAINT [FkAttribute_Entity] FOREIGN KEY([EntityId]) references Entities([Id]),
	CONSTRAINT [FkAttribute_DataType] FOREIGN KEY([DataTypeId]) references DataTypes([Id])
)

CREATE TABLE [dbo].[Languages](
	[Id] [UNIQUEIDENTIFIER] NOT NULL DEFAULT newsequentialid(),
	[Name] [nvarchar](128) NOT NULL,
	CONSTRAINT [PkLanguage] PRIMARY KEY([Id])
)

CREATE TABLE [dbo].[LanguagesDataTypes](
	[Id] [UNIQUEIDENTIFIER] NOT NULL DEFAULT newsequentialid(),
	[LanguageId] [UNIQUEIDENTIFIER] NOT NULL,
	[DataTypeId] [UNIQUEIDENTIFIER] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[NameNullable] [nvarchar](128) NULL,
	[Format] [nvarchar](128) NULL,
	CONSTRAINT [PkLanguagesDataType] PRIMARY KEY([Id]),
	CONSTRAINT [FkLanguagesDataType_Language] FOREIGN KEY([LanguageId]) references Languages([Id]),
	CONSTRAINT [FkLanguagesDataType_DataType] FOREIGN KEY([DataTypeId]) references DataTypes([Id])
)



INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c40',N'guid', 0)
INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c41',N'int', 0)
INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c42',N'long', 0)
INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c43',N'string', 1)
INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c44',N'bool', 0)
INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c45',N'date', 0)
INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c46',N'datetime', 0)
INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c47',N'decimal', 1)
INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c48',N'object<>', 0)
INSERT [dbo].[DataTypes] ([Id], [Name], [UseLength]) VALUES ('15ab1963-512d-4e89-8d1a-8b7980534c49',N'array<>', 0)



INSERT [dbo].[Languages] ([Id], [Name]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', N'CSharp')
INSERT [dbo].[Languages] ([Id], [Name]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f92', N'Swagger Doc')

INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c40', N'Guid', N'Guid?', NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c41', N'int', N'int?', NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c42', N'long', N'long?', NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c43', N'string', NULL, NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c44', N'bool', N'bool?', NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c45', N'DateTime', N'DateTime?', NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c46', N'DateTime', N'DateTime?', NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c47', N'decimal', N'decimal?', NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c48', N'object', NULL, NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ('19e2b0de-df22-468a-9ba3-33d56bf38f91', '15ab1963-512d-4e89-8d1a-8b7980534c49', N'array', NULL, NULL)

INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c40', N'string', NULL, N'uuid')
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c41', N'integer', NULL, N'int32')
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c42', N'integer', NULL, N'int64')
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c43', N'string', NULL, N'string')
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c44', N'boolean', NULL, N'boolean')
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c45', N'string', NULL, N'date')
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c46', N'string', NULL, N'date-time')
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c47', N'number', NULL, N'float')
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c48', N'object', NULL, NULL)
INSERT [dbo].[LanguagesDataTypes] ( [LanguageId], [DataTypeId], [Name], [NameNullable], [Format]) VALUES ( '19e2b0de-df22-468a-9ba3-33d56bf38f92', '15ab1963-512d-4e89-8d1a-8b7980534c49', N'array', NULL, NULL)

