Instrukcja korzystania z zasobów. 

Zawartość repozytorium AZRM, zawiera:
1. /pominięto/
2. pliki .sql:
	- w których znajdują się skrypty bazy danych SWD2 - będącej bazą danych AZRM;

3. /pominięto/

4. plik .xls:
	- w którym znajduje się słownik z danymi dla klasy ICD10 w AZRM; 

5. Folder AZRM2023v1:
	- w którym znajduje się pliki źródłowe AZRM;

5.1. Sposób instalacji:
	- należy na serwerze/lub dysku lokalnym utworzyć bazę danych SWD zgodnie ze skryptami z pkt. 2,
	  wg kolejności ScryptDataBase, StoredProcedures, Functions, Triggers;
		- dane słownikowe ICD-10.xls można wgrać poprzez użycie MS SQL SMS;
		- wgrać plik z pkt. 3 do bazy danych SWD2 poprzez użycie MS SQL SMS;
	- w pliku źródłowym z pkt. 5 - 'appsettings.json' - w pozycji // connectingString: "conn":<należy wpisać obowiązujący connecting string> // dla nowopowstałej bazy 	  	  danych SWD2; 
	- pliki źródłowe zawarte w folderze AZRM2023v1, należy uruchomić w MS Visual Studio 2022 poprzez uruchomienie pliku typu visual studio solution : 'AZRM2023v1.sln'.


Repozytorium AZRM nie zawiera: 
 Plików.pbix:
	- w których znajdują się źródła dla raportów Power BI wykorzystywanych w AZRM, z uwagi na dostęp do danych drażliwych dla osób nieupoważnionych. 

pliku .bak:
	- w którym znajduje się BackUp Bazy danych, celem ułatwienia prezentacji AZRM2023v1, z uwagi na dostęp do danych drażliwych


5.2 Sposób logowania do AZRM (użycie Login i UserName):
	- dla celów akademickich w Tabeli 'AspNetUsers', istnieje odhaszowana kolumna - 'Password' zawierająca 'Pierwsze Hasło' dla danego konta, kolejne zmiany hasła nie 	  będą przedstawione w powyższej kolumnie, login to Username z wspomnianej tabeli.



	
