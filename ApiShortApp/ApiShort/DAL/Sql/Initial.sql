USE master
GO

IF EXISTS (SELECT 1 FROM sysdatabases WHERE NAME='ShortDB')
	DROP DATABASE ShortDB
GO

CREATE DATABASE ShortDB
GO


USE [ShortDB]
GO


IF OBJECT_ID('dbo.Projects') IS NOT NULL 
    ALTER TABLE dbo.Facilities  
    DROP CONSTRAINT [FK_dbo.Facilities_dbo.Projects_ProjectId];   
    GO

	DROP TABLE dbo.Projects
GO

CREATE TABLE dbo.Projects(
    Id           INT           NOT NULL IDENTITY(1,1),
    Cipher       NVARCHAR(MAX) NOT NULL,
    [Name]       NVARCHAR(MAX) NOT NULL,
    CreationDate DATETIME      NOT NULL,
    UpdateDate   DATETIME      NOT NULL,

    CONSTRAINT [PK_dbo.Projects] PRIMARY KEY (Id),
    CONSTRAINT [C_Projects_Date] CHECK(UpdateDate >= CreationDate)    
);
GO


IF OBJECT_ID('dbo.Facilities') IS NOT NULL 
    ALTER TABLE dbo.ChildParentFacilities  
    DROP CONSTRAINT [FK_dbo.ChildParentFacilities_dbo.Facilities_ParentId];   
    GO

    ALTER TABLE dbo.ChildParentFacilities  
    DROP CONSTRAINT [FK_dbo.ChildParentFacilities_dbo.Facilities_ChildId];   
    GO

	DROP TABLE dbo.Facilities
GO

CREATE TABLE dbo.Facilities(
    Id            INT           NOT NULL IDENTITY(1,1),
    Code          NVARCHAR(MAX) NOT NULL,
    [Name]        NVARCHAR(MAX) NOT NULL,
    CreationDate  DATETIME      NOT NULL,
    UpdateDate    DATETIME      NOT NULL,
    ProjectId     INT           NOT NULL,

    CONSTRAINT [PK_dbo.Facilities] PRIMARY KEY(Id),
  
    CONSTRAINT [FK_dbo.Facilities_dbo.Projects_ProjectId] 
    FOREIGN KEY(ProjectId) REFERENCES dbo.Projects(Id) ON DELETE CASCADE
);
GO

IF OBJECT_ID('dbo.ChildParentFacilities') IS NOT NULL 
	DROP TABLE dbo.ChildParentFacilities
GO

CREATE TABLE dbo.ChildParentFacilities(
    ParentId INT NOT NULL,
    ChildId  INT NOT NULL,

    CONSTRAINT [PK_dbo.ChildParentFacilities] 
    PRIMARY KEY(ParentId,ChildId),

    CONSTRAINT [FK_dbo.ChildParentFacilities_dbo.Facilities_ParentId] 
    FOREIGN KEY(ParentId) REFERENCES dbo.Facilities(Id) ON DELETE CASCADE,

    CONSTRAINT [FK_dbo.ChildParentFacilities_dbo.Facilities_ChildId] 
    FOREIGN KEY(ChildId) REFERENCES dbo.Facilities(Id)
);
GO

CREATE INDEX IX_ParentId ON ChildParentFacilities(ParentId);
GO

CREATE INDEX IX_ChildId ON ChildParentFacilities(ChildId);
GO



ALTER TABLE dbo.Projects NOCHECK CONSTRAINT ALL
GO

INSERT dbo.Projects (Id, Cipher, [Name], CreationDate, UpdateDate)
VALUES (1,'AQWJV','Мечта', '19961023','20001023')
    ,(2,'QGJNB','Зенит','20011023','20061123')
    ,(3,'GHDEP','Стрела','20031023','20041123')
GO

ALTER TABLE dbo.Projects CHECK CONSTRAINT ALL
GO


ALTER TABLE dbo.Facilities NOCHECK CONSTRAINT ALL
GO

INSERT dbo.Facilities (Id, Code, [Name], CreationDate, UpdateDate, ProjectId)
VALUES (1,'ALFKI','Кустовая площадка','19961023','20001023', 1)
    ,(2,'ANATR','Нефтепровод','20011023','20011023', 1)
    ,(3,'AROUT','Автодорога','20031023','20041123', 2)
    ,(4,'DSDFD','Дренажная ёмкость','19961023','20001023', 1)
    ,(5,'UOIUO','Прожекторная мачта','20011023','20011023', 2)
    ,(6,'GHHJJ','Забор','20031023','20041023', 2)
    ,(7,'FGHTY','Столб','20051023','20061023', 2)
GO

ALTER TABLE dbo.Facilities CHECK CONSTRAINT ALL
GO


ALTER TABLE dbo.ChildParentFacilities NOCHECK CONSTRAINT ALL
GO

INSERT dbo.ChildParentFacilities(ParentId, ChildId)
VALUES (1, 4)
    ,(1, 5)
    ,(2, 5)
    ,(2, 6)
    ,(3, 7)

ALTER TABLE dbo.ChildParentFacilities CHECK CONSTRAINT ALL
GO