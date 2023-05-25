# Autorizace a Autentifikace v .NET (webová aplikace) za pomoci Keycloak

## Postup pro nastavení Keycloak
1. Nainstalovat [Docker](https://www.docker.com)
2. Do terminálu zadat příkaz ```docker run -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:21.1.1 start-dev```
3. Přihlásit se na [zde]([https://www.docker.com](http://localhost:8080)) pomocí přihlašovacích údajů jméno = admin, heslo = admin
4. Rozbalit dropdown s nápisem master
5. Kliknout na Create realm
6. Do pole Resource file nahrát soubor BP-realm.json a uložit
