# MyCloudStore
## Projekat iz zaštite informacija 2019 - Branko Simović 16326

### Raspored
MyCloudStoreApp - Klijentska aplikacija  
MyCloudStoreLib - Crypto biblioteka  
MyCloudStoreSvc - WCF servis  
db-init.sql - SQL komande za inicijalizaciju SQLite baze 
Baza mora biti na putanji: C:\mcsdb\mcdsb.db  

### OFB mod pojašnjenje
Integrisan je u funkcijama Encrypt u klasama algoritama.  
Za OFB mod nije potreban algoritam dekripcije bloka, zato sam implementirao samo enkripciju.  
Klijentska aplikacija prosleđuje hash fajla kao vektor inicijalizacije.  

### XTEA i Double transposition pojašnjenje
Pre kriptovanja, ulazni fajl se proširuje nulama da bi bio deljiv brojem bajta u bloku (8 za XTEA, 16 za DT)  
Za oba algoritma, nakon kriptovanja fajl se proširuje sa još 8 bajta koji sadrže dužinu originalnog fajla, da bi se mogao vratiti. (ideja iz MD5 algoritma)  
Za XTEA, ključ se hashira i taj hash se koristi za enkripciju bloka. (taman 128 bita)  
Za DT uzmaju se prvih 4 i drugih 4 bajta iz hasha ključa kao ključ kolona i ključ vrsta (budući da je blok 16 bajta).  