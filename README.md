# Autorizace a Autentifikace v .NET (webová aplikace) za pomoci Keycloak

## Postup pro nastavení Keycloak
1. Nainstalovat [Docker](https://www.docker.com).
2. Do terminálu zadat příkaz ```docker run -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:21.1.1 start-dev```
3. Přihlásit se na [zde](http://localhost:8080) pomocí přihlašovacích údajů jméno = admin, heslo = admin.
4. Rozbalit dropdown s nápisem master.
5. Kliknout na Create realm.
6. Do pole Resource file nahrát soubor BP-realm.json a uložit.

## Uživatelé
### Vytvoření uživatele
1. V hlavním menu zvolíme Users.
2. Najdeme zde již existující uživatele a tlačítko Add user.
3. Klikneme na tlačítko Add user, vyplníme uživatelské údaje a klikneme na tlačítko Create.
4. Po přesměrování na detail uživatele vybereme záložku Credentials.
5. Klikneme na tlačítko Set password.
6. Zadáme heslo, zopakujeme heslo pro kontrolu a zvolíme, zda je heslo dočasné, či nikoliv.
7. Klikneme na Save.

### Přidání role
1. Z výpisu uživatelů zvolíme požadovaného uživatele.
2. Vybereme položku Role mappings.
3. Zde vidíme seznam rolí, ke kterým je uživatel přiřazen a tlačítko assign role.
4. Klikneme na tlačítko Assign role.
5. Vybereme ze seznamu role, které chceme uživateli přiřadit.
6. Klikneme na Assign.

### Přiřazení do skupiny
1. Z výpisu uživatelů zvolíme požadovaného uživatele.
2. Vybereme položku Groups.
3. Zde vidíme seznam skupin, jejíchž se uživatel členem a tlačítko Join group.
4. Klikneme na tlačítko Join group.
5. Vybereme ze seznamu skupiny, do kterých chceme uživatele přiřadit.
6. Klikneme na tlačítko Join.


## Role
### Forma rolí používaných v demonstračních aplikacích
- EditSubjects - možnost editovat všechny parametry entity Subject.
- EditSubjects.Description - možnost editovat pouze parametr Description.
- ReadSubjects - možnost číst všechny parametry entity Subject.

### Vytvoření role na úrovni realmu
1. V hlavním menu zvolíme Realm roles.
2. Najdeme zde již existující role a tlačítko Create role.
4. Po stisknutí tlačítka Create role vyplníme jméno a můžeme přidat popisek role.

### Vytvoření role na úrovni aplikace
1. V hlavním menu zvolíme Clients.
2. Ze seznamu aplikací vybereme aplikace, kde chceme vytvořit novou roli.
3. Pod zálkožkou Roles najdeme existující role a tlačítko Create role.
4. Po stisknutí tlačítka Create role vyplníme jméno a můžeme přidat popisek role.

## Skupiny
### Vytvoření skupiny
1. V hlavním menu vybereme položku Groups.
2. V sloupci vedle menu vidíme výpis skupin (včetně hierchie) a ve vedlejší sekci výpis skupin nejvyšší úrovně dle hiearchie a tlačítko Create group.
3. Klikneme na tlačítko Create group, vyplníme název skupiny a uložíme.

### Vytvoření podskupiny
1. Z výpisu skupin vybereme skupinu.
2. Vybereme položku Child groups.
3. Klikneme na tlačítko Create group, vyplníme název a uložíme.

### Přidání uživatelů
1. Z výpisu skupin vybereme skupinu.
2. Vybereme položku Members.
3. Klikneme na tlačítko Add member.
4. Z nabídky vybereme uživatele, které chceme do skupiny přidat a klikneme na Add.

### Přidání role
1. Z výpisu skupin vybereme skupinu.
2. Vybereme položku Role mapping.
3. Klikneme na tlačítko Assign role.
4. Z nabídky vybereme role, které chceme do skupiny přidat a klikneme na Assign.

## Webová aplikace
### Přihlášení a zobrazení tabulky předmětů
1. Po spuštění webové aplikace ji najdete [zde](https://localhost:7161).
2. V horním menu klikněte na položku Subjects.
3. V případě, že nejste přihlášení budete přesměrování na Keycloak.
4. Zadejte přihlašovací údaje jednoho, z uživatelů, kteří jsou vytvoření v Keycloak realmu BP.
5. Po úspěšném ověření pomocí přihlašovacích údajů budete vyzváni k nastavení OTP. Pokud máte OTP pro daného uživatele nastavené, pokračujte bodem 7.
6. K nastavení OTP použijte na svém mobilním zařízení aplikaci Google Authenticator, nebo FreeOTP.
7. Zadejte OTP, které se Vám vygenerovalo ve Vámi nastavené aplikaci ve vašem mobilním zařízení.
8. Po úspěšném přihláníšení jste přesměrování na další stránku 
   - V případě, že uživatel má oprávnění pro čtení předmětů (ReadSubjects), bude Vám zobrazena stránka s předměty.
   - V případě, že uživatel nemá roli pro mazání, budete přesměrování na stránku s nápisem Access Denied (přístup odepřen). V případě, že chcete navštívit požadovanou stránku, přihlašte se jako jiný uživatel, nebo současnému uživateli přidělte roli pro zobrazení předmětů (ReadSubjects), odhlašte se z aplikace a přihlaste se znovu.

### Vytvoření předmětu
1. Po úspěšném přihlášení a zobrazení tabulky předmětů vidíte jednotlivé předměty a vedle jednotlivých předmětů tlačítka Edit a Delete.
   - V případě, že uživatel nemá roli pro vytváření předmětů, tlačítko Create New Subject nebude možné stisknout.
   - V případě, že má uživatel roli pro vytváření předmětů (CreateSubjects), můžete kliknout na tlačítko Create New Subject. Pokračujte bodem 2.
2. Klikněte na tlačítko Create New Subject
3. Budete předměrování na stránku pro vytváření předmětů.
4. Do textových polí doplňte název předmětu a jeho popisek.
5. Klikněte na tlačítko Save Subject.
6. Po úspěšném vytvoření předmětu budete přesměrování na stránku detailu daného předmětu.
  

### Mazání předmětů
1. Po úspěšném přihlášení a zobrazení tabulky předmětů vidíte jednotlivé předměty a vedle jednotlivých předmětů tlačítka Edit a Delete.
   - V případě, že má uživatel roli pro mazání předmětů (DeleteSubjects), můžete kliknout na tlačítko Delete u zvoleného předmětu a tím ho smazat.
   - V případě, že uživatel nemá roli pro mazání, tlačítko Delete nebude možné stisknout.

### Úprava předmětů
1. Po úspěšném přihlášení a zobrazení tabulky předmětů vidíte jednotlivé předměty a vedle jednotlivých předmětů tlačítka Edit a Delete.
   - V případě, že uživatel nemá roli pro úpravu předmětů, tlačítko Edit nebude možné stisknout.
   - V případě, že má uživatel roli pro úpravu (EditSubjects), nebo jakoukoliv její variantu (EditSubjects.Name), můžete kliknout na tlačítko Edit, které Vás přesměruje na detail předmětu. Pokračujte bodem 2.
2. Po zobrazení detailu předmětu vidíte textová pole, která obsahují název a popisek předmětu.
3. V případě, že uživatel nemá dostatečnou roli pro úpravu daného parametru (např. Name), bude dané textové pole zašedlé a text v něm obsažený nebude možno upravit.
4. Po provedení požadovaných úprav klikněte na tlačítko Save a tím se uloží provedené úpravy.
