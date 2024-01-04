# E-Shop .NET

![E-Shop Logo](/assets/shopping.png)

E-Shop .NET is a robust e-commerce platform built on .NET Core 6.0 with a MongoDB backend. It follows the Repository pattern for data access abstraction and implements the CQRS pattern using Mediatr for seamless command and query handling. The project is designed to be scalable and flexible, with future plans to incorporate React as the front-end technology.

## Features

- **RESTful API**: Built using .NET Core 6.0 for efficient and scalable backend services.
- **MongoDB Database**: Utilizes MongoDB for a flexible and scalable NoSQL database solution.
- **Repository Pattern**: Implements the repository pattern for organized and testable data access.
- **CQRS with Mediatr**: Adopts the CQRS pattern with Mediatr for clean and maintainable command and query handling.

## Getting Started

### Prerequisites

- [.NET Core 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MongoDB](https://www.mongodb.com/try/download/community)

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/shakerkamal/e-shop-dotnet.git
    cd e-shop-dotnet
    ```

2. Restore dependencies and build the project:

    ```bash
    dotnet restore
    dotnet build
    ```

3. Configure MongoDB connection in `appsettings.json`.

4. Run the application:

    ```bash
    dotnet run
    ```

## API Documentation

Visit [API Documentation](link-to-your-api-docs) for detailed information on available endpoints, request/response formats, and authentication details.

## Contributing

If you would like to contribute to the project, please follow our [Contributing Guidelines](CONTRIBUTING.md).

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Thanks to [MediatR](https://github.com/jbogard/MediatR) for providing a clean and simple implementation of the CQRS pattern.
- Special thanks to the .NET Core and MongoDB communities for their excellent support and resources.
