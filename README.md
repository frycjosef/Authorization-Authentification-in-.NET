# Autorizace a Autentifikace v .NET (webová aplikace) za pomoci Keycloak

## Postup pro nastavení Keycloak
1. Nainstalovat [Docker](https://www.docker.com)
2. Do terminálu zadat příkaz ```docker run -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:21.1.1 start-dev```
3. Přihlásit se na [zde](http://localhost:8080) pomocí přihlašovacích údajů jméno = admin, heslo = admin
4. Rozbalit dropdown s nápisem master
5. Kliknout na Create realm
6. Do pole Resource file nahrát soubor BP-realm.json a uložit

## Uživatelé
### Vytvoření uživatele
1. V hlavním menu zvolíme Users
2. Najdeme zde již existující uživatele a tlačítko Add user
3. Klikneme na tlačítko Add user, vyplníme uživatelské údaje a klikneme na tlačítko Create
4. Po přesměrování na detail uživatele vybereme záložku Credentials
5. Klikneme na tlačítko Set password
6. Zadáme heslo, zopakujeme heslo pro kontrolu a zvolíme, zda je heslo dočasné, či nikoliv
7. Klikneme na Save

### Přidání role
1. Z výpisu uživatelů zvolíme požadovaného uživatele
2. Vybereme položku Role mappings
3. Zde vidíme seznam rolí, ke kterým je uživatel přiřazen a tlačítko assign role
4. Klikneme na tlačítko Assign role
5. Vybereme ze seznamu role, které chceme uživateli přiřadit
6. Klikneme na Assign

### Přiřazení do skupiny
1. 1. Z výpisu uživatelů zvolíme požadovaného uživatele
2. Vybereme položku Groups
3. Zde vidíme seznam skupin, jejíchž se uživatel členem a tlačítko Join group
4. Klikneme na tlačítko Join group
5. Vybereme ze seznamu skupiny, do kterých chceme uživatele přiřadit
6. Klikneme na tlačítko Join


## Role
### Forma rolí používaných v demonstračních aplikacích
- EditSubjects - možnost editovat všechny parametry entity Subject
- EditSubjects.Description - možnost editovat pouze parametr Description
- ReadSubjects - možnost číst všechny parametry entity Subject

### Vytvoření role na úrovni realmu
1. V hlavním menu zvolíme Realm roles
2. Najdeme zde již existující role a tlačítko Create role
4. Po stisknutí tlačítka Create role vyplníme jméno a můžeme přidat popisek role

### Vytvoření role na úrovni aplikace
1. V hlavním menu zvolíme Clients
2. Ze seznamu aplikací vybereme aplikace, kde chceme vytvořit novou roli
3. Pod zálkožkou Roles najdeme existující role a tlačítko Create role
4. Po stisknutí tlačítka Create role vyplníme jméno a můžeme přidat popisek role

## Skupiny
### Vytvoření skupiny
1. V hlavním menu vybereme položku Groups
2. V sloupci vedle menu vidíme výpis skupin (včetně hierchie) a ve vedlejší sekci výpis skupin nejvyšší úrovně dle hiearchie a tlačítko Create group
3. Klikneme na tlačítko Create group, vyplníme název skupiny a uložíme

### Vytvoření podskupiny
1. Z výpisu skupin vybereme skupinu
2. Vybereme položku Child groups
3. Klikneme na tlačítko Create group, vyplníme název a uložíme

### Přidání uživatelů
1. Z výpisu skupin vybereme skupinu
2. Vybereme položku Members
3. Klikneme na tlačítko Add member
4. Z nabídky vybereme uživatele, které chceme do skupiny přidat a klikneme na Add

### Přidání role
1. Z výpisu skupin vybereme skupinu
2. Vybereme položku Role mapping
3. Klikneme na tlačítko Assign role
4. Z nabídky vybereme role, které chceme do skupiny přidat a klikneme na Assign

## Webová aplikace
