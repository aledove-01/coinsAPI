# Proyecto: API REST de Criptomonedas

Este proyecto consiste en una API REST desarrollada en C# y .NET 7 que accede a la API de Coinmarketcap y expone los datos a través de un conjunto de endpoints. 
La API permite realizar peticiones utilizando los verbos HTTP GET para obtener información sobre criptomonedas, tasas de cambio y otra información relevante.

La API se comunica con la API de Coinmarketcap utilizando una clave de API proporcionada por Coinmarketcap. Esta clave se utiliza para autenticar las peticiones y asegurar que solo se realizan peticiones válidas.

## Los endpoints disponibles en la API incluyen:

/API/AccessDataCryptoCoins/TopCoins: Este endpoint permite obtener las criptomonedas ordenadas por su valoracion de capitalizacion de mercado. Por defecto devuelve el top 25.

/API/AccessDataCryptoCoins/PriceCoins: Este endpoint permite obtener el precio actual de las criptomonedas enviadas como paramtro.

/API/AccessDataCryptoCoins/MetadataCoins: Este endpoint permite obtener la metadata de las criptomonedas enviadas como parametro.

Cada endpoint devuelve una respuesta en formato JSON con información relevante sobre las criptomonedas solicitadas.

La API está diseñada para ser utilizada por un cliente web, que puede consumir los datos devueltos por los endpoints y utilizarlos para mostrar información sobre criptomonedas en un sitio web o aplicación.

Para utilizar la API, es necesario tener una clave de API válida de Coinmarketcap. Una vez obtenida, se pueden realizar peticiones a los endpoints utilizando el verbo GET y proporcionando la clave de API en la cabecera de la petición (debera modificarse en appsettings.json).

Se utiliza el patrón de Repositorio para encapsular la lógica de acceso a la API de CoinMarketCap en una clase separada, permitiendo mayor flexibilidad y facilidad de mantenimiento en caso de que sea necesario hacer cambios en el futuro sobre la fuente de datos.

Este proyecto es una muestra de cómo utilizar una API externa para obtener información relevante y exponerla a través de una interfaz fácil de usar para clientes web.
