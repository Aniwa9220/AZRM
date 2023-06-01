USE [SWD2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------
--Podstawowe funkkcje sk³¹dowe pomocnicze dla admina----


CREATE procedure [dbo].[zrobpowiazania]
as
BEGIN

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_ZG£OSZENIE_SZPITAL'))
BEGIN
	PRINT 'POWI¥ZANIE ZG£OSZENIE-SZPITAL ISTNIEJE W BD SWD2'
	END
ELSE
BEGIN
	PRINT 'TWORZE POWI¥ZANIE ZG£OSZENIE-SZPITAL'
	ALTER TABLE ZG£OSZENIE ADD
	CONSTRAINT [FK_ZG£OSZENIE_SZPITAL] FOREIGN KEY
	(IDszpitala) REFERENCES SZPITAL(IDszpitala)
END
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_ZG£OSZENIE_PACJENT'))
BEGIN
	PRINT 'POWI¥ZANIE ZG£OSZENIE-PACJENT ISTNIEJE W BD SWD2'
	END
ELSE
BEGIN
	PRINT 'TWORZE POWI¥ZANIE ZG£OSZENIE-PACJENT'
	ALTER TABLE ZG£OSZENIE ADD
	CONSTRAINT [FK_ZG£OSZENIE_PACJENT] FOREIGN KEY
	(IDpacjenta) REFERENCES PACJENT(IDpacjenta)
END
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_SK£AD_GRAFIK'))
BEGIN
	PRINT 'POWI¥ZANIE SK£AD-GRAFIK ISTNIEJE W BD SWD2'
END
ELSE
BEGIN
	PRINT 'TWORZE POWI¥ZANIE SK£AD-GRAFIK'
	ALTER TABLE SK£AD ADD
	CONSTRAINT [FK_SK£AD_GRAFIK] FOREIGN KEY
	(IDsk³ad, dzieñpracy) REFERENCES GRAFIK(IDsk³ad, dzieñdy¿uru)
END
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_AspNetUsers_FUNKCJA'))
BEGIN
	PRINT 'POWI¥ZANIE AspNetUsers-FUNCKJA ISTNIEJE W BD SWD2'
END
ELSE
BEGIN
	PRINT 'TWORZE POWI¥ZANIE AspNetUsers-FUNKCJA'
	ALTER TABLE AspNetUsers ADD
	CONSTRAINT [FK_AspNetUsers_FUNCKJA] FOREIGN KEY
	(IDfunkcji) REFERENCES FUNKCJA(IDfunkcji)
END

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_SK£AD_AspNetUsers'))
BEGIN
	PRINT 'POWI¥ZANIE SK£AD-AspNetUsers ISTNIEJE W BD SWD2'
END
ELSE
BEGIN
	PRINT 'TWORZE POWI¥ZANIE SK£AD-AspNetUsers'
	ALTER TABLE SK£AD ADD
	CONSTRAINT [FK_SK£AD_AspNetUsers] FOREIGN KEY
	(IDpracownika) REFERENCES AspNetUsers(IDpracownika)
END 
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_ZG£OSZENIE_KARTACHOROBY'))
BEGIN 
	PRINT 'POWI¥ZANIE ZG£OSZENIE-KARTACHOROBY ISTNIEJE W BD SWD2'
	END
ELSE 
BEGIN
	PRINT 'TWORZE POWI¥ZANIE ZG£OSZENIE-KARTACHOROBY'
	ALTER TABLE ZG£OSZENIE ADD
	CONSTRAINT [FK_ZG£OSZENIE_KARTACHOROBY] FOREIGN KEY
	(IDkartychoroby) REFERENCES KARTACHOROBY(IDkarty)
END
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_KARTACHOROBY_PACJENT'))
BEGIN 
	PRINT 'POWI¥ZANIE KARTACHOROBY-PACJENT ISTNIEJE W BD SWD2'
	END
ELSE 
BEGIN
	PRINT 'TWORZE POWI¥ZANIE KARTACHOROBY-PACJENT'
	ALTER TABLE KARTACHOROBY ADD
	CONSTRAINT [FK_KARTACHOROBY_PACJENT] FOREIGN KEY
	(IDpacjenta) REFERENCES PACJENT(IDpacjenta)
END
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_KARTACHOROBY_ICD10'))
BEGIN
	PRINT 'POWI¥ZANIE KARTACHOROBY-ICD10 ISTNIEJE W BD SWD2'
	END
ELSE
BEGIN
	PRINT 'TWORZE POWI¥ZANIE KARTACHOROBY-ICD10'
	ALTER TABLE KARTACHOROBY ADD
	CONSTRAINT [FK_KARTACHOROBY_ICD10] FOREIGN KEY
	(IDicd10) REFERENCES ICD10(IDicd10)
END
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_ZG£OSZENIE_PACJENT'))
BEGIN
	PRINT 'POWI¥ZANIE ZG£OSZENIE-PACJENT ISTNIEJE W BD SW2'
	END
ELSE
BEGIN 
	PRINT 'TWORZÊ POWI¥ZANIE ZG£OSZENIE-PACJENT'
	ALTER TABLE ZG£OSZENIE ADD
	CONSTRAINT [FK_ZG£OSZENIE_PACJENT] FOREIGN KEY
	(IDpacjenta) REFERENCES PACJENT(IDpacjenta)
END
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_ZG£OSZENIE_GRAFIK'))
BEGIN
	PRINT 'POWI¥ZANIE ZG£OSZENIE-GRAFIK ISTNIEJE W BD SWD2'
	END
ELSE
BEGIN
	PRINT 'TWORZE POWI¥ZANIE GRAFIK-ZG£OSZENIE'
	ALTER TABLE ZG£OSZENIE ADD
	CONSTRAINT [FK_GRAFIK_ZG£OSZENIE] FOREIGN KEY
	(IDsk³ad, datazg³oszenia) REFERENCES GRAFIK(IDsk³ad, dzieñdy¿uru)
END
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID('FK_SK£AD_GeoData'))
BEGIN
	PRINT 'POWI¥ZANIE SK£AD-GeoData ISTNIEJE W BD SWD2'
END
ELSE
BEGIN
	PRINT 'TWORZE POWI¥ZANIE SK£AD-GeoData'
	ALTER TABLE SK£AD ADD
	CONSTRAINT [FK_SK£AD_GeoData] FOREIGN KEY
	(IDsk³ad) REFERENCES GeoData(IDgps)
END
END
GO



CREATE procedure [dbo].[zrobtabele] 
as
begin
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'FUNKCJA'))
BEGIN
	PRINT 'TABELA FUNKCJA W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN 
	PRINT 'TWORZÊ TABELE FUNCKJA'
	CREATE TABLE FUNKCJA (
	IDfunkcji int  primary key identity (1,1),
	stanowisko varchar(10) null default ('brak przydzia³u')
	)
END


IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'SK£AD'))
BEGIN
	PRINT 'TABELA SK£AD W DB SWD2 ISTNIEJE JU¯'
	END
ELSE
BEGIN
	PRINT 'TWORZÊ TABELE SK£AD'
	CREATE TABLE SK£AD (
	Porzadkowa int primary key identity (1,1),
	IDsk³ad int not null,
	typsk³adu varchar(1) check(typsk³adu in ('S', 'P', 'T')) default ('X'),
	dzieñpracy date not null DEFAULT GETDATE(),
	IDpracownika int not null
	)
	ALTER TABLE SK£AD
	ADD CONSTRAINT UK_SK£AD UNIQUE NONCLUSTERED (IDpracownika,dzieñpracy)
END



IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'GRAFIK'))
BEGIN
	PRINT 'TABELA GRAFIK W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
	PRINT 'TWORZÊ TABELE GRAFIK'
	CREATE TABLE GRAFIK (
	Lp int primary key identity (1,1), 
	IDsk³ad int not null,
	dzieñdy¿uru date not null  DEFAULT GETDATE(),
	zmiana int not null
	)
	ALTER TABLE GRAFIK
	ADD CONSTRAINT UK_GRAFIK UNIQUE NONCLUSTERED (IDsk³ad,dzieñdy¿uru)
END



IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'ZG£OSZENIE'))
BEGIN
	PRINT 'TABELA ZG£OSZENIE W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
	PRINT 'TWORZÊ TABELE ZG£OSZENIE'
	CREATE TABLE ZG£OSZENIE (
	IDzg³oszenia int primary key identity (1,1),
	IDsk³ad int not null,
	IDpacjenta int null default ('0'), -- 0 dla bezdomnych
	datazg³oszenia date not null  DEFAULT GETDATE(),
	danezg³oszenia varchar(50) null default('brak opisu'),
	datazamkniecia datetime null,
	datarejestracji datetime null,
	IDszpitala int null default ('0'), -- 0 pacjent w drodze
	IDkartychoroby int not null
	)
	ALTER TABLE ZG£OSZENIE
	ADD CONSTRAINT UK_ZG£OSZENIE UNIQUE(IDzg³oszenia, datazg³oszenia)
END



IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'SZPITAL'))
BEGIN
	PRINT 'TABELA SZTPITAL W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
	PRINT 'TWORZÊ TABELE SZPITAL'
	CREATE TABLE SZPITAL (
	IDszpitala int primary key identity (0,1), -- 0 pacjent w drodze
	nazwa varchar(20) null default ('placówka spoza PRM'),
	adres varchar(50) null default ('brak adresu')
	)
	insert into SZPITAL values('pacjent w drodze', default)
END



IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'PACJENT'))
BEGIN
	PRINT 'TABELA PACJENT W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
	PRINT 'TWORZÊ TABELE PACJENT'
	CREATE TABLE PACJENT (
	IDpacjenta int primary key identity (0,1), -- 0 dla bezdomnych
	imie varchar(20) null default ('brak imienia'),
	nazwisko varchar(20) null default ('brak nazwiska')
	
	)
	INSERT INTO PACJENT VALUES (DEFAULT, DEFAULT)
END



IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'KARTACHOROBY'))
BEGIN
	PRINT 'TABELA KARTACHOROBY W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
	PRINT 'TWORZÊ TABELE KARTAchoroby'
	CREATE TABLE KARTACHOROBY(
	IDkarty int primary key identity (1,1),
	IDicd10 varchar(10) null default ('brak info'),
	iDpacjenta int not null default (0)
	)
END


IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'ICD10'))
BEGIN
	PRINT 'TABELA ICD10 W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
	PRINT 'TWORZÊ TABELE ICD10'
	CREATE TABLE ICD10 (
	IDicd10 varchar(10) primary key,
	opis varchar(255) not null
	)
	insert into ICD10 VALUES('brak info', 'brak wywiadu')
END


IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'GeoData'))
BEGIN
	PRINT 'TABELA GeoData W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
	PRINT 'TWORZÊ TABELE GeoData'
	CREATE TABLE GeoData(
	IDgps int primary key identity (1,1), 
	szer nvarchar (255) null,
	dl nvarchar (255) null,
	datarejestracji datetime null
	)	
END

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'__EFMigrationHistory'))
BEGIN
	PRINT 'TABELA EF Migration W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
PRINT 'TWORZÊ TABELE EFMigrationsHistory'
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'AspNetRoles'))
BEGIN
	PRINT 'TABELA AspNetRoles W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
PRINT 'TWORZÊ TABELE AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'AspNetRoleCLaims'))
BEGIN
	PRINT 'TABELA AspNetRoleClaims W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
PRINT 'TWORZÊ TABELE AspNetRolesClaims'
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]

END

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'AspNetUsers'))
BEGIN
	PRINT 'TABELA AspNetUsers W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
PRINT 'TWORZÊ TABELE AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Mail] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IDfunkcji] [int] NULL,
	[IDpracownika] [int] NOT NULL identity (1,1),
	[Rola] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_AspNetUsers] UNIQUE NONCLUSTERED 
(
	[IDpracownika] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = OFF, ALLOW_PAGE_LOCKS = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__AspNetUser__Name__2898D86D]  DEFAULT (N'') FOR [Name]


ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_FUNKCJA] FOREIGN KEY([IDfunkcji])
REFERENCES [dbo].[FUNKCJA] ([IDfunkcji])


ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_FUNKCJA]

END


IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'AspNetUsersTokens'))
BEGIN
	PRINT 'TABELA AspNetUsersTokens W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
PRINT 'TWORZÊ TABELE AspNetUsersTokens'
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE


ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]

END

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'AspNetUsersRoles'))
BEGIN
	PRINT 'TABELA AspNetUsersRoles W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
PRINT 'TWORZÊ TABELE AspNetUsersRoles'
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE


ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]


ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE


ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
END

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'AspNetUserLogins'))
BEGIN
	PRINT 'TABELA AspNetUserLogins W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
PRINT 'TWORZÊ TABELE AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE


ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
END

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID=OBJECT_ID(N'AspNetUserClaims'))
BEGIN
	PRINT 'TABELA AspNetUserClaims W DB SWD2 ISTNIEJE JU¯!'
	END
ELSE 
BEGIN
PRINT 'TWORZÊ TABELE AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE


ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]

END

END







GO




--------------------------------------------------------
CREATE procedure [dbo].[zapisz_nowy_sk³ad]
	(
	@IDsk³ad int,
	@typsk³adu varchar(1),
	@IDpracownika int,
	@DzieñPracy date,
	@Komunikat varchar(200) output)
	as
	begin try
		insert into SK£AD( IDsk³ad, typsk³adu, IDpracownika, DzieñPracy)
		values (@IDsk³ad, @typsk³adu, @IDpracownika, @DzieñPracy)
		set @Komunikat = 'Tworzê nowy sk³ad - pomyœlnie ukoñczono'
	end try

	begin catch
		set @Komunikat = 'B³¹d tworzenia nowego sk³adu - brak zapisu'
	select ERRORNUMBER=ERROR_NUMBER(),
			ERRORSEVERITY = ERROR_SEVERITY(),
			ERORRSTATE = ERROR_STATE(),
		ERRORPROCEDURE = ERROR_PROCEDURE(),
		ERRORLINE = ERROR_LINE(),
		ERRORMESSAGE = ERROR_MESSAGE()
END CATCH
GO



-------------------------------------------------------
create procedure [dbo].[zapisz_nowy_pacjent]
	(
	@imie varchar(20),
	@nazwisko varchar(30),
	@Komunikat varchar(200) output)
	as
	begin try
		insert into PACJENT ( imie, nazwisko)
		values (@imie, @nazwisko)
		set @Komunikat = 'Tworzê nowego pacjenta - pomyœlnie ukoñczono'
	end try

	begin catch
		set @Komunikat = 'B³¹d tworzenia nowego pacjenta - brak zapisu'
	select ERRORNUMBER=ERROR_NUMBER(),
			ERRORSEVERITY = ERROR_SEVERITY(),
			ERORRSTATE = ERROR_STATE(),
		ERRORPROCEDURE = ERROR_PROCEDURE(),
		ERRORLINE = ERROR_LINE(),
		ERRORMESSAGE = ERROR_MESSAGE()
END CATCH
GO





----------------------------------------------------------
create procedure [dbo].[zapisz_nowy_grafik]
	(
	@IDsk³ad int,
	@DzieñDy¿uru date,
	@zmiana int, 
	@Status int,
	@Komunikat varchar(200) output)
	as
	begin try
		insert into Grafik( IDsk³ad, DzieñDy¿uru, zmiana, Status)
		values (@IDsk³ad, @DzieñDy¿uru, @zmiana,@Status)
		set @Komunikat = 'Tworzê nowy grafik - pomyœlnie ukoñczono'
	end try

	begin catch
		set @Komunikat = 'B³¹d tworzenia nowego grafiku - brak zapisu'
	select ERRORNUMBER=ERROR_NUMBER(),
			ERRORSEVERITY = ERROR_SEVERITY(),
			ERORRSTATE = ERROR_STATE(),
		ERRORPROCEDURE = ERROR_PROCEDURE(),
		ERRORLINE = ERROR_LINE(),
		ERRORMESSAGE = ERROR_MESSAGE()
END CATCH
GO




--------------------------------------------------------

create  procedure [dbo].[zapisz_nowa_karta]
(@IDid10 varchar(10), 
@Komunikat varchar(200) output)
as
declare @IDpacjenta int
Begin try
set @IDpacjenta=(SELECT TOP(1) IDpacjenta
FROM Pacjent Order By IDpacjenta DESC)
insert into KARTACHOROBY (IDicd10, iDpacjenta)
values (@IDid10, @IDpacjenta) 
set @Komunikat = 'Tworze nowa karte choroby dla ' + (cast(@IDpacjenta as varchar)) + ' - operacja pomyœlna'
end try
begin catch
SET @komunikat='B£¥D ZAPISU KARTY CHOROBY - NIE ZAPISANO'
SELECT ERRORNUMBER=ERROR_NUMBER(),
			ERRORSEVERITY = ERROR_SEVERITY(),
			ERORRSTATE = ERROR_STATE(),
		ERRORPROCEDURE = ERROR_PROCEDURE(),
		ERRORLINE = ERROR_LINE(),
		ERRORMESSAGE = ERROR_MESSAGE()
END CATCH
GO
-----------------------------------------------------

CREATE procedure [dbo].[zapisz_nowe_zgloszenie]
(@IDsk³ad int,
@datazg³oszenia date,
@danezg³oszenia varchar(50),
@datazamkniecia datetime,
@IDszpitala int,
@Komunikat varchar(200) output)
as
declare @IDpacjenta int
declare @IDkartychoroby int
declare @IDZgloszenia int
Begin try
set @IDkartychoroby = (SELECT TOP(1) IDkarty
FROM KARTACHOROBY Order By IDkarty DESC)
set @IDpacjenta=(SELECT TOP(1) IDpacjenta
FROM Pacjent Order By IDpacjenta DESC)
--set @IDZgloszenia=(SELECT TOP(1) IDzg³oszenia +1
--FROM ZG£OSZENIE Order By IDzg³oszenia DESC)
--SET IDENTITY_INSERT ZG£OSZENIE On
insert into ZG£OSZENIE ( IDsk³ad, datazg³oszenia, danezg³oszenia, datazamkniecia ,IDszpitala, IDkartychoroby) --IDzg³oszenia,
values ( @IDsk³ad, @datazg³oszenia, @danezg³oszenia, @datazamkniecia, @IDszpitala, @IDkartychoroby) --@IDZgloszenia,
--SET IDENTITY_INSERT ZG£OSZENIE Off
set @Komunikat = 'Utworzono nowe zg³oszenia dla ID pacjenta ' + (cast(@IDpacjenta as varchar)) + ' - operacja pomyœlna'
end try
begin catch
SET @komunikat='B£¥D ZAPISU Zg³oszenia - NIE ZAPISANO'
SELECT ERRORNUMBER=ERROR_NUMBER(),
			ERRORSEVERITY = ERROR_SEVERITY(),
			ERORRSTATE = ERROR_STATE(),
		ERRORPROCEDURE = ERROR_PROCEDURE(),
		ERRORLINE = ERROR_LINE(),
		ERRORMESSAGE = ERROR_MESSAGE()
END CATCH
GO

------------------------------------------------------------------

-------------------------------------------------------
--Procedury pomocnicze sk³adowe dla SWD2---------------------------


CREATE PROCEDURE [dbo].[ICDAFTER]
AS
BEGIN

-- injection xls 
SET IDENTITY_INSERT [KARTACHOROBY] ON;
ALTER TABLE ZG£OSZENIE ADD
	CONSTRAINT [FK_ZG£OSZENIE_KARTACHOROBY] FOREIGN KEY
	(IDkartychoroby) REFERENCES KARTACHOROBY(IDkarty);
END;
GO

-------------------------------------------------------


CREATE PROCEDURE [dbo].[ICDBEFORE]
AS 
BEGIN
ALTER TABLE [ICD10] DROP CONSTRAINT [FK_KARTACHOROBY_ICD10];
ALTER TABLE ZG£OSZENIE DROP CONSTRAINT [FK_ZG£OSZENIE_KARTACHOROBY];
ALTER TABLE KARTACHOROBY DROP CONSTRAINT [FK_KARTACHOROBY_PACJENT];
DELETE FROM [ICD10];
ALTER TABLE ICD10 ADD
	CONSTRAINT [FK_KARTACHOROBY_ICD10] FOREIGN KEY
	(IDicd10) REFERENCES ICD10(IDicd10);
ALTER TABLE KARTACHOROBY ADD
	CONSTRAINT [FK_KARTACHOROBY_PACJENT] FOREIGN KEY
	(IDpacjenta) REFERENCES PACJENT(IDpacjenta);
END;
GO

--------------------------------------------------------

ALTER procedure [dbo].[usundata]
as
begin
PRINT 'USUNIÊTO WSZYSTKIE DANE Z TABEL Z K S G I Sz F i P - Asp.Net pozostawiono W BD SWD2'
TRUNCATE TABLE ZG£OSZENIE 
TRUNCATE TABLE KARTACHOROBY
TRUNCATE TABLE SK£AD
TRUNCATE TABLE GRAFIK
TRUNCATE TABLE ICD10
TRUNCATE TABLE SZPITAL
TRUNCATE TABLE FUNKCJA
TRUNCATE TABLE PRACOWNIK
END

------------------------------------------------------------

create procedure [dbo].[usunpowiazania]
as
begin 

ALTER TABLE SK£AD DROP
		CONSTRAINT [FK_SK£AD_AspNetUsers]
ALTER TABLE GRAFIK DROP 
		CONSTRAINT [FK_GRAFIK_ZG£OSZENIE]
ALTER TABLE SK£AD DROP
		CONSTRAINT [FK_SK£AD_GRAFIK]
ALTER TABLE ZG£OSZENIE DROP
		CONSTRAINT [FK_ZG£OSZENIE_KARTACHOROBY]

ALTER TABLE ZG£OSZENIE DROP
		CONSTRAINT [FK_ZG£OSZENIE_PACJENT]

ALTER TABLE KARTACHOROBY DROP
		CONSTRAINT [FK_KARTACHOROBY_PACJENT]
	
ALTER TABLE KARTACHOROBY DROP
		CONSTRAINT [FK_KARTACHOROBY_ICD10]
ALTER TABLE AspNetUsers DROP 
		CONSTRAINT [FK_AspNetUsers_FUNKCJA]
ALTER TABLE ZG£OSZENIE DROP
		CONSTRAINT [FK_ZG£OSZENIE_SZPTAL]
	
END

GO

---------------------------------------------------------------------------

create procedure [dbo].[usuntabele]
as
begin
print 'usun¹³em tabele P AspNetUsers G S Z Sz K Icd:-)'
DROP TABLE PACJENT
DROP TABLE AspNetUsers
DROP TABLE GRAFIK
DROP TABLE SK£AD
DROP TABLE FUNKCJA
DROP TABLE ZG£OSZENIE
DROP TABLE SZPITAL
DROP TABLE KARTACHOROBY
DROP TABLE ICD10

end
GO

---------------------------------------------------------------------------

