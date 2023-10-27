<h1 align="center"> 🏨 Projeto TrybeHotel 🏨 </h1>

<h2 align="center">Projeto de Back-End em C#</h2>

<h4 align="center">Qual o objetivo deste projeto?</h4>

<p>O projeto TrybeHotel é uma API de back-end desenvolvida em C# para gerenciamento hoteleiro. Ela oferece funcionalidades para autenticação de usuários, gerenciamento de cidades, hotéis, quartos, reservas e geolocalização. A API permite que os usuários obtenham informações detalhadas sobre hotéis, realizem reservas, encontrem hotéis próximos com base em endereços e muito mais.</p>

<details>
<summary>Observações de desenvolvedor:</summary>

<br>
<p>1. O projeto faz uso da arquitetura MSC(Model, Service, Controller) para mais fácil manutenção e escalabilidade de código.</p>
<p>2. O projeto faz uso de Docker, possuindo um arquivo docker-compose que cria e inicia um container com o banco de dados.</p>
<p>3. O projeto faz uso de Entity Framework para facilitar, agilizar e simplificar o processo de requisição junto ao banco de dados.</p>
<p>4. O projeto faz uso de um sistema de autenticação e autorização utilizando JWT(Json Web Token), possuindo permissões de <strong>Cliente</strong> ou <strong>Administrador</strong>.</p>
<br>

</details>

<h2>Aprendizados com este projeto</h2>
<ul>
<li>C#</li>
<li>ASP.NET</li>
<li>Microsoft SQL Server</li>
<li>Entity Framework</li>
<li>Docker</li>
<li>JWT (Json Web Token)</li>
<li>Azure Data Studio</li>
</ul>

------------

<h2>Tabela de Conteúdo</h2>

- [Status](#status)
  - [GET /](#get-)
- [Login](#login)
  - [POST /login](#post-login)
- [City](#city)
  - [GET /city](#get-city)
  - [POST /city](#post-city)
  - [PUT /city](#put-city)
- [Hotel](#hotel)
  - [GET /hotel](#get-hotel)
  - [POST /hotel](#post-hotel)
- [Room](#room)
  - [GET /room](#get-room)
  - [POST /room](#post-room)
  - [DELETE /room](#delete-room)
- [Booking](#booking)
  - [GET /booking](#get-booking)
  - [POST /booking](#post-booking)
- [GeoLocation](#geolocation)
  - [GET /geo/status](#get-geo-status)
  - [GET /geo/address](#get-geo-address)
- [User](#user)
  - [GET /user](#get-user)
  - [POST /user](#post-user)


------------

<h1>Status</h1>

<strong> A funcionalidade de Status retorna uma mensagem informando o status da API. Possui a requisição: GET.</strong>

<details>
<summary>GET /</summary>
<h3>GET /</h3>

<strong>Retorna um objeto com a "message" informando o status da API.</strong>

<strong>Endereço de requisição - [ GET `/` ]</strong>

<h4>Corpo da Requisição:</h4>

Não requer informação no corpo da requisição.

<h3>Respostas da API:</h3>

A resposta segue o formato abaixo com status <code>200</code>:

    { "message": "online" }

</details>
<h1>Login</h1>

<strong>A funcionalidade de login é responsável por autenticar os usuários. Os usuários podem enviar suas credenciais (email e senha) para a API, que verifica essas informações no banco de dados. Se as credenciais estiverem corretas, a API retorna um token de autenticação que será usado para autorizar solicitações subsequentes. Possui a requisição: POST.</strong>

<details>
<summary>POST /login</summary>
<h3>POST /login</h3>

<strong>Endereço de requisição - [ POST `/login` ]</strong>

<strong>Verifica os dados de login e retorna um token de sucesso ou erro.</strong>

<h4>Corpo da Requisição:</h4>

O corpo da requisição deve seguir o formato abaixo:

    {
    	"Email": "rebeca.santos@trybehotel.com",
    	"Password": "123456"
    }

<h3>Respostas da API:</h3>
<details>
<summary><strong>Em caso de login válido (Status 200):</strong></summary>

    {
    	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiYWRtaW4iLCJlbWFpbCI6ImRhbmlsby5zaWx2YUBiZXRyeWJlLmNvbSIsIm5iZiI6MTY4ODQxMTIxMiwiZXhwIjoxNjg4NDk3NjEyLCJpYXQiOjE2ODg0MTEyMTJ9.q1cNj2_xspeQC6Uz1maV79P95hVtWH4Z7auZgOen-Qo",
    }

</details>
<details>
<summary><strong>Em caso de login inválido (Status 401):</strong></summary>

    {
    	"message": "Incorrect e-mail or password"
    }

</details>
</details>
<h1>City</h1>

<strong>A funcionalidade relacionada a cidades permite que os usuários obtenham informações sobre cidades presentes no banco de dados. Além disso, é possível adicionar novas cidades ao banco de dados ou atualizar informações de cidades existentes.. Possui as seguintes requisições: GET, POST e PUT.</strong>

<details>
<summary>GET /city</summary>
<h2>GET /city</h2>

<strong>Endereço de requisição - [ GET `/city` ]</strong>

<strong>Retorna informações sobre cidades presentes no banco de dados.</strong>

<h4>Corpo da Requisição:</h4>

Não requer informação no corpo da requisição.

<h4>Resposta da API:</h4>

A resposta segue o formato abaixo com status <code>200</code>:

    [
      {
    	"cityId": 1,
    	"name": "Blumenau",
    	"state": "SC"
      },
      /* ... */
    ]

</details>

<details>
<summary>POST /city</summary>
<h2>POST /city</h2>

<strong>Endereço de requisição - [ POST `/city` ]</strong>

<strong>Adiciona uma nova cidade ao banco de dados.</strong>

<h4>Corpo da Requisição:</h4>

O corpo da requisição deve seguir o formato abaixo:

    {
    	"Name": "Blumenau",
    	"State": "SC"
    }

<h4>Resposta da API:</h4>
A resposta segue o formato abaixo com status <code>201</code>:

    {
    	"cityId": 1,
    	"name": "Blumenau",
    	"state": "SC"
    }

</details>

<details>
<summary>PUT /city</summary>
<h2>PUT /city</h2>

<strong>Endereço de requisição - [ PUT `/city` ]</strong>

<strong>Atualiza informações de uma cidade existente no banco de dados.</strong>

<h4>Corpo da Requisição:</h4>

O corpo da requisição deve seguir o formato abaixo:

    {
    	"CityId": 1,
    	"Name": "Blumenau",
    	"State": "SC"
    }
<h4>
 Resposta da API:
</h4>
A resposta segue o formato abaixo com status <code>200</code>:

    {
    	"cityId": 1,
    	"name": "Blumenau",
    	"state": "SC"
    }

</details>

<h1>Hotel</h1>

<strong>A funcionalidade de hotel permite que os usuários obtenham informações sobre hotéis presentes no banco de dados. Os usuários também podem adicionar novos hotéis, fornecendo detalhes como nome, endereço e cidade. Possui as seguintes requisições: GET, POST.</strong>

<details>
<summary>GET /hotel</summary>
<h2>GET /hotel</h2>

<strong>Endereço de requisição - [ GET `/hotel` ]</strong>

<strong>Retorna informações sobre hotéis presentes no banco de dados.</strong>

<h4>Corpo da Requisição:</h4>

Não requer informação no corpo da requisição.

<h4>Resposta da API:</h4>
A resposta segue o formato abaixo com status <code>200</code>:

    [
      {
    	"hotelId": 1,
    	"name": "Trybe Hotel BNU 1",
    	"address": "Rua XV de Novembro",
    	"cityId": 1,
    	"cityName": "Blumenau",
    	"state": "SC"
      },
      /* ... */
    ]

</details>

<details>
<summary>POST /hotel</summary>
<h2>POST /hotel</h2>

<strong>Endereço de requisição - [ POST `/hotel` ]</strong>

<strong>Adiciona um novo hotel ao banco de dados.</strong>

-- Obs.: É necessário token de autenticação como **Administrador** --

<h4>Corpo da Requisição:</h4>

O corpo da requisição deve seguir o formato abaixo:

    {
	"Name":"Trybe Hotel BNU 1",
	"Address":"Rua XV de Novembro",
	"CityId": 1
    }

<h4>Resposta da API</h4>
A resposta segue o formato abaixo com status <code>201</code>:

    {
    	"hotelId": 1,
    	"name": "Trybe Hotel BNU 1",
    	"address": "Rua XV de Novembro",
    	"cityId": 1,
    	"cityName": "Blumenau",
    	"state": "SC"
    }

</details>

 <h1>Room</h1>

<strong>A funcionalidade de quarto envolve operações relacionadas a quartos de hotel. Os usuários podem obter informações detalhadas sobre um quarto específico, adicionar novos quartos ao banco de dados ou remover quartos existentes. Cada quarto está associado a um hotel específico. Possui as seguintes requisições: GET, POST e DELETE.</strong>

<details>
<summary>GET /room/:hotelId</summary>
<h2>GET /room/:hotelId</h2>

<strong>Endereço de requisição - [ GET `/room/:hotelId` ]</strong>

<strong>Retorna todos os quartos de um determinado hotel a partir de um ID</strong>

<h4>Corpo da Requisição:</h4>

O corpo da requisição é vazio, com o ID do quarto vindo do próprio endereço de requisição { `/:hotelId` }

<h3>Resposta da API:</h3>
A resposta segue o formato abaixo com status <code>200</code>

    [
      {
    	"roomId": 1,
    	"name": "Suite básica",
    	"capacity": 2,
    	"image": "image suite",
    	"hotel": {
    		"hotelId": 1,
    		"name": "Trybe Hotel BNU 1",
    		"address": "Rua XV de Novembro",
    		"cityId": 1,
    		"cityName": "Blumenau",
    		"state": "SC"
      	  }
      }
    ]

</details>

<details>
<summary>POST /room</summary>
<h2>POST /room</h2>

<strong>Endereço de requisição - [ POST `/room` ]</strong>

<strong>Adiciona um novo quarto ao banco de dados.</strong>

-- Obs.: É necessário token de autenticação como **Administrador** --

<h4>Corpo da Requisição:</h4>

O corpo da requisição deve seguir o formato abaixo:

    {
    	"Name":"Suite básica",
    	"Capacity":2,
    	"Image":"image suite",
    	"HotelId": 1
    }

<h4>Resposta da API:</h4>
A resposta segue o formato abaixo com status <code>201</code>:

    {
    	"roomId": 1,
    	"name": "Suite básica",
    	"capacity": 2,
    	"image": "image suite",
    	"hotel": {
    		"hotelId": 1,
    		"name": "Trybe Hotel BNU 1",
    		"address": "Rua XV de Novembro",
    		"cityId": 1,
    		"cityName": "Blumenau",
    		"state": "SC"
    	}
    }

</details>

<details>
<summary>DELETE /room</summary>
<h2>DELETE /room</h2>

<strong>Endereço de requisição - [ DELETE `/room/:roomId` ]</strong>

<strong>Remove um quarto do banco de dados.</strong>

-- Obs.: É necessário token de autenticação como **Administrador** --

<h4>Corpo da Requisição:</h4>

O corpo da requisição é vazio, com o ID do quarto vindo do próprio endereço de requisição { `/:roomId` }

<h4>Resposta da API:</h4>

<strong>A API responde apenas com um status <code>403</code></strong>

</details>

<h1>Booking</h1>

<strong>A funcionalidade de reserva (booking) trata de pedidos de hospedagem. Os usuários podem visualizar informações sobre reservas existentes, bem como criar novas reservas. As informações das reservas incluem detalhes como datas de check-in e check-out, quantidade de hóspedes e quarto reservado. Possui as seguintes requisições: GET, POST.</strong>

<details>
<summary>GET /booking</summary>
<h2>GET /booking</h2>

<strong>Endereço de requisição - [ GET `/booking` ]</strong>

<strong>Retorna informações sobre reservas/pedidos presentes no banco de dados.</strong>

<h4>Corpo da Requisição:</h4>

-- Obs.: É necessário token de autenticação como **Cliente** --

Não requer informação no corpo da requisição.

<h4>Resposta da API:</h4>
A resposta segue o formato abaixo com status <code>200</code>:

	{
    	"bookingId": 1,
    	"checkIn": "2030-08-27T00:00:00",
    	"checkOut": "2030-08-28T00:00:00",
    	"guestQuant": 1,
    	"room": {
    		"roomId": 1,
    		"name": "Suite básica",
    		"capacity": 2,
    		"image": "image suite",
    		"hotel": {
    			"hotelId": 1,
    			"name": "Trybe Hotel BNU 1",
    			"address": "Rua XV de Novembro",
    			"cityId": 1,
    			"cityName": "Blumenau",
    			"state": "SC"
			}
		}
	}

</details>

<details>
<summary>POST /booking</summary>
<h2>POST /booking</h2>

<strong>Endereço de requisição - [ POST `/booking` ]</strong>

<strong>Faz uma nova reserva/pedido no banco de dados.</strong>

-- Obs.: É necessário token de autenticação como **Cliente** --

<h4>Corpo da Requisição:</h4>

O corpo da requisição deve seguir o formato abaixo:

<strong>Obs.: A quantidade de "Guests" não pode ser maior do que a capacidade do quarto!</strong>

    {
    	"CheckIn":"2030-08-27",
    	"CheckOut":"2030-08-28",
    	"GuestQuant":"1",
    	"RoomId":1
    }

<h4>
Resposta da API:
</h4>

<details>
<summary><strong>Em caso de sucesso (Status 201):</strong></summary>

        {
        	"bookingId": 1,
        	"checkIn": "2030-08-27T00:00:00",
        	"checkOut": "2030-08-28T00:00:00",
        	"guestQuant": 1,
        	"room": {
        		"roomId": 1,
        		"name": "Suite básica",
        		"capacity": 2,
        		"image": "image suite",
        		"hotel": {
        			"hotelId": 1,
        			"name": "Trybe Hotel BNU 1",
        			"address": "Rua XV de Novembro",
        			"cityId": 1,
        			"cityName": "Blumenau",
        			"state": "SC"
        			}
        		}
        }

</details>

<details>
<summary><strong>Em caso de falha (Status 400):</strong></summary>

    {
    	"message": "Guest quantity over room capacity"
    }

</details>

</details>

<h1>GeoLocation</h1>

<strong>A funcionalidade de geolocalização permite que os usuários obtenham informações relacionadas a localizações geográficas. A API pode verificar o status de uma API externa de geolocalização e também retornar hotéis próximos com base em um endereço fornecido. Possui as seguintes requisições: GET(status), GET.</strong>

<strong>Obs:. As rotas pertencentes a GeoLocation fazem uso de uma API externa para coletar os dados de endereço, portanto podendo conter retornos que não vão de acordo com os padrões previamente utilizados nesta API.</strong>

<details>
<summary>GET /geo/status</summary>
<h2>GET /geo/status</h2>
<strong>Endereço de requisição - [ GET <code>/geo/status</code> ]</strong>

<strong>Verifica o status da API externa de geolocalização.</strong>

<h4>Corpo da Requisição:</h4>

Não requer informação no corpo da requisição.

<h4>Resposta da API:</h4>
Caso a API externa esteja em pleno funcionamento, a resposta seguirá o formato abaixo, com status <code>200</code>:

    {
    	"status": 200,
    	"message": "OK",
    	"data_updated": "2020-05-04T14:47:00+00:00",
    	"software_version": "3.6.0-0",
    	"database_version": "3.6.0-0"
    }

</details>

<details>
<summary>GET /geo/address</summary>
<h2>GET /geo/address</h2>

<strong>Endereço de requisição - [ GET `/geo/address` ]</strong>

<strong>Retorna hotéis próximos com base em um endereço.</strong>

<h4>Corpo da Requisição:</h4>

O corpo da requisição deve seguir o formato abaixo:

    {
    	"Address": "Rodovia Antonio Heil",
    	"City":"Brusque",
    	"State":"SC"
    }

<h4>Resposta da API:</h4>
Caso a API externa esteja em pleno funcionamento, a resposta seguirá o formato abaixo, com status <code>200</code>:

      [
          {
    		"hotelId": 2,
    		"name": "Trybe Hotel BQ 2",
    		"address": "Rodovia Atonio Heil 2",
    		"cityName": "Brusque",
    		"state": "SC",
    		"distance": 64
          },
          {
    		"hotelId": 1,
    		"name": "Trybe Hotel BQ 3",
    		"address": "Rodovia Antonio Heil 3",
    		"cityName": "Itajaí",
    		"state": "SC",
    		"distance": 89
          },
        /* ... */
      }

</details>

<h1>User</h1>

<strong>A funcionalidade de usuário trata do gerenciamento de contas de usuário. Os usuários podem obter informações sobre usuários registrados, bem como criar novos usuários. A autenticação é necessária para realizar ações de usuário. Possui as seguintes requisições: GET, POST.</strong>

<details>
<summary>GET /user</summary>
<h2>GET /user</h2>

<strong>Endereço de requisição - [ GET `/user` ]</strong>

<strong>Retorna informações sobre usuários presentes no banco de dados.</strong>

-- Obs.: É necessário token de autenticação como **Administrador** --

<h4>Corpo da Requisição:</h4>

Não requer informação no corpo da requisição.

<h4>Resposta da API:</h4>
A resposta segue o formato abaixo com status <code>200</code>:

    [
        {
    		"userId": 1,
    		"name": "Rebecca",
    		"email": "rebecca.guts@trybehotel.com",
    		"userType": "client"
        },
      /*...*/
    ]

</details>

<details>
<summary>POST /user</summary>
<h2>POST /user</h2>

<strong>Endereço de requisição - [ POST `/user` ]</strong>

<strong>Cria um novo usuário no banco de dados.</strong>

<strong>Obs.: Não é possível cadastrar um e-mail já existente no banco de dados.</strong>
<strong>Obs.: Na criação de um novo usuário, o campo userType é automaticamente definido como "client".</strong>

<h4>Corpo da Requisição:</h4>

O corpo da requisição deve seguir o formato abaixo:

    {
    	"Name":"Rebecca",
    	"Email": "rebecca.guts@trybehotel.com",
    	"Password": "123456"
    }

<h4>Resposta da API:</h4>

<details>
<summary><strong>Em caso de sucesso (Status 201):</strong></summary>

    {
    	"userId": 1,
    	"Name":"Rebecca",
    	"Email": "rebecca.guts@trybehotel.com",
    	"userType": "client"
    }

</details>

<details>
<summary><strong>Em caso de falha (Status 409):</strong></summary>

    {
    	"message": "User email already exists"
    }

</details>

</details>
