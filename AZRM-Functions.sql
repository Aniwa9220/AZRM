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
		S.IDsk³ad as ID_sk³adu, S.typsk³adu as typ_sk³adu, S.dzieñpracy as Dzieñ_pracy
		from Sk³ad S inner join AspNetUsers P on S.IDpracownika=P.IDpracownika 
		inner join Funkcja F on F.IDfunkcji=P.IDfunkcji
		where S.dzieñpracy=@data
		group by S.typsk³adu, S.IDsk³ad, S.dzieñpracy, P.Name, P.UserName, F.stanowisko)
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
return (select P.nazwisko as nazwisko, P.Imie as imie, I.Opis as choroba, Z.datazg³oszenia as Przyjecie
		from Zg³oszenie Z inner join Szpital Sz on Sz.IDszpitala=Z.IDszpitala 
		inner join Pacjent P on Z.IDpacjenta=P.IDpacjenta 
		inner join KARTACHOROBY K on K.iDpacjenta=P.IDpacjenta
		inner join ICD10 I on I.IDicd10=K.IDicd10
		where Sz.nazwa=@nazwa)
GO


-------------------------------------------------------------


create function [dbo].[znajdz_slowo](@slowo varchar(9))

returns table 
as

return (select P.nazwisko as nazwisko, P.Imie as imie, I.Opis as choroba, Z.datazg³oszenia as Przyjecie,
		sz.nazwa as nazwa_szpitala, Z.danezg³oszenia as szczegó³y_pogotowia
		from Zg³oszenie Z inner join Szpital Sz on Sz.IDszpitala=Z.IDszpitala 
		inner join Pacjent P on Z.IDpacjenta=P.IDpacjenta 
		inner join KARTACHOROBY K on K.iDpacjenta=P.IDpacjenta
		inner join ICD10 I on I.IDicd10=K.IDicd10
		where Z.danezg³oszenia like '%'+@slowo+'%')
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
set @Sum = (select count(*) from Sk³ad where dzieñpracy=@data)
set @S = (select count(*) from Sk³ad where typsk³adu='S' and dzieñpracy=@data)
set @T = (select count(*) from Sk³ad where typsk³adu='T' and dzieñpracy=@data)
set @P= (select count(*) from Sk³ad where typsk³adu='P' and dzieñpracy=@data)
select @WYNIK= 'w dniu' + cast((@data) as char) +' jest ' + cast((@Sum) as char) +' za³óg obecnie na mieœcie, w tym:' + cast((@P)as char) + ' za³óg typu P, '+ cast((@S)as char)+' za³óg typu S, '+ cast((@T) as char)+' za³óg typu T.'
from Grafik inner join Sk³ad on Grafik.IDsk³ad=Sk³ad.IDsk³ad
where Grafik.dzieñdy¿uru=@data
return (@WYNIK)
end
GO

-------------------------------------------------------------

CREATE FUNCTION [dbo].[HIST_N] (@nazwisko char(30))
RETURNS nvarchar(200)
AS
BEGIN 
	DECLARE @WYNIK NVARCHAR(200)
	SELECT @WYNIK=P.NAZWISKO+' '+P.IMIE+' zosta³ przyjêty przez PRM w dniu '+cast((Z.datazg³oszenia) as char)+',  rozpoznano chorobê wg ICD10: '+ K.IDicd10 + '-'+I.Opis+'.'
	from ZG£OSZENIE Z inner join Pacjent P on P.IDpacjenta=Z.IDpacjenta inner join KARTACHOROBY K on P.IDpacjenta=K.iDpacjenta inner join ICD10 I on K.IDicd10=I.IDicd10
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
select @WYNIK=cast((count(Zg³oszenie.IDszpitala)) as int) from Zg³oszenie inner join Szpital on Zg³oszenie.IDszpitala=Szpital.IDszpitala
where Szpital.nazwa=@nazwa
return @WYNIK
end
GO

