# **ERM. Core API**

## Introduction

This project is an API for managing enterprise risks (ERM). It provides endpoints for creating and retrieving risk
profiles and migrating risk data to persistent storage.

## Usage

### Endpoints

- **GET /api/v1/riskprofiles**: Get all risks.
- **GET /api/v1/riskprofiles?name=Risk**: Get risk by name.
- **POST /api/v1/riskprofiles**: Add a new risk.
- **DELETE /api/v1/riskprofiles?name=Risk**: Delete by name.

### Request Examples

#### Get all risks

```http
GET /api/v1/riskprofiles
```

#### Get risk by Name

```http
GET /api/v1/riskprofiles?name=Risk
```

#### Add a risk

```http
POST /api/v1/riskprofiles
Content-Type: application/json

{
  "name": "Risk name",
  "description": "description",
  "businessProcess": "business Process",
  "occurrenceProbability": 0,
  "potentialBusinessImpact": 0
}
```

#### Delete risk by Name

```http
DELETE /api/v1/riskprofiles?name=Risk
```

## Technologies Used

- C#
- .NET Core
- PostgreSQL
- Entity Framework
- Fluent Validation
- Automapper