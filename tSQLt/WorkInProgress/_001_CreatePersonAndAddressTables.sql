
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='Person')
BEGIN
	DROP TABLE [Person]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='Address')
BEGIN
	DROP TABLE [Address]
END
GO

CREATE TABLE [Address]
(
	AddressID int identity(1,1) not null,
	AddressLine1 varchar(200) not null,
	AddressLine2 varchar(200) null,
	City varchar(200) null,
	PostCode varchar(20) not null,
	Country varchar(100) not null,
	CONSTRAINT PK_Address PRIMARY KEY (AddressID)
)
GO


CREATE TABLE Person
(
	PersonID int identity(1,1) not null,
	FirstName varchar(100) not null,
	Surname varchar(100) not null,
	IDNumber varchar(20) not null,
	Email varchar(200) not null,
	DateOfBirth datetime not null,
	ResidentialAddressID int not null,
	PostalAddressID int not null,
	CONSTRAINT PK_Person PRIMARY KEY (PersonID),
	CONSTRAINT FK_ResidentialAddress FOREIGN KEY (ResidentialAddressID) REFERENCES [Address](AddressID),
	CONSTRAINT FK_PostalAddress FOREIGN KEY (PostalAddressID) REFERENCES [Address](AddressID)
)
