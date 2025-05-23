# Insurance Quote Manager
## Project Overview

**Insurance Quote Manager** is a simple ASP.NET Core application designed for managing Insurance Quotes. The application utilises React as a frontend to display a table of quotes that the user can edit or delete. There is also a form for adding and editing quotes with server-side and client-side validation that gives real-time feedback when submitting form. Uses SQLite as a database.

## Features
- View a list of Quotes
- Add a new Quote via a form page
- Edit an existing Quote's details
- Validates input
- SQLite Database
- Simple UI
- Unit tests for validation on Service layer, and Integration tests on Repository Layer.

## Validation Rules


| Property            | Type       | Validation                                                   |
|---------------------|------------|--------------------------------------------------------------|
| ClientName          | String     | Required, Max length 100                                     |
| ClientAge           | Int        | Required, range: 18 to 100                                   |
| Email               | String     | Required, must contain both letters and numbers              |
| PolicyType          | PolicyType | Required; Life, Auto or Home insurance types                 |
| QuoteStatus         | QuoteStatus| Required; Pending, Approved, Rejected status                 |
| ExpiryDate          | DateTime?  | Required, Custom validation attribute, must be in future     |
| IsSmoker            | Bool       | Optional,                                                    |
| HasChronicIllness   | Bool       | Optional,                                                    |
| Location            | String?    | Optional, High Risk of Floods, used for risk calculations    |
| PropertyType        | String?    | Optional, Apartment or Flat, used for risk calculations      |
| ConstructionType    | String?    | Optional, Wood or brick, used for risk calculations          |
| PropertyAge         | Int?       | Optional, If the property age is higher, higher risk         |

## User Interface
- The main page shows a list of quotes in a table
- Add and edit buttons trigger form for adding or updated the database.

## Testing
Unit and Integration Tests are included.Tests project using xUnit and Moq. Tests cover:
- Validation of individual fields
- Service Layer
- Repository Layer
- Controller logic

## Challenges
- Resolving the issue of performing calculations on an UpdateQuoteRequest, and a CreateQuoteRequest, due to different fields. Solution was to use interface for passing fields in and based on which request was passed in, perform a slightly different calculation.
- Providing feedback validation for ExpiryDate, default was set to null originally, which would cause throw errors during CoverageDurationMethods calculation, and prevent other form errors from displaying. solution was to set a default date to current time, thus giving the custom error that expiry date needed to be in the future, and any other errors.
