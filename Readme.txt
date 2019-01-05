1 - Tecnologie utilizzate
	- .NET core 2.0, web api, con sql server 2014. Per eseguire l'applicazione, seguire i seguenti passi:
		- installare visual studio 2017 (community edition) con .net core sdk (dalla 2 in poi va bene)
		- installare sql server 2014 per l'installazione del db. Per questa parte avrei voluto configurare un servizio cloud (cosi da rendere più facile la vostra verifica),
		ma purtroppo non ho avuto tempo e modo.
	- Una volta installato il software necessario, aprire sql server 2014, creare un nuovo db "GestioneIndirizzi", ed eseguire lo script in allegato.
	- Aprire il progetto in visual studio e cambiare la connection string, nel file application.json (nella versione inviata c'è quella del mio pc)
	
2 Architettura e scelte progettuali
	- Ho identificato 3 entità: tenant, shipping address e billing address. Le tabelle degli indirizzi sono relazionate alla tabella degli utenti mediante foreign key.
	- Lato applicazione ho utilizzato Entity Framework come ORM, per poter gestire, mediante context, tutte le operazioni da effettuare sul db.
	- Tutte le operazioni relative al servizio HTTP sono state effettuate mediante 3 controller
		- TenantsController
		- ShippingAddressesController
		- BillingAddressesController
	- Gestione multitenancy
		- Ho creato un controller "padre", che va a gestire tutte le richieste di indirizzo che arrivano. Ognuna di queste richieste, sarà composta da un header con chiave 
		"X-Tenant-ID" e come valore l'id dell'utente attivato in precedenza (mediante post su TenantsController), che verrà intercettato dal controller di base per verificare se l'utente è autorizzato ad 
		accedere alla risorsa.
	- Per il tenant ho previsto l'inserimento obbligatorio del campo tenant_id, mentre per lo shipping e billing address tutti i campi sono obbligatori ad eccezione del
	del campo "company".
	- Per il punto 8, ho previsto l'inserimento del warning all'interno di una tabella di log, al verificarsi della condizione indicata.
	- Per il punto 10 ho creato due tabelle di storico (per billing e shipping), in modo da salvare il vecchio record prima di modificarlo (patch). Le chiamate
	corrispondenti di storico andranno a recuperare le informazioni direttamente dalle tabelle citate in precedenza.
	- l'applicazione come start point, farà visualizzare l'elenco di tutti i tenants attivi.
3 Esecuzione casi di test
	- ho creato un progetto di test, comprensivo di 3 classi, per permettere l'esecuzione separata di tutta la suite. 
	- Per eseguire i casi di test, ho utilizzato un "db in memory", senza andare a leggere e scrivere sulla base dati esistente, cosi da rendere i test indipendenti.

4 Test api
	- per testare tutte le api prodotte, ho utilizzato postman.