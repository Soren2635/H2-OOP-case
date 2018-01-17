IF exists(SELECT * FROM sys.Databases WHERE NAME = 'BankDB')
BEGIN
	USE MASTER
	DROP DATABASE BankDB
END
GO

CREATE DATABASE BankDB
GO
USE BankDB
GO

CREATE TABLE Kunde
(
   KundeNr INT PRIMARY KEY IDENTITY,
   Fornavn NVARCHAR(50) NOT NULL,
   Efternavn NVARCHAR(50) NOT NULL,
   Adresse NVARCHAR(50) NOT NULL,
   FK_PostNr INT NOT NULL,
   Oprettelsesdato DATE NOT NULL
   CHECK (Oprettelsesdato <= GetDate())
)

CREATE TABLE Konto
(
   KontoNr INT PRIMARY KEY IDENTITY,
   FK_KundeNr INT NOT NULL,
   FK_KontoTypeID INT NOT NULL,
   Saldo DECIMAL NOT NULL,
   Oprettelsesdato DATE NOT NULL 
   CHECK (Oprettelsesdato <= GetDate())
)

CREATE TABLE Transaktioner
(
	TransaktionsID INT PRIMARY KEY,
	Beløb INT NOT NULL,
	FK_KontoNr INT NOT NULL,
	Dato DATE NOT NULL 
	CHECK (Dato <= GetDate())
)

CREATE TABLE [Postnummer-by]
(
	PostNr INT PRIMARY KEY,
	ByNavn NVARCHAR(100) NOT NULL
)

CREATE TABLE Kontotype
(
	ID INT PRIMARY KEY,
	KontoType NVARCHAR(20) NOT NULL,
	Rente DECIMAL NOT NULL
)

ALTER TABLE Kunde
ADD FOREIGN KEY (FK_PostNr) REFERENCES [Postnummer-by](PostNr);

ALTER TABLE Konto
ADD FOREIGN KEY (FK_KundeNr) REFERENCES Kunde(KundeNr);

ALTER TABLE Konto
ADD FOREIGN KEY (FK_KontoTypeID) REFERENCES Kontotype(ID);

ALTER TABLE Transaktioner
ADD FOREIGN KEY (FK_KontoNr) REFERENCES Konto(KontoNr);


BULK
INSERT [Postnummer-by]
FROM 'C:\Users\Tec\Desktop\postnummer-by.csv'
WITH ( FIELDTERMINATOR = ';', ROWTERMINATOR ='\n')

INSERT INTO Kunde
VALUES
('Lars', ' Larsen', 'Ballerup Telegrafvej 1', 2750, '2018-01-16'),
('Simon', 'Simonsen', 'Simonsvej', 2000, '2016-06-16'),
('Rasmus', 'Rasmussen', 'Hørsholm', 2970, '2018-01-16'),
('Alex', 'Alexsen', 'Ballerup Telegrafvej 1', 2750, '2018-01-16'),
('Søren', 'Sørensen', 'Bredekærs vænge 59', 2635, '2014-02-01'),
('Jason', 'Jasonsen', 'Hillerødgade 88', 2200, '2010-07-23')
go

INSERT INTO Kontotype
VALUES 
(1,'LÅN', 0.5),
(2,'OPSARING', 1),
(3,'BØRNEOPSARING', 0.85),
(4,'LØNKONTO', 0.5),
(5,'KONTOKONTO',0.5),
(6, 'AKTIEKONTO',0.4)
go

INSERT INTO Konto(FK_KundeNr,FK_KontoTypeID,Saldo,Oprettelsesdato)
VALUES
(1, 1, 0, '2018-01-16'),
(2, 3, 2, '2017-06-16'),
(1, 4, -5000000, '2018-01-16'),
(2, 5, 1, '2018-01-16'),
(1, 2, 0.00001, '2014-02-01'),
(2, 6, 1000000, '2010-07-23')
go
--SELECT * FROM [Postnummer-by]

--SELECT * FROM Kunde

SELECT KundeNr, Fornavn, Efternavn, KontoNr, Saldo
FROM Kunde
INNER JOIN Konto ON Kunde.KundeNr = Konto.FK_KundeNr;

SELECT Saldo FROM Konto WHERE FK_KundeNr = 2;

SELECT KundeNr, Fornavn, Efternavn, KontoNr, Saldo
FROM Kunde
INNER JOIN Konto ON Kunde.KundeNr = Konto.FK_KundeNr
WHERE KundeNr = 2