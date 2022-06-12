# Technical Test - Full-Stack

## Setup

Before starting, you should Clone or [fork the repo](https://docs.github.com/en/free-pro-team@latest/github/getting-started-with-github/fork-a-repo). If forking, it is **highly recommended** to [create a private fork](https://stackoverflow.com/a/30352360).



## Setup natively
1. Download and install .NET Core.

>https://dotnet.microsoft.com/download

2. Download VSCode or an IDE of your choice.

3. Open the root folder (or the solution file depending on your IDE).

4. Run the project.
```shell
dotnet run -p src/TechnicalTest.Project
```

You can make HTTP calls using curl
```shell
curl http://localhost:5000/home
```

Or using an HTTP client of your choice.


Postman was used to debug the API

# Limitations

Put, Post, and endpoints that get nested relationships do not return status codes due to the object cycles that are caused by the HealthFacility and Practitioner objects containing HealthFacilityService and PractitionerService objects, which also contain HealthFacility and Practitioner objects.

# Endpoints

## Modality Endpoints
Gets all modalities from the modalities.json file using polymorphism.  

Returns 100 entries so example response will not be fully provided  

Example Request:
```
Get  http://localhost:5000/api/Modality/get_all_modality
```

Example Response:
```
[
    {
        "name": " officiis",
        "type": "PaymentModality",
        "detail": "145"
    },
    {
        "name": " quia",
        "type": "PaymentModality",
        "detail": "240"
    },
    {
        "name": " consequatur",
        "type": "PaymentModality",
        "detail": "55"
    },
    ...
]
```

## Health Facility Endpoints

Get the First Health Facility in the Database  

Example Request:
```
Get  http://localhost:5000/api/Facility/get_facility
```

Example Response:
```
{
    "id": "db34c393-8027-7502-a9e4-e94a9b7fba23",
    "name": "Lang Inc",
    "phoneNumber": null,
    "streetNumber": "164 Drew Squares",
    "streetName": "Alexandra Path",
    "city": "Ottawa",
    "isOpen": true,
    "type": "pharmacy",
    "healthFacilityServices": null
}
```

Get a Health Facility and all of its nested relationships by its Id  


Example Request:
```
Get	 http://localhost:5000/api/Facility/get_facility_id?id=ab30dae0-75c1-c1b8-1d1d-63c77d1e3fbb
```


Add a new Health Facility to the Database  


Example Request:
```
Post  http://localhost:5000/api/Facility/post_facility
Body: 
{ 
    "HealthFacility": { 
        "Id": "au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb",
        "IsOpen": "True",
        "Name": "ddd Group",
        "Type": "clinic",
        "HealthFacilityServices" : [
            {
                "ServiceId": "7a42005c-438d-473b-a1dd-a4551d00a08b",
                "HealthFacilityId": "au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb"
            },
            {
                "ServiceId": "f59e958b-e820-4437-e1a0-9ef00d51487c",
                "HealthFacilityId": "au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb"
            }
        ]
    }
}
```

List all Health Facilities with the given name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_facility_name?name=Champlin Group
```
Example Response:
```
{
    "id": "ab30dae0-75c1-c1b8-1d1d-63c77d1e3fbb",
    "city": "Quebec City",
    "isOpen": "true",
    "name": "Champlin Group",
    "phoneNumber": "1-667-785-4072 x8988",
    "streetName": "Dovie Spur",
    "streetNumber": "4737 Williamson Green",
    "type": "clinic"
	"healthFacilityServices": null
}
```

Count all Health Facilities with the given name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_facility_name?name=Champlin Group
```

Example Response:
```
1
```

List all Health Facilities with the given phone  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_facility_phone?phone=1-667-785-4072 x8988
```
Example Response:
```
{
    "id": "ab30dae0-75c1-c1b8-1d1d-63c77d1e3fbb",
    "city": "Quebec City",
    "isOpen": "true",
    "name": "Champlin Group",
    "phoneNumber": "1-667-785-4072 x8988",
    "streetName": "Dovie Spur",
    "streetNumber": "4737 Williamson Green",
    "type": "clinic"
	"healthFacilityServices": null
}
```

Count all Health Facilities with the given phone  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_facility_phone?phone=1-667-785-4072 x8988
```

Example Response:
```
1
```

List all Health Facilities with the given street number  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_facility_street_num?streetnumber=4737 Williamson Green
```

Example Response:
```
{
    "id": "ab30dae0-75c1-c1b8-1d1d-63c77d1e3fbb",
    "city": "Quebec City",
    "isOpen": "true",
    "name": "Champlin Group",
    "phoneNumber": "1-667-785-4072 x8988",
    "streetName": "Dovie Spur",
    "streetNumber": "4737 Williamson Green",
    "type": "clinic"
	"healthFacilityServices": null
}
```

Count all Health Facilities with the given street number  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_facility_street_num?streetnumber=4737 Williamson Green
```

Example Response:
```
1
```

List all Health Facilities with the given street name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_facility_street?streetname=Dovie Spur
```

Example Response:
```
{
    "id": "ab30dae0-75c1-c1b8-1d1d-63c77d1e3fbb",
    "city": "Quebec City",
    "isOpen": "true",
    "name": "Champlin Group",
    "phoneNumber": "1-667-785-4072 x8988",
    "streetName": "Dovie Spur",
    "streetNumber": "4737 Williamson Green",
    "type": "clinic"
	"healthFacilityServices": null
}
```

Count all Health Facilities with the given street name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_facility_street?streetname=Dovie Spur
```

Example Response:
```
1
```

List all Health Facilities with the given city  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_facility_city?city=Quebec City
```

Count all Health Facilities with the given city  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_facility_city?city=Quebec City
```

List all Health Facilities with the given open status  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_facility_open_status?status=true
```

Count all Health Facilities with the given open status  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_facility_open_status?name=true
```

List all Health Facilities with the given type  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_facility_type?type=clinic
```

Count all Health Facilities with the given type  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_facility_type?type=clinic
```

Update existing Health Facility  

Example Request:
```
Put	 http://localhost:5000/api/Facility/put_facility
Body: 
{ 
    "HealthFacility": { 
        "Id": "au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb",
        "IsOpen": "True",
        "Name": "Different Name",
        "Type": "clinic",
        "HealthFacilityServices" : [
            {
                "ServiceId": "7a42005c-438d-473b-a1dd-a4551d00a08b",
                "HealthFacilityId": "au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb"
            },
            {
                "ServiceId": "f59e958b-e820-4437-e1a0-9ef00d51487c",
                "HealthFacilityId": "au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb"
            }
        ]
    }
}
```

Delete Health Facility  

Example Request:
```
Delete	 http://localhost:5000/api/Facility/delete_facility?id=au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb
```

## Practitioner Endpoints

Get the First Practitioner in the Database  

Example Request:
```
Get  http://localhost:5000/api/Practitioner/get_practitioner
```

Get a Practitioner and all of its nested relationships by its Id  

Example Request:
```
Get	 http://localhost:5000/api/Practitioner/get_practitioner_id?id=1a28ae23-0b58-5496-6a33-df58adfcb77b
```

List all Practitioners with the given first name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_practitioner_firstname?firstname=Peter
```

Count all Practitioners with the given first name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_practitioner_firstname?firstname=Peter
```

List all Practitioners with the given last name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_practitioner_lastname?lastname=Leuschke
```

Count all Practitioners with the given last name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_practitioner_lastname?lastname=Leuschke
```

List all Practitioners with the given age  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_practitioner_age?age=46
```

Count all Practitioners with the given age  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_practitioner_age?age=46
```

List all Practitioners with the given practitioner number  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_practitioner_number?practitionernumber=3P774X62OMGB87284
```

Count all Practitioners with the given practitioner number  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_practitioner_number?practitionernumber=3P774X62OMGB87284
```

Add a new Practitioner to the Database  

Example Request:
```
Post  http://localhost:5000/api/Practitioner/post_practitioner
Body: 
{ 
    "practitioner": {
		"Id": "1a24ae23-0b58-5496-6a33-df58adfcb77b",
		"Age": "42",
		"FirstName": "John",
		"HealthFacilityId": "db34c393-8027-7502-a9e4-e94a9b7fba23",
		"LastName": "John",
		"PracticeNumber": "7P774O62OMKB87384"
		"PractitionerServices" : [
            {
                "ServiceId": "7a42005c-438d-473b-a1dd-a4551d00a08b",
                "PractitionerId": "1a24ae23-0b58-5496-6a33-df58adfcb77b"
            }
        ]
	},
}
```

Update an existing practitioner Example Request:
```
Put  http://localhost:5000/api/Practitioner/put_practitioner  

Body: 
{ 
    "practitioner": {
		"Id": "1a24ae23-0b58-5496-6a33-df58adfcb77b",
		"Age": "45",
		"FirstName": "Not John",
		"HealthFacilityId": "db34c393-8027-7502-a9e4-e94a9b7fba23",
		"LastName": "John",
		"PracticeNumber": "7P774O62OMKB87384"
		"PractitionerServices" : [
            {
                "ServiceId": "7a42005c-438d-473b-a1dd-a4551d00a08b",
                "PractitionerId": "1a24ae23-0b58-5496-6a33-df58adfcb77b"
            }
        ]
	},
}
```

Delete a Practitioner from the Database  

Example Request:
```
Delete  http://localhost:5000/api/Practitioner/delete_practitioner?id=1a24ae23-0b58-5496-6a33-df58adfcb77b
```

## Service Endpoints

Get the First Service in the Database  

Example Request:
```
Get  http://localhost:5000/api/Service/get_service
```

Get the First HealthFacilityService in the Database  

Example Request:
```
Get  http://localhost:5000/api/Service/get_facility_service
```

Get the First PractitionerService in the Database  

Example Request:
```
Get  http://localhost:5000/api/Service/get_practitioner_service
```

Get Service and all of its nested relationships by its Id  

Example Request:
```
Get	 http://localhost:5000/api/Service/get_service_id?id=262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8
```

Create a new Service  

Example Request:
```
Post  http://localhost:5000/api/Service/post_service
Body:
{
    "Id": "262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8",
    "Description": "Atque rem adipisci neque. Eum facere voluptatibus quasi deleniti error cumque. Dicta rerum consequatur nulla sed enim aut rerum. Quaerat sit dignissimos porro.",
    "Name": "hic",
	"PractitionerServices" : [
		{
			"ServiceId": "262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8",
			"PractitionerId": "1a24ae23-0b58-5496-6a33-df58adfcb77b"
		}
	],
	"HealthFacilityServices" : [
		{
			"ServiceId": "262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8",
			"HealthFacilityId": "au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb"
		}
	]
}
```

Create a new Health Facility Service  

Example Request:
```
Post  http://localhost:5000/api/Service/post_facility_service?facilityId=au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb?serviceid=262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8
```

Create a new Practitioner Service  

Example Request:
```
Post  http://localhost:5000/api/Service/post_practitioner_service?practitionerId=1a24ae23-0b58-5496-6a33-df58adfcb77b?serviceid=262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8
```

List all Services with the given name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/list_service_name?name=natus
```

Count all Services with the given name  

Example Request:
```
Get	 http://localhost:5000/api/Facility/count_service_name?name=natus
```

List all Services with the given description  

Example Request:
```
Get	 http://localhost:5000/api/Service/list_service_description?description=Molestias expedita ut minus. Voluptatum minima quia odio id enim dolorem. Ipsam maiores dolore ut.
```

Count all Services with the given description  

Example Request:
```
Get	 http://localhost:5000/api/Service/count_service_description?description=Molestias expedita ut minus. Voluptatum minima quia odio id enim dolorem. Ipsam maiores dolore ut.
```

Update Service  

Example Request:
```
Put  http://localhost:5000/api/Facility/put_service
Body:
{
    "Id": "262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8",
    "Description": "New Description",
    "Name": "New Name",
	"PractitionerServices" : [
		{
			"ServiceId": "262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8",
			"PractitionerId": "1a24ae23-0b58-5496-6a33-df58adfcb77b"
		}
	],
	"HealthFacilityServices" : [
		{
			"ServiceId": "262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8",
			"HealthFacilityId": "au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb"
		}
	]
}
```

Delete Service  

Example Request:
```
Delete  http://localhost:5000/api/Facility/delete_service?id=262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8
```

Delete Health Facility Service  

Example Request:
```
Delete  http://localhost:5000/api/Facility/delete_facility_service?facilityid=au4sdae0-75c1-c1b8-1d1d-63c77d1e3fbb?serviceid=262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8
```

Delete Practitioner Service  

Example Request:
```
Delete  http://localhost:5000/api/Facility/delete_practitioner_service?practitionerid=1a24ae23-0b58-5496-6a33-df58adfcb77b?serviceid=262fb9a4-f2de-c9f1-8bc3-4d8bbfad9bd8
```
