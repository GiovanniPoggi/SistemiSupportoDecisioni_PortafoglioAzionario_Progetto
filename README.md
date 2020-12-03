# Sistemi di Supportoalle Decisioni - Portafoglio Azionario - Progetto 2020

# Come Buildare il progetto su Visual Studio Code

- Aprire un Nuovo Terminale
- dotnet build

# Avvio Server del Progetto

- Aprire un Nuovo Terminale (New Terminal)
- Spostarsi nella Cartella principale del Progetto (Quella sotto Client)
- Eseguire il Comando "dotnet run"

# Consentire Comunicazione con il Server

- Al primo run, avviare Chrome (Da altri Browser da problemi di certificato https)
- Accedere a "https://localhost:5001/api/ElencoIndici"
- Si visualizzerà un errore legato al certificato, cliccare su "Opzioni Avanzate" e poi su "Continua"

# Avviare il Client

- Aprire nel Browser il file index.html dentro la cartella client/

# Info su possibili Errori

- In Models, PythonRunner.cs usa System.Drawing per utilizzare la classe Bitmap. Questa libreria necessita di essere importata lanciando il seguente comando: "dotnet add package System.Drawing.Common"
- Quando Python legge numeri decimali vuole come delimitatore il "." (punto) e non la virgola. I file .CSV utilizzati in risposta alle GET devono esser scritti secondo la cultura inglese, utilizzando il "." come delimitatore tra parte intera e decimale. Di default dotnet core stabilisce la cultura con cui runnare il programma sulla base del pc che esegue l'applicazione. Per fare override delle info di cultura, andare nel file Startup.cs, nel metodo pubblico "Configure" ed aggiungere le seguenti righe di codice:

```
  var cultureInfo = new CultureInfo("en-US");
  cultureInfo.NumberFormat.CurrencySymbol = "€";

  CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
  CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
```
