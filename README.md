# AEShip

Rest API to manage the ships and find the nearest port to a given ship with the estimated arrival time based on the velocity and geolocation (longitude and latitude) of the given ship.

The API is implemented using:
- C#
- EF Core SqlServer for database
- Autofac for IoC container
- AutoFixture and Moq for unit testing

## Assumptions

- Velocity is using `km/hour`
- Velocity does not care about the vector, therefore the value will always be a positive number
- Use Haversine formula to determine the distance between two points given their longitudes and latitudes
- Earth radius in the Haversine formula is `6371` since the velocity is using `km/hour`
- Since no REST API is required for creating ports, the initial ports are seeded with random data
- Estimated arrival time will be in `hours` because the velocity is `km/hour` and the distance will be in `km`

## Models

There are 2 classes that will represent Port and Ship respectively in the database:
```csharp
public class Port
{
     public string Id { get; set; }
     public string Name { get; set; }
     public double Latitude { get; set; }
     public double Longitude { get; set; }
}

public class Ship
{
     public string Id { get; set; }
     public string Name { get; set; }
     public double Latitude { get; set; }
     public double Longitude { get; set; }
     public double Velocity { get; set; }
}
```

## Find nearest port along with the estimated arrival time

Haversine formula is used to calculate the distance between a port and a ship. For more details, please check [`ShipService.cs`](https://github.com/currarpickt/AEShip/blob/main/AEShip.Service/Services/ShipService.cs) and [`ShipUtilities.cs`](https://github.com/currarpickt/AEShip/blob/main/AEShip.Service/Services/ShipUtilities.cs)

To get the nearest port:
- Get the ship location from ship id
- Get all ports from the system
- Iterate over the available ports and calculate the distance between each port and the ship
- Order the result based on the distance
- Return the first element

To calculate the estimated arrival time: `Distance from the nearest port / ship velocity`.


## Run the API

1. Open AEShip solution
2. Build and run AEShip project
3. It will automatically show [list of ports](http://localhost:51785/api/ports) from initial seeding.
4. Use Postman to test the APIs

## List of APIs
The available APIs match the challenge stories:

**1. Add ship(s): ```POST api/ships```** 

It will return 200OK when the operation is successful.

Request body:

```json
[   
    {
        "Id": "S03",
        "Name": "Ship_C",
        "Latitude": 51.32429,
        "Longitude": -18.78973,
        "Velocity": 8
    },
    {
        "Id": "S04",
        "Name": "Ship_D",
        "Latitude": -63.78201,
        "Longitude": 27.32808,
        "Velocity": 11
    },
    {
        "Id": "S05",
        "Name": "Ship_E",
        "Latitude": 5.51874,
        "Longitude": -134.15695,
        "Velocity": 8
    }
]

```

**2. View all ships: ```GET api/ships```** 

It will return 200OK along with the following response when the request is successful.

```json
[   
    {
        "Id": "S03",
        "Name": "Ship_C",
        "Latitude": 51.32429,
        "Longitude": -18.78973,
        "Velocity": 8
    },
    {
        "Id": "S04",
        "Name": "Ship_D",
        "Latitude": -63.78201,
        "Longitude": 27.32808,
        "Velocity": 11
    },
    {
        "Id": "S05",
        "Name": "Ship_E",
        "Latitude": 5.51874,
        "Longitude": -134.15695,
        "Velocity": 8
    }
]
```

**3. Update velocity of a ship: ```PUT api/ships/{id}```**

Request body:

```json
{
    "Velocity": 46
}
```

Response:
- HTTP 200: When the operation is successful
- HTTP 404: When ship id does not exist in the system.
```json
{
    "message": "No ship found with Id:S10"
}
```

**4. Find the nearest port to a ship with estimated arrival time to the port together with port details: ```GET api/ships/nearesPort/{id}```**

Response:
- HTTP 200: When the operation is successful along with the following response

```json
{
    "id": "P03",
    "name": "Port_C",
    "latitude": -49.7501,
    "longitude": -54.88544,
    "estimatedArrivalTimeInHours": 190.4843553001511
}
```
- HTTP 404: When ship id does not exist in the system.
```json
{
    "message": "No ship found with Id:S10"
}
```

**5. [EXTRA] View all ports available: ```GET api/ports```**
```json
[   
    {
        "Id": "S03",
        "Name": "Ship_C",
        "Latitude": 51.32429,
        "Longitude": -18.78973,
        "Velocity": 8
    },
    {
        "Id": "S04",
        "Name": "Ship_D",
        "Latitude": -63.78201,
        "Longitude": 27.32808,
        "Velocity": 11
    },
    {
        "Id": "S05",
        "Name": "Ship_E",
        "Latitude": 5.51874,
        "Longitude": -134.15695,
        "Velocity": 8
    }
]
```

## Reference
- [Haversine formula](https://en.wikipedia.org/wiki/Haversine_formula)
