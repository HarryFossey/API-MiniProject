# API-MiniProject

## Install

    clone the repository

## Run the app

    run the solution

# REST API

The REST API to the example app is described below.

## Get list of Things

### Request

`GET /api/Customers`

    curl -X 'GET' \ 'https://localhost:port/api/Customers' \ -H 'accept: text/plain'

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

`POST /Customer/`

    'Accept: application/json' (https://localhost:7075/api/Customers)

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
    "orders":{},
    "status":"200 Success"}

## Get a specific Customer

### Request

`GET /api/Customers/{id}`

    curl -X 'GET' \ 'https://localhost:port/api/Customers/ALFKI' \ -H 'accept: text/plain'

### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: length

    [body]
    

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

### Request

## Delete a Customer


`DELETE /api/Customers/{id}

    curl -X 'DELETE' \ 'https://localhost:7075/api/Customers/ALFKI' \ -H 'accept: */*'

### Response

    HTTP/1.1 204 No Content
    Date: Thu, 24 Feb 2011 12:36:32 GMT
    Status: 204 No Content
    Connection: close
