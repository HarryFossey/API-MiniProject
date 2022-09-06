# API-MiniProject

## Install

    clone the repository

## Run the app

    run the solution

# REST API

The REST API for the Customer app is described below.

## Get list of Customers
### Request

`GET /api/Customers`

    'GET' \ 'https://localhost:port/api/Customers' 

### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: length

    [body]

## Create a new Customer
### Request

`POST /Customers/`

    'Accept: application/json' (https://localhost:port/api/Customers)

### Response

    HTTP/1.1 201 Created
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 201 Created
    Connection: close
    Content-Type: application/json
    Location: /customers
    Content-Length: 36

    {"customerId": "ALFKI",
    "companyName": "Alfreds Futterkiste",
    "contactName": "Maria Anders",
    "contactTitle": "Sales Representative",
    "address": "Obere Str. 57",
    "city": "Berlin",
    "region": null,
    "postalCode": "12209",
    "country": "Germany",
    "phone": "030-0074321",
    "fax": "030-0076545",
    "orders":{}
    }

## Get a specific Customer
### Request

`GET /api/Customers/{id}`

    'https://localhost:port/api/Customers/ALFKI'

### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: length

    [body]
    
    
## Get all orders from Customer
### Request

`GET /api/Customers/{id}/Orders`

    curl -X 'GET' \ 'https://localhost:7075/api/Customers/{id}/Orders' \ -H 'accept: text/plain'

### Response

    Status: 200 OK
    [body]
    
## Get specific order from Customer
### Request

`GET /api/Customers/{id}/Orders/{id}`

    curl -X 'GET' \ 'https://localhost:7075/api/Customers/{id}/Orders/{id}' \ -H 'accept: text/plain'

### Response

    Status: 200 OK
    
    {
      "orderId": 10625,
      "customerId": "ANATR",
      "employeeId": 3,
      "orderDate": "1997-08-08T00:00:00",
      "requiredDate": "1997-09-05T00:00:00",
      "shippedDate": "1997-08-14T00:00:00",
      "shipVia": 1,
      "freight": 43.9,
      "shipName": "Ana Trujillo Emparedados y helados",
      "shipAddress": "Avda. de la Constitución 2222",
      "shipCity": "México D.F.",
      "shipRegion": null,
      "shipPostalCode": "05021",
      "shipCountry": "Mexico",
      "customer": null
    }
    

## Update a specific Customer
### Request

`PUT /api/Customers/{id}`

### Response

    HTTP/1.1 204 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 204 OK
    Connection: close
    Content-Type: application/json
    Content-Length: length

    [body]

## Delete a Customer
### Request

`DELETE /api/Customers/{id}

    'https://localhost:port/api/Customers/{id} 
    
### Response

    HTTP/1.1 204 No Content
    Date: Thu, 24 Feb 2011 12:36:32 GMT
    Status: 204 No Content
    Connection: close
