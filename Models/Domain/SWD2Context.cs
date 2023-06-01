using System;
using System.Collections.Generic;
using AZRM2023v1.Models.SWD2;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
namespace AZRM2023v1.Models.Domain
{
    public partial class SWD2Context : DbContext
    {
       

        public SWD2Context()
        {
        }

        public SWD2Context(DbContextOptions<SWD2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Funkcja> Funkcjas { get; set; } = null!;
        public virtual DbSet<Grafik> Grafiks { get; set; } = null!;
        public virtual DbSet<Icd10> Icd10s { get; set; } = null!;
        public virtual DbSet<Kartachoroby> Kartachorobies { get; set; } = null!;
        public virtual DbSet<Pacjent> Pacjents { get; set; } = null!;
        public virtual DbSet<Skład> Składs { get; set; } = null!;
        public virtual DbSet<Szpital> Szpitals { get; set; } = null!;
        public virtual DbSet<VChorobyBezdomni> VChorobyBezdomnis { get; set; } = null!;
        public virtual DbSet<VChorobyWszyscy> VChorobyWszyscies { get; set; } = null!;
        public virtual DbSet<VDaneWyjazdSexualAbuse> VDaneWyjazdSexualAbuses { get; set; } = null!;
        public virtual DbSet<VHistoriaTransportu> VHistoriaTransportus { get; set; } = null!;
        public virtual DbSet<VIlIntPracownikSklad> VIlIntPracownikSklads { get; set; } = null!;
        public virtual DbSet<VIlInterwencjiPracownika> VIlInterwencjiPracownikas { get; set; } = null!;
        public virtual DbSet<VIlPracyPracownika> VIlPracyPracownikas { get; set; } = null!;
        public virtual DbSet<VIlZgWgdatum> VIlZgWgdata { get; set; } = null!;
        public virtual DbSet<VIleRazyPacjent> VIleRazyPacjents { get; set; } = null!;
        public virtual DbSet<VKtoPracowal> VKtoPracowals { get; set; } = null!;
        public virtual DbSet<VListazgłoszeń> VListazgłoszeńs { get; set; } = null!;
        public virtual DbSet<VObsadaStanowiskWgdaty> VObsadaStanowiskWgdaties { get; set; } = null!;
        public virtual DbSet<VPracownicyWgdatyUzytoRezerwa> VPracownicyWgdatyUzytoRezerwas { get; set; } = null!;
        public virtual DbSet<VRezerwa> VRezerwas { get; set; } = null!;
        public virtual DbSet<VStatystykaPrmZałóg> VStatystykaPrmZałógs { get; set; } = null!;
        public virtual DbSet<Zgłoszenie> Zgłoszenies { get; set; } = null!;
        public virtual DbSet<ViewAll> views { get; set; }
        public virtual DbSet<ViewWord> views2 { get; set; }
        public virtual DbSet<ViewCrew> views3 { get; set; }

 



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                  .Build();

                string connectionString = configuration.GetConnectionString("conn");

                optionsBuilder.UseSqlServer(connectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.Idpracownika, "IX_AspNetUsers")
                    .IsUnique();

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Idfunkcji).HasColumnName("IDfunkcji");

                entity.Property(e => e.Idpracownika).HasColumnName("IDpracownika");

                entity.Property(e => e.Name).HasDefaultValueSql("(N'')");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256)
                                                .HasColumnName("UserName"); // dopisane



                entity.HasOne(d => d.IdfunkcjiNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.Idfunkcji)
                    .HasConstraintName("FK_AspNetUsers_FUNKCJA");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUsers>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });


            modelBuilder.Entity<Funkcja>(entity =>
            {
                entity.HasKey(e => e.Idfunkcji)
                    .HasName("PK__FUNKCJA__A6667A452474D119");

                entity.ToTable("FUNKCJA");

                entity.Property(e => e.Idfunkcji).HasColumnName("IDfunkcji");

                entity.Property(e => e.Stanowisko)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("stanowisko")
                    .HasDefaultValueSql("('brak przydziału')");
            });

            modelBuilder.Entity<Grafik>(entity =>
            {
                entity.HasKey(e => e.Lp)
                    .HasName("PK__GRAFIK__3214A56DACBAFC82");

                entity.ToTable("GRAFIK");
                entity.Property(e => e.Lp).HasColumnName("Lp"); 
                entity.Property(e => e.Status).HasColumnName("Status"); 
                entity.Property(e => e.Status).HasDefaultValueSql("('0')");
                entity.HasIndex(e => new { e.Idskład, e.Dzieńdyżuru }, "UK_GRAFIK")
                    .IsUnique();

                entity.Property(e => e.Dzieńdyżuru)
                    .HasColumnType("date")
                    .HasColumnName("dzieńdyżuru")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Idskład).HasColumnName("IDskład");

                entity.Property(e => e.Zmiana).HasColumnName("zmiana");
            });

            modelBuilder.Entity<Icd10>(entity =>
            {
                entity.HasKey(e => e.Idicd10)
                    .HasName("PK__ICD10__FB8E2A1FF1E69731");

                entity.ToTable("ICD10");

                entity.Property(e => e.Idicd10)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDicd10");

                entity.Property(e => e.Opis)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("opis");
            });

            modelBuilder.Entity<Kartachoroby>(entity =>
            {
                entity.HasKey(e => e.Idkarty)
                    .HasName("PK__KARTACHO__87BD17976C8AF2DA");

                entity.ToTable("KARTACHOROBY");

                entity.Property(e => e.Idkarty).HasColumnName("IDkarty");

                entity.Property(e => e.IDpacjenta).HasColumnName("iDpacjenta");

                entity.Property(e => e.Idicd10)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IDicd10")
                    .HasDefaultValueSql("('brak info')");

                entity.HasOne(d => d.IDpacjentaNavigation)
                    .WithMany(p => p.Kartachorobies)
                    .HasForeignKey(d => d.IDpacjenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KARTACHOROBY_PACJENT");

                entity.HasOne(d => d.Idicd10Navigation)
                    .WithMany(p => p.Kartachorobies)
                    .HasForeignKey(d => d.Idicd10)
                    .HasConstraintName("FK_KARTACHOROBY_ICD10");
            });

            modelBuilder.Entity<Pacjent>(entity =>
            {
                entity.HasKey(e => e.Idpacjenta)
                    .HasName("PK__PACJENT__B0742EBF513E711C");

                entity.ToTable("PACJENT");
                modelBuilder.Entity<Pacjent>().ToTable(tb => tb.HasTrigger("create_100sqd"));

                entity.Property(e => e.Idpacjenta).HasColumnName("IDpacjenta");

                entity.Property(e => e.Imie)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("imie")
                    .HasDefaultValueSql("('brak imienia')");

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nazwisko")
                    .HasDefaultValueSql("('brak nazwiska')");
            });

            modelBuilder.Entity<Skład>(entity =>
            {

                entity.HasKey(e => e.Porzadkowa)
                    .HasName("PK_SKŁAD_123");
          
              

                entity.ToTable("SKŁAD");
                entity.Property(e => e.Porzadkowa).HasColumnName("Porzadkowa"); 
                entity.HasIndex(e => new { e.Idpracownika, e.Dzieńpracy }, "UK_SKŁAD")
                    .IsUnique();

                entity.Property(e => e.Dzieńpracy)
                    .HasColumnType("date")
                    .HasColumnName("dzieńpracy")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Idpracownika).HasColumnName("IDpracownika");

                entity.Property(e => e.Idskład).HasColumnName("IDskład");
                entity.Property(e => e.Typskładu)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("typskładu");
                   

                entity.HasOne(d => d.IdpracownikaNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.Idpracownika)
                    .HasForeignKey(d => d.Idpracownika)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SKŁAD_AspNetUsers");

                entity.HasOne(d => d.Grafik)
                    .WithMany()
                    .HasPrincipalKey(p => new { p.Idskład, p.Dzieńdyżuru })
                    .HasForeignKey(d => new { d.Idskład, d.Dzieńpracy })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SKŁAD_GRAFIK");
            });

            modelBuilder.Entity<Szpital>(entity =>
            {
                entity.HasKey(e => e.Idszpitala)
                    .HasName("PK__SZPITAL__096A7191E5F76810");

                entity.ToTable("SZPITAL");

                entity.Property(e => e.Idszpitala).HasColumnName("IDszpitala");

                entity.Property(e => e.Adres)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adres")
                    .HasDefaultValueSql("('brak adresu')");

                entity.Property(e => e.Nazwa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nazwa")
                    .HasDefaultValueSql("('placówka spoza PRM')");
            });

            modelBuilder.Entity<VChorobyBezdomni>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vCHOROBY_BEZDOMNI");

                entity.Property(e => e.DataInterwencji)
                    .HasColumnType("date")
                    .HasColumnName("Data_interwencji");

                entity.Property(e => e.Idzgłoszenia).HasColumnName("IDzgłoszenia");

                entity.Property(e => e.JednostkaChorobowa)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Jednostka_chorobowa");

                entity.Property(e => e.NumerZałogi).HasColumnName("Numer_Załogi");

                entity.Property(e => e.Pacjent)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PACJENT");

                entity.Property(e => e.SzpitalDocelowy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Szpital_docelowy");
            });

            modelBuilder.Entity<VChorobyWszyscy>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vCHOROBY_WSZYSCY");

                entity.Property(e => e.DataInterwencji)
                    .HasColumnType("date")
                    .HasColumnName("Data_interwencji");

                entity.Property(e => e.Idzgłoszenia).HasColumnName("IDzgłoszenia");

                entity.Property(e => e.JednostkaChorobowa)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Jednostka_chorobowa");

                entity.Property(e => e.NumerZałogi).HasColumnName("Numer_Załogi");

                entity.Property(e => e.Pacjent)
                    .HasMaxLength(41)
                    .IsUnicode(false)
                    .HasColumnName("PACJENT");

                entity.Property(e => e.SzpitalDocelowy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Szpital_docelowy");
            });

            modelBuilder.Entity<VDaneWyjazdSexualAbuse>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDANE_WYJAZD_SEXUAL_ABUSE");

                entity.Property(e => e.DataInterwencji)
                    .HasColumnType("date")
                    .HasColumnName("Data_interwencji");

                entity.Property(e => e.JednostkaChorobowa)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Jednostka_chorobowa");

                entity.Property(e => e.NumerZałogi).HasColumnName("Numer_Załogi");

                entity.Property(e => e.Pacjent)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PACJENT");

                entity.Property(e => e.SzpitalDocelowy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Szpital_docelowy");
            });

            modelBuilder.Entity<VHistoriaTransportu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vHISTORIA_TRANSPORTU");

                entity.Property(e => e.DataInterwencji)
                    .HasColumnType("date")
                    .HasColumnName("Data_interwencji");

                entity.Property(e => e.JednostkaChorobowa)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Jednostka_chorobowa");

                entity.Property(e => e.NumerZałogi).HasColumnName("Numer_Załogi");

                entity.Property(e => e.Pacjent)
                    .HasMaxLength(41)
                    .IsUnicode(false)
                    .HasColumnName("PACJENT");

                entity.Property(e => e.SzpitalDocelowy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Szpital_docelowy");
            });

            modelBuilder.Entity<VIlIntPracownikSklad>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vIL_INT_PRACOWNIK_SKLAD");

                entity.Property(e => e.AgnieszkaKozierak).HasColumnName("Agnieszka Kozierak");

                entity.Property(e => e.AleksandraLewak).HasColumnName("Aleksandra Lewak");

                entity.Property(e => e.AnatolIgorek).HasColumnName("Anatol \r\nIgorek");

                entity.Property(e => e.AndrzejMuc).HasColumnName("Andrzej Muc");

                entity.Property(e => e.DanielKaza).HasColumnName("Daniel \r\nKaza");

                entity.Property(e => e.Idskład).HasColumnName("IDskład");

                entity.Property(e => e.IgorTuszewski).HasColumnName("Igor Tuszewski");

                entity.Property(e => e.IwonaKostelnik).HasColumnName("Iwona \r\nKostelnik");

                entity.Property(e => e.JadwigaJanda).HasColumnName("Jadwiga Janda");

                entity.Property(e => e.JadwigaStaniszkis).HasColumnName("Jadwiga Staniszkis");

                entity.Property(e => e.JarosławNiedbała).HasColumnName("Jarosław \r\nNiedbała");

                entity.Property(e => e.JoannaOsuch).HasColumnName("Joanna Osuch");

                entity.Property(e => e.JózefKolmosiak).HasColumnName("Józef Kolmosiak");

                entity.Property(e => e.KamilLewański).HasColumnName("Kamil Lewański");

                entity.Property(e => e.KamilaLewańska).HasColumnName("Kamila Lewańska");

                entity.Property(e => e.KatarzynaKazara).HasColumnName("[Katarzyna Kazara");

                entity.Property(e => e.KatarzynaZielińska).HasColumnName("Katarzyna \r\nZielińska");

                entity.Property(e => e.KrystianNorwid).HasColumnName("[Krystian Norwid");

                entity.Property(e => e.KrystynaLubecka).HasColumnName("Krystyna Lubecka");

                entity.Property(e => e.KrystynaPawłowicz).HasColumnName("Krystyna Pawłowicz");

                entity.Property(e => e.MariuszTuszela).HasColumnName("Mariusz Tuszela");

                entity.Property(e => e.MartynaGołojuch).HasColumnName("Martyna Gołojuch");

                entity.Property(e => e.MatueszGładysz).HasColumnName("Matuesz Gładysz");

                entity.Property(e => e.MichałMarlo).HasColumnName("Michał Marlo");

                entity.Property(e => e.OlgaStanior).HasColumnName("Olga Stanior");

                entity.Property(e => e.PaulBorubar).HasColumnName("Paul Borubar");

                entity.Property(e => e.PawełBednarz).HasColumnName("Paweł\r\nBednarz");

                entity.Property(e => e.PawełJanus).HasColumnName("Paweł Janus");

                entity.Property(e => e.PawełKrystek).HasColumnName("Paweł Krystek");

                entity.Property(e => e.PiotrDalejko).HasColumnName("Piotr Dalejko");

                entity.Property(e => e.PrzemysławaZadybka).HasColumnName("Przemysława Zadybka");
            });

            modelBuilder.Entity<VIlInterwencjiPracownika>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vIL_INTERWENCJI_PRACOWNIKA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IleInterwencji).HasColumnName("Ile_interwencji");
            });

            modelBuilder.Entity<VIlPracyPracownika>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vIL_PRACY_PRACOWNIKA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IlePracował).HasColumnName("ILE_pracował");
            });

            modelBuilder.Entity<VIlZgWgdatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vIL_ZG_WGDATA");

                entity.Property(e => e.Data).HasColumnType("date");

                entity.Property(e => e.IleZgłoszeń).HasColumnName("ILE_zgłoszeń");
            });

            modelBuilder.Entity<VIleRazyPacjent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vILE_RAZY_PACJENT");

                entity.Property(e => e.DataInterwencji).HasColumnName("Data_interwencji");

                entity.Property(e => e.IleRazy).HasColumnName("ile_Razy");

                entity.Property(e => e.NazwiskoPacjenta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Nazwisko_pacjenta");
            });

            modelBuilder.Entity<VKtoPracowal>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vKTO_PRACOWAL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IlePracował).HasColumnName("ILE_pracował");

                entity.Property(e => e.Status)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<VListazgłoszeń>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vLISTAZGŁOSZEŃ");

                entity.Property(e => e.DataZgłoszenia)
                    .HasColumnType("date")
                    .HasColumnName("Data_Zgłoszenia");

                entity.Property(e => e.IdSkładuPrm).HasColumnName("ID_składu_PRM");

                entity.Property(e => e.IdZgłoszenia).HasColumnName("ID_zgłoszenia");

                entity.Property(e => e.ImiePacjenta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Imie_pacjenta");

                entity.Property(e => e.NazwiskoPacjenta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Nazwisko_pacjenta");
            });

            modelBuilder.Entity<VObsadaStanowiskWgdaty>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vOBSADA_STANOWISK_WGDATY");

                entity.Property(e => e.DzieńPracy)
                    .HasColumnType("date")
                    .HasColumnName("DZIEŃ_PRACY");

                entity.Property(e => e.LiczbaPracownikówNaDniu).HasColumnName("LICZBA_PRACOWNIKÓW_NA_DNIU");

                entity.Property(e => e.NazwaStanowiska)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NAZWA_STANOWISKA");
            });

            modelBuilder.Entity<VPracownicyWgdatyUzytoRezerwa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vPRACOWNICY_WGDATY_UZYTO_REZERWA");

                entity.Property(e => e.DzieńPracy)
                    .HasColumnType("date")
                    .HasColumnName("DZIEŃ_PRACY");

                entity.Property(e => e.LiczbaPracownikówNaDniu).HasColumnName("LICZBA_PRACOWNIKÓW_NA_DNIU");

                entity.Property(e => e.NazwaStanowiska)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NAZWA_STANOWISKA");
            });

            modelBuilder.Entity<VRezerwa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vREZERWA");

                entity.Property(e => e.DzieńPracy)
                    .HasColumnType("date")
                    .HasColumnName("DZIEŃ_PRACY");
            });
            modelBuilder.Entity<ViewAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewAll");
                
                entity.Property(e => e.Przyjecie)
                    .HasColumnType("DATE")
                    .HasColumnName("Przyjecie");

                entity.Property(e => e.choroba)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("choroba");

                entity.Property(e => e.imie)
                   .HasMaxLength(20)
                   .IsUnicode(true)
                   .HasColumnName("imie");

                entity.Property(e => e.nazwisko)
                    .HasMaxLength(20)
                    .IsUnicode(true)
                    .HasColumnName("nazwisko");
            });

            modelBuilder.Entity<ViewWord>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewWord");

                entity.Property(e => e.Przyjecie)
                    .HasColumnType("DATE")
                    .HasColumnName("Przyjecie");

                entity.Property(e => e.choroba)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("choroba");

                entity.Property(e => e.imie)
                   .HasMaxLength(20)
                   .IsUnicode(true)
                   .HasColumnName("imie");

                entity.Property(e => e.nazwisko)
                    .HasMaxLength(20)
                    .IsUnicode(true)
                    .HasColumnName("nazwisko");

                entity.Property(e => e.nazwa_szpitala)
                    .HasMaxLength(20)
                    .IsUnicode(true)
                    .HasColumnName("nazwa_szpitala");


                entity.Property(e => e.szczegóły_pogotowia)
                   .HasMaxLength(50)
                   .IsUnicode(true)
                   .HasColumnName("szczegóły_pogotowia");
            });


            modelBuilder.Entity<ViewCrew>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewCrew");
                entity.Property(e => e.ID_składu).HasColumnName("ID_składu");

                entity.Property(e => e.Dzień_pracy)
                    .HasColumnType("DATE")
                    .HasColumnName("Dzień_pracy");

                entity.Property(e => e.Imie_Nazwisko)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Imie_Nazwisko");

                entity.Property(e => e.Log_in)
                   .HasMaxLength(256)
                   .IsUnicode(true)
                   .HasColumnName("Log_in");

                entity.Property(e => e.stanowisko)
                    .HasMaxLength(10)
                    .IsUnicode(true)
                    .HasColumnName("stanowisko");

                entity.Property(e => e.typ_składu)
                    .HasMaxLength(1)
                    .IsUnicode(true)
                    .HasColumnName("typ_składu");
            });
                modelBuilder.Entity<VStatystykaPrmZałóg>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vSTATYSTYKA_PRM_ZAŁÓG");

                entity.Property(e => e.JednostkaChorobowa)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Jednostka_chorobowa");

                entity.Property(e => e.LiczbaInterwencji).HasColumnName("liczba_interwencji");

                entity.Property(e => e.NumerZałogi).HasColumnName("Numer_załogi");

                entity.Property(e => e.SzpitalDocelowy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Szpital_docelowy");
            });

            modelBuilder.Entity<Zgłoszenie>(entity =>
            {
                entity.HasKey(e => e.Idzgłoszenia)
                    .HasName("PK__ZGŁOSZEN__067A62784F09EB07");
              
                entity.ToTable("ZGŁOSZENIE");
                modelBuilder.Entity<Zgłoszenie>().ToTable(tb => tb.HasTrigger("change_status1"));
                modelBuilder.Entity<Zgłoszenie>().ToTable(tb => tb.HasTrigger("change_status0"));
                entity.HasIndex(e => new { e.Idzgłoszenia, e.Datazgłoszenia }, "UK_ZGŁOSZENIE")
                    .IsUnique();

                entity.Property(e => e.Idzgłoszenia).HasColumnName("IDzgłoszenia");

                entity.Property(e => e.Danezgłoszenia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("danezgłoszenia")
                    .HasDefaultValueSql("('brak opisu')");

                entity.Property(e => e.Datazgłoszenia)
                    .HasColumnType("datetime")
                    .HasColumnName("datazgłoszenia")
                    .HasDefaultValueSql("(getdate())"); 

                entity.Property(e => e.Datazamkniecia)
                   .HasColumnType("datetime") 
                   .HasColumnName("datazamkniecia")
                   .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Datarejestracji)
                 .HasColumnType("datetime") 
                 .HasColumnName("datarejestracji")
                 .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Idkartychoroby).HasColumnName("IDkartychoroby");

                entity.Property(e => e.Idpacjenta)
                    .HasColumnName("IDpacjenta")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Idskład).HasColumnName("IDskład");

                entity.Property(e => e.Idszpitala)
                    .HasColumnName("IDszpitala")
                    .HasDefaultValueSql("('0')");

                entity.HasOne(d => d.IdkartychorobyNavigation)
                    .WithMany(p => p.Zgłoszenies)
                    .HasForeignKey(d => d.Idkartychoroby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZGŁOSZENIE_KARTACHOROBY");

                entity.HasOne(d => d.IdpacjentaNavigation)
                    .WithMany(p => p.Zgłoszenies)
                    .HasForeignKey(d => d.Idpacjenta)
                    .HasConstraintName("FK_ZGŁOSZENIE_PACJENT");

                entity.HasOne(d => d.IdszpitalaNavigation)
                    .WithMany(p => p.Zgłoszenies)
                    .HasForeignKey(d => d.Idszpitala)
                    .HasConstraintName("FK_ZGŁOSZENIE_SZPITAL");

                entity.HasOne(d => d.Grafik)
                    .WithMany(p => p.Zgłoszenies)
                    .HasPrincipalKey(p => new { p.Idskład, p.Dzieńdyżuru })
                    .HasForeignKey(d => new { d.Idskład, d.Datazgłoszenia })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GRAFIK_ZGŁOSZENIE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
