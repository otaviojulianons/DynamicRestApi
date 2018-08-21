

CREATE TABLE [dbo].[DataTypes](
	[Id] [bigint] NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] [nvarchar](128) NOT NULL,
	[UseLength] [bit] NOT NULL
) 

insert into DataTypes(Name,UseLength) values('int',0)
insert into DataTypes(Name,UseLength) values('long',0)
insert into DataTypes(Name,UseLength) values('string',1)
insert into DataTypes(Name,UseLength) values('bool',0)
insert into DataTypes(Name,UseLength) values('date',0)
insert into DataTypes(Name,UseLength) values('datetime',0)
insert into DataTypes(Name,UseLength) values('decimal',1)


--drop table [dbo].[Entities]

CREATE TABLE [dbo].[Entities](
	[Id] [bigint] NOT NULL IDENTITY(1,1),
	[Name] [nvarchar](128) NOT NULL,
	[Area] [nvarchar](128) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

--drop table [Attributes]

CREATE TABLE [dbo].[Attributes](
	[Id] [bigint] NOT NULL IDENTITY(1,1),
	[EntityId] [bigint] NOT NULL,
	[DataTypeId] [bigint] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[AllowNull] [bit] NOT NULL,
	[PrimaryKey] [bit] NOT NULL,
	[ForeignKey] [bit] NOT NULL,
	[Length] [int] NULL,
	[ForeignEntity] [varchar](128) NULL,
	[ForeignAttribute] [varchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Attributes]  WITH CHECK ADD  CONSTRAINT [FkDataTypeAttribute] FOREIGN KEY([DataTypeId])
REFERENCES [dbo].[DataTypes] ([Id])
GO

ALTER TABLE [dbo].[Attributes] CHECK CONSTRAINT [FkDataTypeAttribute]
GO

ALTER TABLE [dbo].[Attributes]  WITH CHECK ADD  CONSTRAINT [FkEntityAttribute] FOREIGN KEY([EntityId])
REFERENCES [dbo].[Entities] ([Id])
GO

ALTER TABLE [dbo].[Attributes] CHECK CONSTRAINT [FkEntityAttribute]
GO
