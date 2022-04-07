# API Documentatie

Dit document heeft als bedoeling om een duidelijk overzicht te scheppen wat er juist mogelijk is met de API.

Hier onder kan u het ER schema bekijken ter verduidelijking:

![alt text](img/ERDcloud.PNG "Aangepaste ER Schema")

## Users

#### Functionaliteit

Zoals je kan zien in het ER schema kan een team meerdere users hebben. Deze users kunnen via een sign-up via de site of via de app in een team terecht komen. Verder kunnen users zich aanmelden op de site en het spel om verschillende dingen te gaan doen.

#### API Requests

Een gebruiker kan zich makkelijk registreren ( sign-up ) via de route:

api/users3/

Method : POST
Request-body: user-object

---

Een gebruiker kan zich na een sign-up aanmelden via de route:

api/users3/login

Method : POST
Request-body: user-object (enkel email en passwoord volstaan)

---

Het is mogelijk om voor test-purposes alle users die geregistreerd zijn op te halen. Bij een concrete uitrol kan je deze methode natuurlijk weglaten. Je kan dit bekijken via de route:

api/users3

Method : GET
Request-body: none

---

Het is mogelijk om een gebruiker op te halen aan de hand van zijn email adress.

api/users3/email

Method : GET
Request-body: email adres van de user die je wilt opvragen (string email)

---

Verder is het mogelijk om een user aan te passen (momenteel niet mogelijk via site of app, maar ik heb de functionaliteit)

api/users3/updateuser

Method : PUT
Request-body: eender welk veld van het user object kan geüpdatet worden

---

Het is mogelijk om de locatie van een user aan te passen (momenteel niet mogelijk via app, maar ik heb de functionaliteit, initieel multiplayer in gedachten)

api/users3/updateloc

Method : PUT
Request-body: Enkel de Lat en Long velden van het user object kunnen geüpdatet worden, je geeft minimaal de username en email ook mee

---

Tot slot is het mogelijk om een bestaande user te verwijderen

api/users3

Method : DELETE
Request-body: Je geeft minimaal de username en email mee

## Teams

#### Functionaliteit

De teams directory van de API biedt de mogelijkheid om teams op te halen, aan te maken en gegevens over de teams op te vragen.

Teams worden aangemaakt via de bijgeleverde site. Als een user is ingelogt en een team aanmaakt, komt hij automatisch in een team. Vervolgens heeft deze gebruiker de mogelijkheid om vragen voor het team aan te maken.

Verder krijgt een user, als een bepaalde user bestaat, de mogelijkheid om een team te joinen. Dit gebeurd via de TeamId property van de user. Als deze een team gekozen heeft kan de user geupdate worden met de desbetreffende TeamId. Deze tabellen zijn verder in de database gelinkt met elkaar. Zo heeft tbl_users een foreign key die gelinkt is met de primary key van de tbl_teams.

Via de site is het ten slotte nog mogelijk om abilities voor een team toe te voegen. Deze kunnen obstakels bevatten (bom) of een power-up. Hiertussen wordt gedifferentieerd door middel van de AbilityType property.

Als alle vragen en optioneel abilities gemaakt zijn voor een team, kunnen andere gebruikers inloggen op de spel-app die gemaakt is. Vervolgens kan de user een team kiezen uit een lijst (e.g. het team dat was aangemaakt op de site)

#### API Requests

Een team(naam) kan makkelijk aangemaakt worden via de route:

api/teams3

Method : POST
Request-body: teamnaam

---

Alle teams kunnen voor en overzicht makkelijk worden opgehaald via de route:

api/teams3

Method : GET
Request-body: none

---

Alle users in een team kunnen opgezocht worden via de route:

api/teams3/users/{int teamid}

Method : GET
Request-body: none

---

Alle vragen voor een team kunnen opgezocht worden via de route:

api/teams3/vragen/{int teamid}

Method : GET
Request-body: none

---

Alle abilities voor een team kunnen opgezocht worden via de route:

api/teams3/ability/{int teamid}

Method : GET
Request-body: none

---

## Vragen

#### Functionaliteit

Via de site is het mogelijk om vragen aan te maken voor een team.

#### API Requests

Er kan een vraag voor een team aangemaakt worden via de route:

api/vragen3

Method : POST
Request-body: vragen-object

---

Verder kunnen alle vragen opgevraagd worden die aangemaakt zijn

api/vragen3

Method : GET
Request-body: none

---

## Highscore

#### Functionaliteit

Als er een user wordt aangemaakt via de site of via de app, wordt er ook automatisch een highscore aangemaakt voor deze user. Het user object is voorzien van de ScoreId property. Deze is gelinkt met de primary key van de tbl_highscore. Op deze manier kan makkelijk de score van een user worden verwerkt.

#### API Requests

Score van een user updateten kan makkelijk via de route:

api/highscore3/users/{int ScoreId}

Method : POST
Request-body: Highscore-object. In dit enkele geval dient de ScoreId property meegegeven te worden.

---

Een highscore van de user kan makkelijk gevonden worden via volgende route:

api/highscore3/users/{int ScoreId}

Method : GET
Request-body: none

---

## Ability

#### Functionaliteit 

Een team(leider) kan abilities toevoegen als power-ups, of obstakels zoals een bom. Deze worden op de map geplaatst via lat en long properties. Tussen een obstakel of powerup wordt gedifferentieerd via de AbilityType property.

#### API Requests

Een ability toevoegen kan makkelijk via de route:

api/ability3

Method : POST
Request-body: Ability object

---

Je kan alle abilities te zien krijgen via:

api/ability3

Method : GET
Request-body: none
