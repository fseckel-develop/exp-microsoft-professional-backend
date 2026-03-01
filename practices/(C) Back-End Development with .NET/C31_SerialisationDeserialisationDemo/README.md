# (C31) SerialisationDeserialisationDemo

An ASP.NET Core API demonstrating object serialisation and deserialisation using JSON, XML, and binary formats, including file persistence and API-based data exchange.

---
## Course Context

**Course**: Back-End Development with .NET  
**Section**: Serialisation and Deserialisation

This project demonstrates how .NET applications convert objects into transferable or storable formats and reconstruct them back into usable objects. It explores multiple serialisation approaches through API endpoints and file storage, illustrating how data moves between applications, files, and networks.

---
## Concepts Demonstrated

- Object serialisation and deserialisation
- JSON serialisation using `System.Text.Json`
- XML serialisation using `XmlSerializer`
- Manual binary serialisation for compact storage
- API request body deserialisation
- Automatic vs manual serialisation in ASP.NET Core
- File persistence across multiple formats
- Data transfer and API payload validation

---
## Project Structure

- **Models** – Course domain model used for serialisation examples  
- **Services** – Logic for serialising/deserialising objects and storing them in files  
- **Data** – Example files generated through binary, XML, and JSON storage  
- **Program.cs** – API endpoints demonstrating serialisation, deserialisation, and file persistence  
- **Requests.http** - HTTP requests to provided endpoints for testing