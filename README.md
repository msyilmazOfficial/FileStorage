# FileStorage Project


A backend service built with **.NET 8.0** for securely storing, organizing, and sharing files. This service is similar to cloud storage solutions like Dropbox and Google Drive. It allows users to upload files, organize them in folders, and control access permissions.

## Features

- **File Upload**: Upload files of various types (documents, images, videos).
- **Folder Structure**: Organize files into a hierarchical folder structure.
- **Sharing**: Share files and folders with other users via links or email invitations.
- **Permissions**: Control access permissions (view-only, edit, comment, delete) for shared content.
- **Version Control**: Basic version control to view and restore previous file versions.
- **Search**: Search for files and folders based on metadata.
- **Activity Logs**: Track uploads, downloads, sharing events, and more.

## Technologies Used

- **.NET 8.0**: Backend framework for building the API.
- **Swagger**: For API documentation and testing.
- **SQL Server / Azure SQL Database**: To store file metadata and user information.
- **Azure Blob Storage**: For storing the file contents.
- **Cookie Authentication**: For securing API endpoints.
- **Swashbuckle**: For generating Swagger documentation.

## Setup Instructions
In the appsettings.json **Your-SQL-Server-ConnectionString** and **Your-Azure-Blob-Storage-Connection-String** should be replaced with ConnectionString accordingly.

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server or Azure SQL Database instance
- Azure Blob Storage account

### Clone the Repository

```bash
git clone https://github.com/msyilmazOfficial/FileStorage.git
cd file-storage-service
