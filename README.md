Document Management Web Application
📌 Overview
This project is a web application that provides full document management functionality.
Users can create, upload, share, and manage documents with expiration rules, history tracking, and statistics.

✨ Features
Document creation and upload with:

Name

Custom description

File upload

Expiration/handling time (configurable)

Document history:

When a document’s handling time expires, it is moved to History.

Documents in History are automatically deleted after 30 days.

Soft delete: when a user deletes a document, it is moved to Archive and scheduled for deletion after 30 days.

Document sharing:

Documents can be shared via a unique link.

Once the link is opened, a temporary copy of the original document is created in Data/Temp.

Temporary copies expire after 15 minutes and are deleted after download.

CRUD operations for documents (Create, Read, Update, Delete).

Comments and metadata:

Users can leave comments under documents.

Metadata such as user coordinates can be attached (via map popup selection).

Statistics:

User statistics displayed by year and duration.

Detailed insights into document management activity.

Authentication:

Only authorized users can work with documents.

Login and registration are provided via ASP.NET Identity.

⚙️ Project Setup
1. Create external Docker network
bash
docker network create shared-net
2. Configure environment variables
Rename custom.env to .env.

Adjust values inside .env as needed (database host, port, credentials, etc.).

3. Start database container
bash
docker compose -f ./docker-compose.database.yml up -d --build --force-recreate
4. Run the application
You can run the project in two ways:

Option A: Deploy with Docker Compose
bash
docker compose -f ./docker-compose.deploy.yml up -d --build --force-recreate
Option B: Debug from development environment
Use Docker Compose integration in your IDE (Visual Studio / Rider).

Run in debug mode with docker-compose.
