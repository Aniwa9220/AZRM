USE [SWD2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-----------------------------------------------------------
--funkcje tabelaryczne-------------------------------------

CREATE function [dbo].[Kto_na_miasto](@data date)
returns table
as
return (select P.Name as Imie_Nazwisko, P.UserName as Log_in, F.stanowisko as stanowisko,
		S.IDsk�ad as ID_sk�adu, S.typsk�adu as typ_sk�adu, S.dzie�pracy as Dzie�_pracy
		from Sk�ad S inner join AspNetUsers P on S.IDpracownika=P.IDpracownika 
		inner join Funkcja F on F.IDfunkcji=P.IDfunkcji
		where S.dzie�pracy=@data
		group by S.typsk�adu, S.IDsk�ad, S.dzie�pracy, P.Name, P.UserName, F.stanowisko)
GO

------------------------------------------------------------


Create function [dbo].[makeview1] ()
returns table
as
return (SELECT * FROM Pac_Szp(''));
GO

------------------------------------------------------------

Create function [dbo].[Pac_Szp](@nazwa char(20))
returns table
as
return (select P.nazwisko as nazwisko, P.Imie as imie, I.Opis as choroba, Z.datazg�oszenia as Przyjecie
		from Zg�oszenie Z inner join Szpital Sz on Sz.IDszpitala=Z.IDszpitala 
		inner join Pacjent P on Z.IDpacjenta=P.IDpacjenta 
		inner join KARTACHOROBY K on K.iDpacjenta=P.IDpacjenta
		inner join ICD10 I on I.IDicd10=K.IDicd10
		where Sz.nazwa=@nazwa)
GO


-------------------------------------------------------------


create function [dbo].[znajdz_slowo](@slowo varchar(9))

returns table 
as

return (select P.nazwisko as nazwisko, P.Imie as imie, I.Opis as choroba, Z.datazg�oszenia as Przyjecie,
		sz.nazwa as nazwa_szpitala, Z.danezg�oszenia as szczeg�y_pogotowia
		from Zg�oszenie Z inner join Szpital Sz on Sz.IDszpitala=Z.IDszpitala 
		inner join Pacjent P on Z.IDpacjenta=P.IDpacjenta 
		inner join KARTACHOROBY K on K.iDpacjenta=P.IDpacjenta
		inner join ICD10 I on I.IDicd10=K.IDicd10
		where Z.danezg�oszenia like '%'+@slowo+'%')
GO

-----------------------------------------------------------------------------
--Funkcje skalarne--
create function [dbo].[dat_il_zal](@data date) 
returns nvarchar(300)
as
begin 
declare @WYNIK nvarchar(300)
declare @P int
declare @T int
declare @S int
declare @Sum int 
set @Sum = (select count(*) from Sk�ad where dzie�pracy=@data)
set @S = (select count(*) from Sk�ad where typsk�adu='S' and dzie�pracy=@data)
set @T = (select count(*) from Sk�ad where typsk�adu='T' and dzie�pracy=@data)
set @P= (select count(*) from Sk�ad where typsk�adu='P' and dzie�pracy=@data)
select @WYNIK= 'w dniu' + cast((@data) as char) +' jest ' + cast((@Sum) as char) +' za��g obecnie na mie�cie, w tym:' + cast((@P)as char) + ' za��g typu P, '+ cast((@S)as char)+' za��g typu S, '+ cast((@T) as char)+' za��g typu T.'
from Grafik inner join Sk�ad on Grafik.IDsk�ad=Sk�ad.IDsk�ad
where Grafik.dzie�dy�uru=@data
return (@WYNIK)
end
GO

-------------------------------------------------------------

CREATE FUNCTION [dbo].[HIST_N] (@nazwisko char(30))
RETURNS nvarchar(200)
AS
BEGIN 
	DECLARE @WYNIK NVARCHAR(200)
	SELECT @WYNIK=P.NAZWISKO+' '+P.IMIE+' zosta� przyj�ty przez PRM w dniu '+cast((Z.datazg�oszenia) as char)+',  rozpoznano chorob� wg ICD10: '+ K.IDicd10 + '-'+I.Opis+'.'
	from ZG�OSZENIE Z inner join Pacjent P on P.IDpacjenta=Z.IDpacjenta inner join KARTACHOROBY K on P.IDpacjenta=K.iDpacjenta inner join ICD10 I on K.IDicd10=I.IDicd10
	where P.nazwisko=@nazwisko
	return (@WYNIK)
END
GO


--------------------------------------------------------------------------


CREATE FUNCTION [dbo].[SZPIT_ILE](@nazwa char(30))
RETURNS int
AS
begin
declare @WYNIK int
select @WYNIK=cast((count(Zg�oszenie.IDszpitala)) as int) from Zg�oszenie inner join Szpital on Zg�oszenie.IDszpitala=Szpital.IDszpitala
where Szpital.nazwa=@nazwa
return @WYNIK
end
GO

