# MultiTracks

This project consists of a .NET Web Forms page called `artistDetails.aspx` and an API built using .NET. The web page retrieves artist information from a database and displays it on the page. Additionally, the API provides three endpoints: `Get Artist by Name: api.multitracks.com/artist/search`, `Get All Songs with Paging Support: api.multitracks.com/artist/add`, and `Add Artist: api.multitracks.com/song/list`. 

## Web Forms Page: artistDetails.aspx

The `artistDetails.aspx` page is a part of the .NET Web Forms application. It retrieves artist information from the database and displays it on the page. The page is designed to provide detailed information about a specific artist. Just add the `artistID` parameter to the URL with a value. Example: **http://localhost:56916/artistDetails.aspx?artistID=1**

## API Endpoints

The API component of this project is built using .NET and provides the following endpoints:

### 1. Get Artist by Name

Endpoint: `GET /api.multitracks.com/artist/search/{name}`

This endpoint allows you to retrieve artist information by providing the artist's name as a parameter. It returns a JSON object containing details such as the artist's name, biography, and other information.

### 2. Get All Songs with Paging Support

Endpoint: `GET /api.multitracks.com/songs/list/{pageSize}/{pageNumber}`

This endpoint enables you to retrieve a paginated list of songs. You can specify the page number and page size as query parameters. The response is a JSON array containing the songs' details, including the song name, artist and other relevant information.

### 3. Add Artist

Endpoint: `POST /api.multitracks.com/artist/add`

This endpoint allows you to add a new artist to the database. You need to send a JSON payload in the request body containing the artist's details, such as name, biography, and any other relevant information. On successful addition, it returns a response with the newly created artist's information.

## Running the Project

To run the project successfully, follow these steps:

1. Follow the first basic instructions provided (Clone the repo locally, Open the solution in Visual Studio, Run "Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r" from the Package Manager Console)

2. Run the first scripts (already provided by MultiTracks).

3. Run the new script, GetArtistDetails.sql.

4. Update the database connection string on both the Web Forms project and the API, to point to your database. (Web.config and appsettings.json, respectively)

5. Build the project: Build the solution to ensure all the necessary dependencies are resolved and the project is compiled successfully.

6. Run the project: Start the project by running the web application. The `artistDetails.aspx` page should be accessible through the web browser at the appropriate URL. Ensure that the API endpoints are also accessible at their respective routes.

7. Test the API endpoints: Use Swagger, which is available, or utilize tools like Postman or cURL to interact with the API endpoints. Send HTTP requests to the endpoints described earlier and validate the responses.
