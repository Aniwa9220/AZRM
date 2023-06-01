USE [SWD2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
----------------------------------------------------------------------
--Trigger dot. squad 100 w Grafik, powstaje po rejestracji pacjenta.--
CREATE TRIGGER [dbo].[create_100sqd]
ON [dbo].[PACJENT] AFTER INSERT
AS
IF EXISTS (Select * from GRAFIK where IDsk³ad='100' and dzieñdy¿uru=CONVERT(VARCHAR(10), getdate(), 111))

PRINT('ID 100 istnieje')

ELSE

Insert into GRAFIK(IDsk³ad,dzieñdy¿uru,zmiana,Status) values ('100',GETDATE(),'1','0');


ALTER TABLE [dbo].[PACJENT] ENABLE TRIGGER [create_100sqd]
GO
-----------------------------------------------------------------------
--Trigger dot. statusu w Grafiku---------------------------------------
CREATE TRIGGER [dbo].[change_status0]
ON [dbo].[ZG£OSZENIE] AFTER UPDATE
AS
IF UPDATE(DATAZAMKNIECIA)
UPDATE GRAFIK 
SET GRAFIK.STATUS=0 from Grafik G inner join Zg³oszenie Z on Z.datazg³oszenia=G.dzieñdy¿uru and Z.IDsk³ad=G.IDsk³ad
where G.dzieñdy¿uru=CAST( GETDATE() AS Date )  and G.IDsk³ad=Z.IDsk³ad


ALTER TABLE [dbo].[ZG£OSZENIE] ENABLE TRIGGER [change_status0]
GO


CREATE TRIGGER [dbo].[change_status1]
ON [dbo].[ZG£OSZENIE] AFTER UPDATE
AS
IF UPDATE(IDSK£AD)
UPDATE GRAFIK 
SET GRAFIK.STATUS=1 from Grafik G inner join Zg³oszenie Z on Z.datazg³oszenia=G.dzieñdy¿uru and Z.IDsk³ad=G.IDsk³ad
where G.dzieñdy¿uru=CAST( GETDATE() AS Date )  and G.IDsk³ad=Z.IDsk³ad


ALTER TABLE [dbo].[ZG£OSZENIE] ENABLE TRIGGER [change_status1]
GO



